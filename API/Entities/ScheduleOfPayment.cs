using System;

namespace API.Entities
{
    public class ScheduleOfPayment
    {
        public int Id { get; set; }
        public string OCS_Code { get; set; }
        public DateTime Net_Payment_Made { get; set; }
        public string Dispensing_Month { get; set; }
        public double Net_Payment { get; set; }
        public double Total_Drug { get; set; }
        public double Total_Fees { get; set; }
        public double Total_Costs_wFees { get; set; }
        public double Total_Charges { get; set; }
        public double Total_Account { get; set; }
        public double Recovery_Adv_Payment { get; set; }
        public double Recovery_Adv_Payment_Late_Registered { get; set; }
        public string Balance_Due_Month { get; set; }
        public double Balance_Due { get; set; }
        public int Account_Number { get; set; }
        public int Account_Item { get; set; }
        public double Payment_Account { get; set; }
        public double Adv_Payment_Late { get; set; }
        public double Total_Authorised { get; set; }
        public double Total_Authorised_LPP { get; set; }
        public double Total_Other { get; set; }
        public double Total_Price_Standrt_Disc { get; set; }
        public string Discount_Percent { get; set; }
        public double Discount { get; set; }
        public double Total_Price_Zero_Disc { get; set; }
        public double Sub_Total_Price { get; set; }
        public double Out_Pocket_Expenses { get; set; }
        public double Pay_Consumable { get; set; }
        public double Pay_Consumable_Per_Item { get; set; }
        public double Pay_Container { get; set; }
        public double Total_Costs { get; set; }
        public double Presc_Activity_Pay { get; set; }
        public double Presc_Activity_Pay_Per_Item { get; set; }
        public double Presc_AddFee_2A_Unlicensed_Meds { get; set; }
        public double Presc_AddFee_2B_Appliance_Measure { get; set; }
        public double Presc_AddFee_2B_Appliance_Home { get; set; }
        public double Presc_AddFee_2E_Controlled { get; set; }
        public Docs Document { get; set; }
        public int DocsId { get; set; }
    }
}