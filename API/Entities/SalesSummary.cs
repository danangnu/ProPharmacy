namespace API.Entities
{
    public class SalesSummary
    {
        public int Id { get; set; }
        public int SalesYear { get; set; }
        public int ZeroRatedOTCSale { get; set; }
        public int VATExclusiveOTCSale { get; set; }
        public FilesVersion Version { get; set; }
        public int FilesVersionId { get; set; }
    }
}