using System;

namespace AnimalPassport.Entities.Entities
{
    public class Attachment : BaseEntity
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public DateTime CreationDate { get; set; }

        public Guid MedicalOperationId { get; set; }

        public MedicalOperation MedicalOperation { get; set; }
    }
}