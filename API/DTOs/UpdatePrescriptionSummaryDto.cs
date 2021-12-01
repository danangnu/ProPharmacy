namespace API.DTOs
{
    public class UpdatePrescriptionSummaryDto
    {
        public int PrescMonth { get; set; }
        public int PrescItems { get; set; }
        public int PrescAvgItem { get; set; }
    }
}