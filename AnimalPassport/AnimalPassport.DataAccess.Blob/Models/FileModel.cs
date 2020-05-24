namespace AnimalPassport.DataAccess.Blob.Models
{
    public class FileModel
    {
        public string FilePath { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}