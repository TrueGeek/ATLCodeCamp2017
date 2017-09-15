using BringMeSoup.Models;
using Microsoft.AspNet.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BringMeSoup.Controllers.api
{
    public class RegisterController : ApiController
    {

        public async Task<IHttpActionResult> PostAsync(RegisterUserTypeRequest registerUserTypeRequest)
        {

            var userId = User.Identity.GetUserId();
            if (userId == null) return Unauthorized();

            using (var db = new ApplicationDbContext())
            {

                var user = db.Users.FirstOrDefault(x => x.Id == userId);

                if (user == null) return null;

                user.UserType = registerUserTypeRequest.UserType;

                ApplicationUser associatedSickUser = null;
                if (registerUserTypeRequest.UserType == UserType.Sick)
                {
                    user.SickCode = GenerateSickCode();
                    user.AssociatedSickUserId = null;
                }
                else
                {

                    associatedSickUser = db.Users.FirstOrDefault(x => x.SickCode == registerUserTypeRequest.SickCode);
                    if (associatedSickUser == null)
                    {
                        return BadRequest("Invalid sick code");
                    }

                    user.SickCode = null;
                    user.AssociatedSickUserId = associatedSickUser.Id;

                }

                var result = await db.SaveChangesAsync();

                // return our User model not the Application user
                // otherwise the json serializer will tried to serlializes all the claims
                var userToReturn = new User()
                {
                    Email = user.Email,
                    UserType = user.UserType,
                    SickCode = user.SickCode,
                    AssociatedSickUserId = user.AssociatedSickUserId,
                    AssociatedSickUserEmail = registerUserTypeRequest.UserType == UserType.Sick ? null : associatedSickUser.Email
                };

                return Ok(userToReturn);

            }

        }

        private string GenerateSickCode()
        {

            using (var db = new ApplicationDbContext())
            {
                var highestSickCode = db.Users.Max(x => x.SickCode);
                if (string.IsNullOrEmpty(highestSickCode)) return "1001";

                return (int.Parse(highestSickCode) + 1).ToString();

            }

        }

    }
}
