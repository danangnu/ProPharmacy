namespace API.DTOs
{
    public class SchedulePaymentReportDto
    {
        public int Dispensing_Year { get; set; }
        public int Dispensing_Month { get; set; }
        public double Total_Others { get; set; }
        public double Other_Fee_Medicine_Services { get; set; }
        public double Adv_Others { get; set; }
        public double Enhanced_Services { get; set; }
        public double Total_Charges { get; set; }
    }
}