namespace API.Entities
{
    public class ScheduleOfPayment
    {
        public int Id { get; set; }
        public string Dispensing_Month { get; set; }
        public double NHS_Sales { get; set; }
        public Docs Document { get; set; }
        public int DocsId { get; set; }
    }
}