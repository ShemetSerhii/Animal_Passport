using System;

namespace AnimalPassport.BusinessLogic.DataTransferObjects.Animal
{
    public class AnimalModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Picture { get; set; }

        public string PicturePath { get; set; }

    }
}