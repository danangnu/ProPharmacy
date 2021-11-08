namespace API.DTOs
{
    public class SchedulePaymentReportDto
    {
        public string Dispensing_Month { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public double NHS_SalesSum { get; set; }
    }
}