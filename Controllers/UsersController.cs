using Microsoft.AspNetCore.Mvc;
using Docklly.Database;
using Docklly.Models;
using Docklly.Services;
using Docklly.DTOs;

namespace Docklly.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersServices _userService;

        [HttpGet]
        [Route("api/")]
        public List<UserResponseDto> GetAllUser()
        {
            List<Users> users = _userService.GetAllUsers();

            List<UserResponseDto> response = new List<UserResponseDto>();

            foreach (Users usr in users)
            {
                response.Add(new UserResponseDto
                {
                    Id = usr.Id,
                    Name = usr.Name,
                    Email = usr.Email
                });
            }
            return response;
        }
    }
}