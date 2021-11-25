namespace API.DTOs
{
    public class PrescriptionSummaryDto
    {
        public int Id { get; set; }
        public int PrescMonth { get; set; }
        public int PrescItems { get; set; }
        public int PrescAvgItem { get; set; }
    }
}