using System;

namespace AnimalPassport.BusinessLogic.DataTransferObjects.Animal
{
    public class AnimalInfo : AnimalModel
    {
        public string Kind { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}