using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Lisa.Zuma.BlueJay.Web.Providers
{
    public class ApplicationRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var guid = Guid.NewGuid().ToString();

            var tokenProperties = new AuthenticationProperties(context.Ticket.Properties.Dictionary)
            {
                IssuedUtc = context.Ticket.Properties.IssuedUtc,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(10)
            };

            var ticket = new AuthenticationTicket(context.Ticket.Identity, tokenProperties);

            // maybe only create a handle the first time, then re-use
            refreshTokens.TryAdd(guid, ticket);

            // consider storing only the hash of the handle
            context.SetToken(guid);
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            AuthenticationTicket ticket;
            if (refreshTokens.TryRemove(context.Token, out ticket))
            {
                context.SetTicket(ticket);
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            CreateAsync(context);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            ReceiveAsync(context);
        }

        private static ConcurrentDictionary<string, AuthenticationTicket> refreshTokens =
            new ConcurrentDictionary<string, AuthenticationTicket>();
    }
}