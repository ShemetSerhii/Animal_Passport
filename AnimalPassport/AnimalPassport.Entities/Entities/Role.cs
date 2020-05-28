using System.Collections.Generic;

namespace AnimalPassport.Entities.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}