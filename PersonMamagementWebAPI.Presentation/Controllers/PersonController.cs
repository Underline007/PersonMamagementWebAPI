using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonManagementWebAPI.Application.Dtos;
using PersonManagementWebAPI.Application.Interfaces;
using PersonManagementWebAPI.Entities.Enums;
using System.Net;

namespace PersonManagementWebAPI.Presentation.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        public IActionResult GetAllPerson()
        {
            try
            {
                var person = _personService.GetAllPerson();
                return Ok(person);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all person");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{Id}")]
        public IActionResult GetPersonById(Guid Id) 
        {
            try
            {
                var exsttingPerson = _personService.GetPersonById(Id);
                if (exsttingPerson == null) 
                {
                    return NotFound($"Can not find person with Id: {Id}");
                } else
                {
                    return Ok(exsttingPerson);

                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error while getting person");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult CreatePerson([FromBody] PersonDto person)
        {
            try
            {   
                if (person != null)
                {
                    if (person.DateOfBirth == null)
                    {
                        return BadRequest("DateOfBirth is a required field.");
                    }
                    if (person.Gender == GenderType.Unknown)
                    {
                        return BadRequest("Gender is a required field.");
                    }
                    if (!(Enum.IsDefined(typeof(GenderType), person.Gender)))
                    {
                        return BadRequest("Gender is invalid.");
                    }

                    var createPerson = _personService.CreatePerson(person);
                    return CreatedAtAction(nameof(GetPersonById), new { Id = createPerson.Id }, createPerson);
                }
                return BadRequest("Some feild is null");
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating person");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{Id}")]
        public IActionResult EditPerson(Guid Id, [FromBody] PersonDto person)
        {
            try
            {
                if (person.DateOfBirth == null)
                {
                    return BadRequest("DateOfBirth is a required field.");
                }
                if (person.Gender == GenderType.Unknown)
                {
                    return BadRequest("Gender is a required field.");
                }
                if (!Enum.IsDefined(typeof(GenderType), person.Gender))
                {
                    return BadRequest("Gender is invalid.");
                }
                _personService.EditPerson(Id, person);
                return Ok("Edit person successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while editing person");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult DeletePerson(Guid Id)
        {
            try
            {
                var personToDelete = _personService.GetPersonById(Id);
                if (personToDelete == null)
                {
                    return NotFound($"Person with id {Id} not found");
                }
                _personService.DeletePerson(personToDelete);
                return Ok("Person deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Person");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("filter")]
        public IActionResult FilterPeople(string? name, GenderType? gender, string? birthPlace)
        {
            try
            {
                var filteredPeople = _personService.FilterPeople(name, gender, birthPlace);
                return Ok(filteredPeople);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while filtering people");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


    }
}
