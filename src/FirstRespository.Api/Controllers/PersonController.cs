using System.Collections.Generic;
using System.Linq;
using FirstRespository.Api.Dtos.Person;
using Microsoft.AspNetCore.Mvc;

namespace FirstRespository.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class PersonController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            var response = Persons;

            return Ok(response);
        }

        [HttpGet("{id:int}" , Name = "FindRoute")]
        public IActionResult Find([FromRoute] int id)
        {
            var response = Persons.FirstOrDefault(model => model.Id == id);

            if (response is null)
            {
                return NotFound(id);
            }
            else
            {
                return Ok(response);
            }

        }

        [HttpPost("")]
        public IActionResult Create([FromBody] CreatePersonDto createPersonDto)
        {
            var id = Persons.Max(model => model.Id) + 1;

            Persons.Add(new PersonIndexDto()
            {
                Id= id,
                FirstName = createPersonDto.FirstName,
                LastName = createPersonDto.LastName,
                Age = createPersonDto.Age
            });

            return CreatedAtRoute("FindRoute", new { Id = id }, id);
        }

        [HttpPut("")]
        public IActionResult Edit([FromBody] EditPersonDto editPersonDto)
        {
            var person = Persons
                .FirstOrDefault(model => model.Id == editPersonDto.Id);

            if (person is null)
            {
                return NotFound(editPersonDto.Id);
            }
            else
            {
                person.FirstName = editPersonDto.FirstName;
                person.LastName = editPersonDto.LastName;
                person.Age = editPersonDto.Age;

                return Ok(editPersonDto.Id);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Remove([FromRoute] int id)
        {
            var person = Persons.FirstOrDefault(model => model.Id == id);

            if (person is null)
            {
                return NotFound(id);
            }
            else
            {
                Persons.Remove(person);

                return Ok(id);
            }

        }

        private static List<PersonIndexDto> Persons { get; set; } = new List<PersonIndexDto>()
        {
            new  PersonIndexDto()
            {
                Id = 1,
                FirstName = "Hamoon",
                LastName = "Zehzad",
                Age = 35
            },
            new  PersonIndexDto()
            {
                Id = 2,
                FirstName = "Vahid",
                LastName = "Mohammady",
                Age = 32
            },
            new  PersonIndexDto()
            {
                Id = 3,
                FirstName = "Vahideh",
                LastName = "Ozkan",
                Age = 99
            },
        };
    }
}
