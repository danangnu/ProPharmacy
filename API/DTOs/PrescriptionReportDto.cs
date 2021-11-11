namespace API.DTOs
{
    public class PrescriptionReportDto
    {
        public string Month { get; set; }
        public string Year { get; set; }
        public int AnnualSum { get; set; }
        public int AnnualAvg { get; set; }
    }
}