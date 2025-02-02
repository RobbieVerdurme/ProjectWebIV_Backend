﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using ProjectWebIV_Backend.DTO;
using ProjectWebIV_Backend.Models;
using System.Diagnostics;

namespace ProjectWebIV_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _config;

        public AccountController(
          SignInManager<IdentityUser> signInManager,
          UserManager<IdentityUser> userManager,
          ICustomerRepository customerRepository,
          IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _customerRepository = customerRepository;
            _config = config;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model">the login details</param>
        [HttpPost]
        public async Task<ActionResult<String>> CreateToken(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (result.Succeeded)
                {
                    var role = await _userManager.IsInRoleAsync(user, "Admin");
                    string token = GetToken(user, role ? "Admin":"User");
                    return Created("", token); //returns only the token                    
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="model">the user details</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<String>> Register(RegisterDTO model)
        {            
            IdentityUser user = new IdentityUser { UserName = model.Email, Email = model.Email };
            Customer customer = new Customer { Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "User");
            if (result.Succeeded)
            {
                _customerRepository.Add(customer);
                _customerRepository.SaveChanges();
                string token = GetToken(user, "User");
                return Created("", token);
            }
            return BadRequest();
        }

        /// <summary>
        /// Register a admin
        /// </summary>
        /// <param name="model">the user details</param>
        /// <returns></returns>
        [Authorize(Policy ="Admin", Roles ="Admin")]
        [HttpPost("registerAdmin")]
        public async Task<ActionResult<String>> RegisterAdmin(RegisterDTO model)
        {
            IdentityUser user = new IdentityUser { UserName = model.Email, Email = model.Email };
            Customer customer = new Customer { Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                _customerRepository.Add(customer);
                _customerRepository.SaveChanges();
                string token = GetToken(user, "Admin");
                return Created("", token);
            }
            return BadRequest();
        }

        /// <summary>
        /// Checks if an email is available as username
        /// </summary>
        /// <returns>true if the email is not registered yet</returns>
        /// <param name="email">Email.</param>/
        [AllowAnonymous]
        [HttpGet("checkusername")]
        public async Task<ActionResult<Boolean>> CheckAvailableUserName(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            return user == null;
        }

        private String GetToken(IdentityUser user, string role)
        {
            // Create the token
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.Email),
              new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
              new Claim("roles", role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              null, null,
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}