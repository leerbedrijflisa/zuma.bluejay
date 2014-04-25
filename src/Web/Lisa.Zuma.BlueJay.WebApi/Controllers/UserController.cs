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
    [RoutePrefix("api/user")]
    public class UserController : BaseApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            var users = UserManager.GetAll();
            var result = Converter.ToUser(users, RoleManager.Roles);

            return Ok(result);
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> Get(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = Converter.ToUser(user, RoleManager.Roles);

            return Ok(result);
        }

        [Route("")]
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] User user)
        {
            if (!UserManager.Exists(user.Id))
            {
                return NotFound();
            }

            foreach (var role in user.Roles)
            {
                if (role.Deleted)
                {
                    // TODO: Make extension to delete by role id.
                    var removeRoleResult = await UserManager.RemoveFromRoleByIdAsync(user.Id, role.Id);
                    if (!removeRoleResult.Succeeded)
                    {
                        return BadRequest();
                    }
                }
                else if (!await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    var addToRoleResult = await UserManager.AddToRoleAsync(user.Id, role.Name);
                    if (!addToRoleResult.Succeeded)
                    {
                        // TODO: Create proper error.
                        continue;
                    }
                }
            }

            var dbUser = await UserManager.FindByIdAsync(user.Id);
            var result = Converter.ToUser(dbUser, RoleManager.Roles);

            return Ok(result);
        }
    }
}
