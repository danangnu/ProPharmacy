namespace API.DTOs
{
  public class UpdateSalesSummaryDto
  {
    public int SalesYear { get; set; }
    public int ZeroRatedOTCSale { get; set; }
    public int VATExclusiveOTCSale { get; set; }
  }
}