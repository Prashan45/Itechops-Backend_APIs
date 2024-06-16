using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_API.AccessLayer;
using Task_API.Models;
using System;
using System.IO;

namespace Task_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly Savedata _saveInfo;

        public SignupController(Savedata saveInfo)
        {
            _saveInfo = saveInfo;
        }

        [HttpPost("SaveData")]
        public ActionResult SaveUser([FromForm] Signup_Model data, IFormFile profilePicture)
        {
            try
            {
                if (profilePicture != null && profilePicture.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        profilePicture.CopyTo(memoryStream);
                        data.ProfilePicture = memoryStream.ToArray();
                    }
                }

                data.Id = 0;

                _saveInfo.SaveInformation(data);
                return Ok(new { Message = "Data saved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
