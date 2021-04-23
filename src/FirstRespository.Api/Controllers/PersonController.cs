using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FirstRespository.Api.Data;
using FirstRespository.Api.Dtos.Person;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstRespository.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class PersonController : ControllerBase
    {
        private readonly IMapper _mapper;

        public PersonController(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<PersonIndexDto>))]
        public IActionResult Get()
        {
            var personModelList = AppDbContext.Persons;

            //var personIndexDtoList = personModelList
            //    .Select(model=> new PersonIndexDto() 
            //    { 
            //        Id= model.Id,
            //        FullName = $"{model.FirstName} {model.LastName}",
            //        Age = model.Age
            //    })
            //    .ToList();

            var personIndexDtoList = _mapper.Map<List<PersonIndexDto>>(personModelList);

            return Ok(personIndexDtoList);
        }

        [HttpGet("{Id:int}", Name = "FindRoute")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(PersonDetailDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ValidationProblemDetails))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(int))]
        public IActionResult Find([FromRoute] FindPersonDto findPersonDto)
        {
            var personModel = AppDbContext.Persons.FirstOrDefault(model => model.Id == findPersonDto.Id);

            if (personModel is null)
            {
                return NotFound(findPersonDto.Id);
            }
            else
            {
                //var personDetailDto = new PersonDetailDto();

                //personDetailDto.Id = personModel.Id;
                //personDetailDto.FirstName = personModel.FirstName;
                //personDetailDto.LastName = personModel.LastName;
                //personDetailDto.FullName = $"{personModel.FirstName} {personModel.LastName}";
                //personDetailDto.Age = personModel.Age;
                //personDetailDto.BirthDateYear = DateTime.Now.AddYears(-personModel.Age).Year;

                var personDetailDto = _mapper.Map<PersonDetailDto>(personModel);

                return Ok(personDetailDto);
            }

        }

        [HttpPost("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(int))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ValidationProblemDetails))]
        public IActionResult Create([FromBody] CreatePersonDto createPersonDto)
        {
            //var personModel = new PersonModel();

            //personModel.Id = AppDbContext.Persons.Max(model => model.Id) + 1;
            //personModel.FirstName = createPersonDto.FirstName;
            //personModel.LastName = createPersonDto.LastName;
            //personModel.Age = createPersonDto.Age;

            var personModel = _mapper.Map<PersonModel>(createPersonDto);

            personModel.Id = AppDbContext.Persons.Max(model => model.Id) + 1;

            AppDbContext.Persons.Add(personModel);

            return CreatedAtRoute("FindRoute", new { Id = personModel.Id }, personModel.Id);
        }

        [HttpPut("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(int))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(int))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ValidationProblemDetails))]
        public IActionResult Edit([FromBody] EditPersonDto editPersonDto)
        {
            var personModel = AppDbContext
                .Persons
                .FirstOrDefault(model => model.Id == editPersonDto.Id);

            if (personModel is null)
            {
                return NotFound(editPersonDto.Id);
            }
            else
            {
                //personModel.FirstName = editPersonDto.FirstName;
                //personModel.LastName = editPersonDto.LastName;
                //personModel.Age = editPersonDto.Age;

                personModel = _mapper.Map(editPersonDto, personModel);

                return Ok(editPersonDto.Id);
            }
        }

        [HttpDelete("{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(int))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ValidationProblemDetails))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound, type: typeof(int))]
        public IActionResult Remove([FromRoute] RemovePersonDto removePersonDto)
        {
            var personModel = AppDbContext.Persons.FirstOrDefault(model => model.Id == removePersonDto.Id);

            if (personModel is null)
            {
                return NotFound(removePersonDto.Id);
            }
            else
            {
                AppDbContext.Persons.Remove(personModel);

                return Ok(removePersonDto.Id);
            }

        }

        [HttpGet("secret")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme , Roles = "Administrator")]
        public IActionResult Secret() 
        {
            return Ok("This is a secret message!");
        }
    }
}
