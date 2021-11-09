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
        public double Total_Costs { get; set; }
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
        public Docs Document { get; set; }
        public int DocsId { get; set; }
    }
}