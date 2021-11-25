using System;

namespace API.Entities
{
    public class PriceListHistory
    {
        public int Id { get; set; }
	    	public int PriceListsID { get; set; }
	    	public string SupplierCode { get; set; }
	    	public string Product { get; set; }
	    	public string Size { get; set; }
	    	public double Price { get; set; }
	    	public string PIP { get; set; }
	    	public string AMPPID { get; set; }
	    	public string VMPPID { get; set; }
	    	public DateTime DateAdd { get; set; } = DateTime.Now;
				public Docs Document { get; set; }
      	public int DocsId { get; set; }
    }
}