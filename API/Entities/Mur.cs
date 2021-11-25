namespace API.Entities
{
    public class Mur
    {
        public int Id { get; set; }
        public int MurYear { get; set; }
        public int TotalMur { get; set; }
        public FilesVersion Version { get; set; }
        public int FilesVersionId { get; set; }
    }
}