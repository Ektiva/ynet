﻿using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Bobbity",
                        Street = "10 The Street",
                        City = "New York",
                        State = "NY",
                        Zipcode = "90210"
                    }
                    
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");

            }
        }
    }
}


//new AppUser
//{
//    DisplayName = "Ektiva",
//    Email = "ektiva@test.com",
//    UserName = "ektiva@test.com",
//    Address = new Address
//    {
//        FirstName = "Ektiva",
//        LastName = "Ektivaly",
//        Street = "12 The Street",
//        City = "Landover",
//        State = "MD",
//        Zipcode = "20784"
//    }
//}