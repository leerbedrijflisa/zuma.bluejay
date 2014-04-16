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
                    var removeRoleResult = await UserManager.RemoveFromRoleAsync(user.Id, role.Name);
                    if (!removeRoleResult.Succeeded)
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    if (!await RoleManager.RoleExistsAsync(role.Name))
                    {
                        var roleResult = await RoleManager.CreateAsync(new IdentityRole(role.Name));
                        if (!roleResult.Succeeded)
                        {
                            return BadRequest();
                        }
                    }

                    var addToResult = await UserManager.AddToRoleAsync(user.Id, role.Name);
                    if (!addToResult.Succeeded)
                    {
                        // TODO: Needs proper return type or iteration fix.
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
