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

        [Route("{id}")]
        public async Task<IHttpActionResult> Get(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = new User
            {
                Id = user.Id,
                Type = user.Type,
                UserName = user.UserName,
                Roles = user.Roles.Select(r => new UserRole
                {
                    Id = r.Role.Id,
                    Name = r.Role.Name
                })
                .ToList()
            };

            return Ok(result);
        }

        [Route("addrole/{id}/{role}")]
        [HttpPost]
        public async Task<IHttpActionResult> AddToRole(string id, string role)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (!await RoleManager.RoleExistsAsync(role))
            {
                var roleResult = await RoleManager.CreateAsync(new IdentityRole(role));
                if (!roleResult.Succeeded)
                {
                    return BadRequest(roleResult.ToString());
                }
            }

            var result = await UserManager.AddToRoleAsync(id, role);
            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok(await UserManager.GetRolesAsync(id));
        }
    }
}
