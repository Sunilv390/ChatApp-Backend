using BusinessLayer.Interface;
using CommonLayer.Models.RequestData;
using CommonLayer.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        private readonly IConfiguration config;

        public UserController(IUserBL _userBL, IConfiguration _config)
        {
            userBL = _userBL;
            config = _config;
        }

        /// <summary>
        /// Used for User Registration
        /// </summary>
        /// <param name="registration">SignUp</param>
        /// <returns>responsedata</returns>
        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp(UserSignUp registration)
        {
            try
            {
                ResponseData data = userBL.SignUp(registration);
                bool success = false;
                string message;
                if (data == null)
                {
                    message = "Email already exist";
                    return NotFound(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Account Created Successfully";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception e)
            {
                bool success = false;
                string message = e.Message;
                return BadRequest(new { success, message });
            }
        }

        /// <summary>
        /// Post Method for UserLogin
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Responsedata</returns>
        [HttpPost]
        [Route("login")]
        public IActionResult UserLogin(UserLogin login)
        {
            try
            {
                ResponseData data = userBL.UserLogin(login);
                bool success = false;
                string message, jsonToken;
                if (data == null)
                {
                    message = "Enter Valid Email and Password";
                    return NotFound(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Logged in Successfully.";
                    jsonToken = GenerateToken(data, "User");
                    return Ok(new { success, message, data, jsonToken });
                }
            }
            catch (Exception e)
            {
                bool success = false;
                string message = e.Message;
                return BadRequest(new { success, message });
            }
        }

        //Generates Token for Login Portal
        private string GenerateToken(ResponseData response, string type)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserId", response.UserId.ToString()));
                claims.Add(new Claim("EmailAddress", response.EmailAddress.ToString()));
                var token = new JwtSecurityToken(config["Jwt:Issuer"],
                    config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(120),
                    signingCredentials: signingCreds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
