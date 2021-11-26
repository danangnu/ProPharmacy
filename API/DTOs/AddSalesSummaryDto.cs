namespace API.DTOs
{
    public class AddSalesSummaryDto
    {
        public int SalesYear { get; set; }
        public int ZeroRatedOTCSale { get; set; }
        public int VATExclusiveOTCSale { get; set; }
    }
}