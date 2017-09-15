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

    public class RequestsController : ApiController
    {

        public IHttpActionResult Get()
        {

            using (var db = new ApplicationDbContext())
            {

                var userId = User.Identity.GetUserId();
                if (userId == null) return Unauthorized();
                var user = db.Users.FirstOrDefault(x => x.Id == userId);

                var userIdToSearchFor = userId;
                if (user.UserType == UserType.Caretaker) userIdToSearchFor = user.AssociatedSickUserId;

                var requests = db.Requests.Where(x => x.RequestedByUserId == userIdToSearchFor && x.FullfilledByUserId == null).ToList();

                return Ok(requests);

            }

        }

        public async Task<IHttpActionResult> Post()
        {

            using (var db = new ApplicationDbContext())
            {

                var userId = User.Identity.GetUserId();
                if (userId == null) return Unauthorized();

                var request = new Request()
                {
                    Id = Guid.NewGuid(),
                    RequestedByUserId = userId,
                    RequestedOn = DateTime.UtcNow
                };
                db.Requests.Add(request);

                await db.SaveChangesAsync();

                return Ok(request);

            }

        }

    }

}
