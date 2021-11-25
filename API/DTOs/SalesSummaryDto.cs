namespace API.DTOs
{
    public class SalesSummaryDto
    {
        public int Id { get; set; }
        public int SalesMonth { get; set; }
        public int ZeroRatedOTCSale { get; set; }
        public int VATExclusiveOTCSale { get; set; }
    }
}