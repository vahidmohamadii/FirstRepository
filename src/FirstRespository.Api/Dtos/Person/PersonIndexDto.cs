using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstRespository.Api.Dtos.Person
{
    public sealed class PersonIndexDto
    {
        public int Id { get;  set; }
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}
