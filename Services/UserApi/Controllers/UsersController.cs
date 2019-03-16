﻿//-----------------------------------------------------------------------
// <copyright file="UsersController.cs" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserApi.Dto;
using UserApi.Helpers;

namespace UserApi.Controllers
{
    using System.Threading.Tasks;
    using Domains;
    using Interfaces;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Rest API User controller
    /// </summary>
    [Route("api/users/")]
    [ApiController]
    public class UsersController : ControllerBase, IUserController
    {
        #region Fields
        private readonly IUserRepository _userRepository;
        private readonly IOptions<AppSettingsHelper> _appSettings;
        #endregion

        #region Constructors
        public UsersController(IUserRepository userRepository, IOptions<AppSettingsHelper> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings;
        }
        #endregion

        #region Methods
        [HttpGet]
        [Route("getAll")]
        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetUsers();
        }

        [HttpPost]
        [Route("create/admin")]
        public async Task<User> CreateAdmin([FromBody] RegisterUserDto userDto)
        {
            return await _userRepository.CreateAdmin(userDto.Username, userDto.Password);
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<ActionResult> Authenticate(RegisterUserDto userDto)
        {
            var entity = await _userRepository.ValidateUser(userDto);

            if (entity == null)
                return BadRequest("Username or password is incorrect");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userDto.Username.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = userDto.Username,
                Token = tokenString
            });
        }

        [HttpPost]
        [Route("create/customer")]
        public Task<User> CreateCustomer(User user)
        {
            throw new System.NotImplementedException();
        }


        [AllowAnonymous]
        [HttpPost("isAuthenticated")]
        public IActionResult IsAuthenticated([FromBody] UserTokenDto token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt = null;

            // Trying to parse the token string
            try
            {
                jwt = tokenHandler.ReadJwtToken(token.TokenString);

            }
            catch (ArgumentException)
            {
                return BadRequest("UnAuthorized");
            }

            // Get user id from token
            var userIdentifier = jwt.Claims.Where(c => c.Type == "unique_name").SingleOrDefault().Value;
            int.TryParse(userIdentifier, out int userId);

            // Get user from database
            //var user = _userRepository.GetById(userId);

            //return Ok(new
            //{
              //  Id = user.Id,
              //  Username = user.Username
            //});

            return null;
        }


        #endregion







    }
}
