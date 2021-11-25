namespace API.DTOs
{
    public class AddPrescriptionSummaryDto
    {
        public int PrescMonth { get; set; }
        public int PrescItems { get; set; }
        public int PrescAvgItem { get; set; }
    }
}