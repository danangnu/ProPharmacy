namespace API.Entities
{
    public class Docs
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public FilesVersion Version { get; set; }
        public int FilesVersionId { get; set; }
    }
}