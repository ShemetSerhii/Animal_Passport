using System;
using System.Collections.Generic;

namespace AnimalPassport.Entities.Entities
{
    public class Animal : BaseEntity
    {
        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public User Owner { get; set; }

        public string PicturePath { get; set; }

        public string Kind { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<MedicalOperation> MedicalOperations { get; set; }
    }
}