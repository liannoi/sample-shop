﻿using System.Collections.Generic;
using System.Security.Claims;

namespace Shop.Clients.WebApi.Core.Identity.Infrastructure.Models.Returnable
{
    public class UserReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public IList<string> Roles { get; set; }
        public IList<Claim> Claims { get; set; }
    }
}