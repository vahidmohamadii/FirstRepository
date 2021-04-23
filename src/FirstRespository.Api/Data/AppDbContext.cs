using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FirstRespository.Api.Data
{
    //Bullshit dbcontext
    public class AppDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public static List<PersonModel> Persons { get; set; } 
            = new List<PersonModel>()
        {
            new  PersonModel()
            {
                Id = 1,
                FirstName = "Hamoon",
                LastName = "Zehzad",
                Age = 35
            },
            new  PersonModel()
            {
                Id = 2,
                FirstName = "Vahid",
                LastName = "Mohammady",
                Age = 32
            },
            new  PersonModel()
            {
                Id = 3,
                FirstName = "Vahideh",
                LastName = "Ozkan",
                Age = 99
            },
        };
    }
}
