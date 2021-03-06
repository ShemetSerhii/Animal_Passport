﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AnimalPassport.BusinessLogic.DataTransferObjects;
using AnimalPassport.BusinessLogic.Interfaces;
using AnimalPassport.WebApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace AnimalPassport.WebApi.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserManager _userManager;

        public AuthService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserModel> AuthenticateAsync(AuthModel model)
        {
            var user = await _userManager.GetAsync(model.Email, model.Password);

            if (user == null)
                return null;

            SetUpToken(user);

            return user;
        }

        public UserModel Authenticate(UserModel model)
        {
            SetUpToken(model);

            return model;
        }

        private void SetUpToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SFSAFFSAFASF424235rewjtsdguxcyvgfdgreugyfd78v6fdgydvydwad7wetfy");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
        }
    }
}