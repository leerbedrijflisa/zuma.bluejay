using Lisa.Zuma.BlueJay.Models;
using Lisa.Zuma.BlueJay.WebApi.Helpers;
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
    [RoutePrefix("api/role")]
    public class RoleController : BaseApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            var roles = RoleManager.Roles.ToList();
            var result = Converter.ToUserRole(roles);

            return Ok(result);
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]UserRole role)
        {
            if (!await RoleManager.RoleExistsAsync(role.Name))
            {
                // TODO: Add error checking
                await RoleManager.CreateAsync(new IdentityRole(role.Name));

                var addedRole = await RoleManager.FindByNameAsync(role.Name);
                var result = Converter.ToUserRole(addedRole);

                return Created(Request.RequestUri, result);
            }

            return Ok();
        }
    }
}
