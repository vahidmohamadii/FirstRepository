using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstRespository.Api.Data
{
    //Bullshit dbcontext
    public static class AppDbContext
    {
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
