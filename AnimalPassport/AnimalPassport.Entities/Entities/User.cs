using System.Collections.Generic;

namespace AnimalPassport.Entities.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<Animal> Animals { get; set; }
    }
}