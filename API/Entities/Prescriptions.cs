namespace API.Entities
{
    public class Prescriptions
    {
        public int Id { get; set; }
        public string OCS_Code { get; set; }
	    public string Dispensing_Year { get; set; }
		public string Dispensing_Month { get; set; }
	    public string Fragment_Id { get; set; }
	    public string Form_Number { get; set; }
	    public int Item_Number { get; set; }
	    public string Element_Id { get; set; }
	    public string Form_Type { get; set; }
	    public string Prescriber_Code { get; set; }
	    public string Group_Type_Declared { get; set; }
	    public string Charge_Status { get; set; }
	    public string Charges_Payable { get; set; }
	    public string Product_Description { get; set; }
	    public string Snomed_Code { get; set; }
	    public string Quantity { get; set; }
	    public string Unit_Of_Measure { get; set; }
	    public string Pack_Price { get; set; }
	    public string Pack_Size { get; set; }
	    public string Basic_Price { get; set; }
	    public string Payment_for_Consumables { get; set; }
	    public string Payment_for_Containers { get; set; }
	    public string NB { get; set; }
	    public string CC { get; set; }
	    public string DA { get; set; }
	    public string DA_Value { get; set; }
	    public string EX { get; set; }
	    public string EX_Value { get; set; }
	    public string GN { get; set; }
	    public string IP { get; set; }
	    public string IP_Value { get; set; }
	    public string LP { get; set; }
	    public string LP_Value { get; set; }
	    public string MC { get; set; }
	    public string MC_Value { get; set; }
    	public string MF { get; set; }
	    public string MI { get; set; }
	    public string MI_Value { get; set; }
	    public string MP { get; set; }
	    public string MP_Value { get; set; }
	    public string NC { get; set; }
	    public string NC_Value { get; set; }
	    public string ND { get; set; }
	    public string RB { get; set; }
	    public string RB_Value { get; set; }
	    public string SF { get; set; }
	    public string SF_Value { get; set; }
	    public string ZD { get; set; }
	    public string LB { get; set; }
	    public string LC { get; set; }
	    public string LE { get; set; }
	    public string LF { get; set; }
	    public string SDR_Professional_Fee_Value { get; set; }
	    public string SDR_Professional_Fee_Number { get; set; }
	    public string ZDR_Professional_Fee_Value { get; set; }
	    public string ZDR_Professional_Fee_Number { get; set; }
	    public string SP_Unlicensed_Meds_Fee_Value { get; set; }
	    public string ED_Unlicensed_Meds_Fee_Value { get; set; }
	    public string MF_Hosiery_Fee_Value { get; set; }
	    public string MF_Truss_Fee_Value { get; set; }
	    public string MF_Belt_and_Girdle_Fee_Value { get; set; }
	    public string Methadone_Fee_Value { get; set; }
	    public string Methadone_Pckgd_Dose_Fee_Value { get; set; }
	    public string Home_Del_SR_Appl_Add_Fee_Value { get; set; }
	    public string Home_Del_HR_Appl_Add_Fee_Value { get; set; }
	    public string CD_Schedule_2_Fee_Value { get; set; }
	    public string CD_Schedule_3_Fee_Value { get; set; }
	    public string Expensive_Item_Fee_Value { get; set; }
	    public string Stoma_Customisation_Fee_Value { get; set; }
	    public string Dispensing_UID { get; set; }
	    public string NHS_Patient_Number { get; set; }
	    public string SSP_Vat_Value { get; set; }
	    public string SSP_Fee_Value { get; set; }
		public Docs Document { get; set; }
        public int DocsId { get; set; }
    }
}