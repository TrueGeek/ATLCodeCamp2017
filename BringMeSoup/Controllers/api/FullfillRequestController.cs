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

namespace BringMeSoup.Controllers
{

    public class FullfillRequestController : ApiController
    {

        public async Task<IHttpActionResult> Post(Guid id)
        {

            using (var db = new ApplicationDbContext())
            {

                var userId = User.Identity.GetUserId();
                if (userId == null) return Unauthorized();

                var request = db.Requests.FirstOrDefault(x => x.Id == id);
                if (request == null) return BadRequest("Invalid RequestId");

                request.FullfilledByUserId = userId;
                request.FullfilledOn = DateTime.UtcNow;

                await db.SaveChangesAsync();

                return Ok(request);

            }

        }

    }

}

