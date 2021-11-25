namespace API.Entities
{
    public class PrescriptionSummary
    {
        public int Id { get; set; }
        public int PrescMonth { get; set; }
        public int PrescItems { get; set; }
        public int PrescAvgItem { get; set; }
        public FilesVersion Version { get; set; }
        public int FilesVersionId { get; set; }
    }
}