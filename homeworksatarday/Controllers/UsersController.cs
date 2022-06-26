using AutoMapper;
using homeworksatarday.Models.Entites;
using homeworksatarday.Models.RequestDTO;
using homeworksatarday.Models.Rresponse;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using homeworksatarday.Repositries.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace homeworksatarday.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly MasterDbContext dbContext;
        private readonly IUserRepo userRepo;

        public UsersController(IUserRepo userRepo,MasterDbContext dbContext, IMapper mapper)
        {
            
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userRepo = userRepo;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult GetAll()
        {

            return Ok(userRepo.GetAll());


        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        [ActionName("GetById")]

        public IActionResult GetById(int id)
        {
            string Error = "";
            var result = userRepo.GetById(id,out Error);
            if (!(string.IsNullOrWhiteSpace( Error)))
                return NotFound(new { ErrorCode = 404, ErrorMessage = Error });

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddUser(UserAddDTO NewUser)
        {
            string Erorr = "";
            var result = userRepo.Add(NewUser,out Erorr);
            return CreatedAtAction("GetById", new { id = result.UserId }, result);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(UserUpdateDTO NewUser, int id)
        {
            string Erorr = "";
            var result = userRepo.Update(NewUser, id, out Erorr);
            if (string.IsNullOrEmpty(result.UserName))
            {
                return BadRequest(new { Errorcode = 501, ErrorMessage =Erorr });
            }
            if (result == null)
            {
                return NotFound(new { ErrorCode = 404, ErrorMessage = Erorr });

            }

            return NoContent();

        }

        [HttpPatch("{id}")]
        public IActionResult UpdateUserPatch(int id, JsonPatchDocument UserPatch)
        {
            string Erorr = "";
            var CurrentUser = userRepo.UpdatePatch(id,UserPatch,out Erorr);
            if (CurrentUser == null)
            {
                return NotFound(new { ErrorCode = 404, ErrorMessage = Erorr });

            }
            
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string Error = "";
            var result = userRepo.Delete(id,out Error);
            if (result == null)
            {
                return NotFound(new { ErrorCode = 404, ErrorMessage = Error});

            }

            return NoContent();
        }


    }
}
