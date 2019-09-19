using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Contact_List.Controllers
{
    [ApiController]
    [Route("contacts")]
    public class ContactsController : ControllerBase
    {
        private static readonly List<Person> people = new List<Person>();

        [HttpGet]
        public IActionResult GetAllItems()
        {
            return Ok(people);
        }

        [HttpPost]
        public IActionResult AddItem([FromBody] Person newPerson)
        {
            if (!ModelState.IsValid) // does not work
            {
                return BadRequest(ModelState);
            }

            if (newPerson.email == null || newPerson.id == 0)
            {
                return BadRequest("Invalid input (e.g. required field missing or empty)");
            }
            people.Add(newPerson);
            return Created("", newPerson);
        }

        [HttpDelete]
        [Route("{personId}")]
        public IActionResult DeleteItem(int personId)
        {
            if (personId >= 0 && personId < people.Count)
            {
                var removed = people.RemoveAll(p => p.id == personId);
                if (removed > 0)
                {
                    return NoContent();
                }
                return NotFound();
            }

            return BadRequest("Invalid ID supplied");
        }


        [HttpGet]
        [Route("findByName")]

        public IActionResult FindByName([FromQuery] string nameFilter)
        {
            if (nameFilter == null || nameFilter.Length == 0)
            {
                return BadRequest("Invalid or missing name");
            }
            return Ok(from p in people where (p.firstName != null && p.firstName.Contains(nameFilter)) || (p.lastName != null && p.lastName.Contains(nameFilter)) select p);
        }


    }
}
