using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstRespository.Api.Dtos.Person
{
    public sealed class PersonDetailDto
    {
        public int Id { get;  set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int BirthDateYear { get; set; }
    }
}
