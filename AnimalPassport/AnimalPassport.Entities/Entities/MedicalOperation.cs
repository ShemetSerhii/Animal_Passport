using System;
using System.Collections.Generic;

namespace AnimalPassport.Entities.Entities
{
    public class MedicalOperation : BaseEntity
    {
        public string Name { get; set; }

        public Guid AnimalId { get; set; }

        public Animal Animal { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateExpiry { get; set; }

        public IEnumerable<Attachment> Attachments { get; set; }
    }
}