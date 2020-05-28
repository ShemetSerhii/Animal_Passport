using System;

namespace AnimalPassport.SmartReader.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Token { get; set; }

        public string Role { get; set; }

        public string Username { get; set; }
    }
}