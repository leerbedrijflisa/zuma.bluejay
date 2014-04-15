using Lisa.Zuma.BlueJay.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Lisa.Zuma.BlueJay.WebApi.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : BaseApiController
    {
        [AllowAnonymous]
        [Route("")]
        public IHttpActionResult Get()
        {
            var users = UserManager.GetAll();
            var result = users.Select(u => new User
            {
                Id = u.Id,
                Type = u.Type,
                UserName = u.UserName,
                Roles = u.Roles.Select(r => new UserRole
                {
                    Id = r.Role.Id,
                    Name = r.Role.Name
                })
                .ToList()
            })
            .ToList();

            return Ok(result);
        }
    }
}
