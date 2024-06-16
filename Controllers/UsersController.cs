using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_API.AccessLayer;
using Task_API.Models;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDataAccess _userDataAccess;

        public UserController(UserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }


        [HttpGet("admin")]
        [Authorize(Policy = "AdminPolicy")] // Only Admin can access this endpoint
        public IActionResult GetUsers()
        {
            var users = _userDataAccess.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("employee")]
        [Authorize(Policy = "EmployeePolicy")] // Only Employee can access this endpoint
        public IActionResult GetUsersForEmployees()
        {
            var users = _userDataAccess.GetAllUsers();
            return Ok(users);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")] // Only Admin can add users
        public IActionResult AddUser([FromBody] Signup_Model user)
        {
            _userDataAccess.AddUser(user);
            return Ok(new { Message = "User added successfully." });
        }


        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")] // Only Admin can update users
        public IActionResult UpdateUser(int id, [FromBody] Signup_Model user)
        {
            var existingUser = _userDataAccess.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            //if (profilePicture != null && profilePicture.Length > 0)
            //{
            //    using (var memoryStream = new MemoryStream())
            //    {
            //        profilePicture.CopyTo(memoryStream);
            //        user.ProfilePicture = memoryStream.ToArray();
            //    }
            //}

            existingUser.Name = user.Name;
            existingUser.Contact = user.Contact;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;
            existingUser.ProfilePicture = user.ProfilePicture;

            _userDataAccess.UpdateUser(existingUser);

            return Ok(new { Message = "User updated successfully." });
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "GuestPolicy")] // Admin, Employee, and Guest can access their own profiles
        public IActionResult GetUser(int id)
        {
            var user = _userDataAccess.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPut("profile/{id}")]
        [Authorize(Policy = "GuestPolicy")] // Admin, Employee, and Guest can update their own profiles
        public IActionResult UpdateUserProfile(int id, [FromBody] Signup_Model user)
        {
            var existingUser = _userDataAccess.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }


            existingUser.Name = user.Name;
            existingUser.Contact = user.Contact;
            existingUser.Email = user.Email;
            existingUser.ProfilePicture = user.ProfilePicture;

            _userDataAccess.UpdateUser(existingUser);

            return Ok(new { Message = "User profile updated successfully." });
        }

    }
}
