using System;

namespace AnimalPassport.BusinessLogic.DataTransferObjects
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string Token { get; set; }

        public string Role { get; set; }

        public string Username { get; set; }
    }
}