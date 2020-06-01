using System;

namespace AnimalPassport.BusinessLogic.DataTransferObjects
{
    public class UserModel : UserInfo
    {
        public string Token { get; set; }

        public string Role { get; set; }
    }
}