﻿using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shop.WebApi.Identity.Models;

namespace Shop.WebApi.Identity.Stores
{
    public class ShopIdentityWebApiContext : IdentityDbContext<AppUser>
    {
        public ShopIdentityWebApiContext() : base("DefaultConnection", false)
        {
            if (!Database.Exists())
                Database.SetInitializer(new ShopIdentityWebApiContextInitializer());
        }

        public static ShopIdentityWebApiContext Create()
        {
            return new ShopIdentityWebApiContext();
        }
    }
}