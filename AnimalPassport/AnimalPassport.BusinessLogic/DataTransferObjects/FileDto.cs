namespace AnimalPassport.BusinessLogic.DataTransferObjects
{
    public class FileDto
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}