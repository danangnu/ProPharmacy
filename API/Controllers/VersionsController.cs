using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class VersionsController : BaseApiController
    {
        private readonly IVersionRepository _versionRepository;
        private readonly IMapper _mapper;
        private readonly IDocRepository _docRepository;
        public VersionsController(IVersionRepository versionRepository, IDocRepository docRepository, IMapper mapper)
        {
            _docRepository = docRepository;
            _mapper = mapper;
            _versionRepository = versionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilesVersion>>> GetFVersion()
        {
            return Ok(await _versionRepository.GetFVersions());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FilesVersion>> GetFVersion(int id)
        {
            return Ok(await _versionRepository.GetVersionByUserIdAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVersion(int id)
        {
            var version = await _versionRepository.GetVersionByUserIdAsync(id);

            _versionRepository.DeleteVersion(version);

            if (await _versionRepository.SaveAllAsync()) return Ok();

            return BadRequest("Error Deleting Version");
        }

        [HttpPost("add-docs/{id}")]
        public async Task<ActionResult<DocsDto>> AddDocs(int id, IFormFile file)
        {
            var version = await _versionRepository.GetVersionByUserIdAsync(id);

            var doc = new Docs
            {
                FileName = file.FileName,
                FileType = file.ContentType,
                FilesVersionId = version.Id
            };

            version.Documents.Add(doc);

            await _versionRepository.SaveAllAsync();
            int docOut = doc.Id;

            var path = string.Empty;
            if (file.FileName.EndsWith(".csv"))
            {
                var document = await _docRepository.GetPresDocByIdAsync(docOut);
                using (var sreader = new StreamReader(file.OpenReadStream()))
                {
                    var i = 0;
                    string[] headers = sreader.ReadLine().Split(',');     //Title
                    while (!sreader.EndOfStream)                          //get all the content in rows 
                    {
                        string[] rows = sreader.ReadLine().Split(',');
                        if (i > 0)
                        {
                            var prescription = new Prescriptions
                            {
                                OCS_Code = rows[0].ToString(),
                                Dispensing_Month = rows[1].ToString(),
                                Fragment_Id = rows[2].ToString(),
                                Form_Number = rows[3].ToString(),
                                Item_Number = int.Parse(rows[4].ToString()),
                                Element_Id = rows[5].ToString(),
                                Form_Type = rows[6].ToString(),
                                Prescriber_Code = rows[7].ToString(),
                                Group_Type_Declared = rows[8].ToString(),
                                Charge_Status = rows[9].ToString(),
                                Charges_Payable = rows[10].ToString(),
                                Product_Description = rows[11].ToString(),
                                Snomed_Code = rows[12].ToString(),
                                Quantity = rows[13].ToString(),
                                Unit_Of_Measure = rows[14].ToString(),
                                Pack_Price = rows[15].ToString(),
                                Pack_Size = rows[16].ToString(),
                                Basic_Price = rows[17].ToString(),
                                Payment_for_Consumables = rows[18].ToString(),
                                Payment_for_Containers = rows[19].ToString(),
                                NB = rows[20].ToString(),
                                CC = rows[21].ToString(),
                                DA = rows[22].ToString(),
                                DA_Value = rows[23].ToString(),
                                EX = rows[24].ToString(),
                                EX_Value = rows[25].ToString(),
                                GN = rows[26].ToString(),
                                IP = rows[27].ToString(),
                                IP_Value = rows[28].ToString(),
                                LP = rows[29].ToString(),
                                LP_Value = rows[30].ToString(),
                                MC = rows[31].ToString(),
                                MC_Value = rows[32].ToString(),
                                MF = rows[33].ToString(),
                                MI = rows[34].ToString(),
                                MI_Value = rows[35].ToString(),
                                MP = rows[36].ToString(),
                                MP_Value = rows[37].ToString(),
                                NC = rows[38].ToString(),
                                NC_Value = rows[39].ToString(),
                                ND = rows[40].ToString(),
                                RB = rows[41].ToString(),
                                RB_Value = rows[42].ToString(),
                                SF = rows[43].ToString(),
                                SF_Value = rows[44].ToString(),
                                ZD = rows[45].ToString(),
                                LB = rows[46].ToString(),
                                LC = rows[47].ToString(),
                                LE = rows[48].ToString(),
                                LF = rows[49].ToString(),
                                SDR_Professional_Fee_Value = rows[50].ToString(),
                                SDR_Professional_Fee_Number = rows[51].ToString(),
                                ZDR_Professional_Fee_Value = rows[52].ToString(),
                                ZDR_Professional_Fee_Number = rows[53].ToString(),
                                SP_Unlicensed_Meds_Fee_Value = rows[54].ToString(),
                                ED_Unlicensed_Meds_Fee_Value = rows[55].ToString(),
                                MF_Hosiery_Fee_Value = rows[56].ToString(),
                                MF_Truss_Fee_Value = rows[57].ToString(),
                                MF_Belt_and_Girdle_Fee_Value = rows[58].ToString(),
                                Methadone_Fee_Value = rows[59].ToString(),
                                Methadone_Pckgd_Dose_Fee_Value = rows[60].ToString(),
                                Home_Del_SR_Appl_Add_Fee_Value = rows[61].ToString(),
                                Home_Del_HR_Appl_Add_Fee_Value = rows[62].ToString(),
                                CD_Schedule_2_Fee_Value = rows[63].ToString(),
                                CD_Schedule_3_Fee_Value = rows[64].ToString(),
                                Expensive_Item_Fee_Value = rows[65].ToString(),
                                Stoma_Customisation_Fee_Value = rows[66].ToString(),
                                Dispensing_UID = rows[67].ToString(),
                                NHS_Patient_Number = rows[68].ToString(),
                                SSP_Vat_Value = rows[69].ToString(),
                                SSP_Fee_Value = rows[70].ToString(),
                                DocsId = docOut
                            };
                            document.Prescriptions.Add(prescription);
                        }
                        i++;

                        //if (i == 5000) break;
                    }
                }
            } else if (file.FileName.EndsWith(".pdf"))
            {
                var document = await _docRepository.GetPayDocByIdAsync(docOut);
                StringBuilder result = new StringBuilder();
                PdfDocument pdfDoc = new PdfDocument(new PdfReader(file.OpenReadStream()));
                int PageNum = pdfDoc.GetNumberOfPages();  
                string[] words;
                double sales = 0;
                string dmonth = string.Empty;
                string ocs_code = string.Empty;
                DateTime net_pay = new DateTime();
                double total_drug = 0;
                double total_fees = 0;
                double total_costs_fee = 0;
                double total_charges = 0;
                double total_account = 0;
                double recover_adv_payment = 0;
                double recover_adv_payment_late = 0;
                string balance_due_month = string.Empty;
                double balance_due = 0;
                int account_number = 0;
                int account_item = 0;
                double payment_account = 0;
                double adv_payment_late = 0;
                double total_authorised = 0;
                double total_authorised_lpp = 0;
                double total_other = 0;
                double total_price_standrt_disc = 0;
                string discount_percent = string.Empty;
                double discount = 0;
                double total_price_zero_disc = 0;
                double sub_total_price = 0;
                double out_pocket_expenses = 0;
                double pay_consumable_per_item = 0;
                double pay_consumable = 0;
                double pay_container = 0;
                double total_costs = 0;
                double presc_activity_pay_per_item = 0;
                double presc_activity_pay = 0;
                double presc_addfee_2a_unlicensed_meds = 0;
                double presc_addfee_2b_appliance_measure = 0;
                double presc_addfee_2b_appliance_home = 0;
                double presc_addfee_2e_controlled = 0;
                double presc_addfee_methadone_pay = 0;
                double presc_addfee_2f_expensive_fees = 0;
                int presc_addfee_2f_expensive_fee_item = 0;
                double presc_addfee_manual_price = 0;
                double transitional_pay = 0;
                double sub_total_presc_fee = 0;
                double other_fee_medicine = 0;
                double other_fee_appliance_patient = 0;
                double other_fee_appliance_premise = 0;
                double other_fee_stoma_custom = 0;
                double other_fee_medicine_service = 0;
                double total_all_fees = 0;
                int charges_collected_excl_hosiery_1_items = 0;
                double charges_collected_excl_hosiery_1_per_item = 0;
                double charges_collected_excl_hosiery_1 = 0;
                int charges_collected_excl_hosiery_2_items = 0;
                double charges_collected_excl_hosiery_2_per_item = 0;
                double charges_collected_excl_hosiery_2 = 0;
                double charges_collected_elastic_hosiery = 0;
                double charges_fp57_refund = 0;
                double local_scheme = 0;
                double lpc_statutory_levy = 0;
                double covid_premises_refrigeration = 0;
                double reimbursement_covid_costs = 0;
                int total_forms_received = 0;
                int total_electronic_prescription_received = 0;
                int total_electronic_prescription_items = 0;
                int items_zero_disc = 0;
                int items_standard_disc = 0;
                int total_paid_fee_items = 0;
                double avg_item_value = 0;
                int referred_back_items = 0;
                int referred_back_forms = 0;
                int medicines_reviews_declared = 0;
                int ytd_mur_declaration = 0;
                int fp57_declared = 0;
                int appliance_reviews_carried_patients_home = 0;
                int appliance_reviews_carried_premises = 0;
                int new_medicine_service_undertaken = 0;
                int new_medicine_service_items = 0;
                int exempt_chargeable = 0;
                int exempt_chargeable_old_rate = 0;
                int chargeable_exempt = 0;
                int chargeable_old_rate_exempt = 0;
                int items_over_100 = 0;
                double items_over_100_basic_price = 0;
                int items_over_300 = 0;
                double items_over_300_basic_price = 0;
                int total_items_over_100 = 0;
                double total_items_over_100_basic_price = 0;
                
                for (int i = 1; i <= PageNum; i++)  
                {
                    var text = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i)); 
                        
                    words = text.Split('\n');

                    if (i == 1)
                    {
                        for (int j = 0, len = words.Length; j < len; j++)
                        {
                            var line = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(words[j]));     
                            if (line.ToLower().StartsWith("ocs code"))
                            {
                                string[] res = line.Split(' ');
                                var ocs = res[res.Length - 1].TrimStart();
                                ocs_code = ocs;
                            }                     
                            if (line.ToLower().StartsWith("net payment made"))
                            {
                                Regex regex = new Regex(@"\d{2} (Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\ \d{4}");
                                Match m = regex.Match(line);
                                if(m.Success)
                                {
                                    DateTime dt = DateTime.ParseExact(m.Value, "dd MMM yyyy", CultureInfo.InvariantCulture);
                                    net_pay = dt;
                                }
                                string[] res = line.Split(' ');
                                var sale = res[res.Length - 1].TrimStart().Replace(",", "");
                                //sale = sale.Replace(".", ",");
                                sales = Math.Round(double.Parse(sale),2,MidpointRounding.AwayFromZero);
                            }

                            if (line.ToLower().StartsWith("dispensing month"))
                            {
                                string[] res = line.Split(' ');
                                var m = DateTime.ParseExact(res[res.Length - 2].TrimStart(), "MMM", CultureInfo.CurrentCulture ).Month;
                                if (m < 10) 
                                    dmonth = res[res.Length - 1].TrimStart() + "0" + m.ToString();
                                else dmonth = res[res.Length - 1].TrimStart() + m.ToString();
                            }
                            if (line.ToLower().StartsWith("total of drug and appliance costs"))
                            {
                                string[] res = line.Split(' ');
                                var total_drugs = res[res.Length - 1].TrimStart().Replace(",", "");
                                //total_drugs = total_drugs.Replace(".", ",");
                                total_drug = Math.Round(double.Parse(total_drugs),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of all fees"))
                            {
                                string[] res = line.Split(' ');
                                var total_fee = res[res.Length - 1].TrimStart().Replace(",", "");
                                //total_fee = total_fee.Replace(".", ",");
                                total_fees = Math.Round(double.Parse(total_fee),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of drug and appliance costs plus fees"))
                            {
                                string[] res = line.Split(' ');
                                var total_cost = res[res.Length - 1].TrimStart().Replace(",", "");
                                //total_cost = total_cost.Replace(".", ",");
                                total_costs_fee = Math.Round(double.Parse(total_cost),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of charges (including fp57 refunds)"))
                            {
                                string[] res = line.Split(' ');
                                var total_charge = res[res.Length - 1].TrimStart().Replace(",", "");
                                //total_charge = total_charge.Replace(".", ",");
                                total_charges = Math.Round(double.Parse(total_charge),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of account"))
                            {
                                string[] res = line.Split(' ');
                                var total_accounts = res[res.Length - 1].TrimStart().Replace(",", "");
                                //total_accounts = total_accounts.Replace(".", ",");
                                total_account = Math.Round(double.Parse(total_accounts),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("recovery of advance payment"))
                            {
                                if (line.ToLower().StartsWith("recovery of advance payment in respect of a late registered batch"))
                                {
                                    string[] rs = line.Split(' ');
                                    var recover_adv_payment_late_reg = rs[rs.Length - 1].TrimStart().Replace(",", "");
                                    //recover_adv_payment_late_reg = recover_adv_payment_late_reg.Replace(".", ",");
                                    recover_adv_payment_late = Math.Round(double.Parse(recover_adv_payment_late_reg),2,MidpointRounding.AwayFromZero);
                                } else {
                                    string[] res = line.Split(' ');
                                    var recovery_adv_payment = res[res.Length - 1].TrimStart().Replace(",", "");
                                    //recovery_adv_payment = recovery_adv_payment.Replace(".", ",");
                                    recover_adv_payment = Math.Round(double.Parse(recovery_adv_payment),2,MidpointRounding.AwayFromZero);
                                }
                            }
                            if (line.ToLower().StartsWith("balance due in respect of"))
                            {
                                Regex regex = new Regex(@"(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\ \d{4}");
                                Match m = regex.Match(line);
                                if(m.Success)
                                {
                                    string[] res = m.Value.Split(' ');
                                    var due = DateTime.ParseExact(res[0], "MMM", CultureInfo.CurrentCulture ).Month;
                                    if (due < 10) 
                                        balance_due_month = res[1].TrimStart() + "0" + due.ToString();
                                    else balance_due_month = res[1].TrimStart() + due.ToString();
                                }
                                string[] rs = line.Split(' ');
                                var balance_dues = rs[rs.Length - 1].TrimStart().Replace(",", "");
                                //balance_dues = balance_dues.Replace(".", ",");
                                balance_due = Math.Round(double.Parse(balance_dues),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("payment on account for"))
                            {
                                string output = line.Split('(', ')')[1];
                                string[] rs = output.Split(' ');
                                for (var k = 0; k < rs.Length - 1;k++)
                                {
                                    int n;
                                    if (int.TryParse(rs[k], out n))
                                    {
                                        if (k == 1)
                                            account_number = int.Parse(rs[k]);
                                        else
                                            account_item = int.Parse(rs[k]);
                                    }
                                }
                                string[] res = line.Split(' ');
                                var payment_accounts = res[res.Length - 1].TrimStart().Replace(",", "");
                                //payment_accounts = payment_accounts.Replace(".", ",");
                                payment_account = Math.Round(double.Parse(payment_accounts),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("advance payment in respect of a late registered batch"))
                            {
                                string[] res = line.Split(' ');
                                var adv_payment_late_reg = res[res.Length - 1].TrimStart().Replace(",", "");
                                //adv_payment_late_reg = adv_payment_late_reg.Replace(".", ",");
                                adv_payment_late = Math.Round(double.Parse(adv_payment_late_reg),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total amount authorised by nhsbsa"))
                            {
                                string[] res = line.Split(' ');
                                var total_authorise = res[res.Length - 1].TrimStart().Replace(",", "");
                                //total_authorise = total_authorise.Replace(".", ",");
                                total_authorised = Math.Round(double.Parse(total_authorise),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total amount authorised by lpp"))
                            {
                                string[] res = line.Split(' ');
                                var total_authorise_lpp = res[res.Length - 1].TrimStart().Replace(",", "");
                                //total_authorise_lpp = total_authorise_lpp.Replace(".", ",");
                                total_authorised_lpp = Math.Round(double.Parse(total_authorise_lpp),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of other amounts authorised"))
                            {
                                string[] res = line.Split(' ');
                                var total_others = res[res.Length - 1].TrimStart().Replace(",", "");
                                //total_others = total_others.Replace(".", ",");
                                total_other = Math.Round(double.Parse(total_others),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of basic prices at standard discount rate"))
                            {
                                string[] res = line.Split(' ');
                                var total_price_standrt_discount = res[res.Length - 1].TrimStart().Replace(",", "");
                                //total_price_standrt_discount = total_price_standrt_discount.Replace(".", ",");
                                total_price_standrt_disc = Math.Round(double.Parse(total_price_standrt_discount),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("discount"))
                            {
                                string[] res = line.Split(' ');
                                for (var k = 1; k < res.Length; k++)
                                {
                                    if (res[k].Equals("%"))
                                        discount_percent = res[k-1].TrimStart().TrimEnd() + " %";
                                }
                                var discounts = res[res.Length - 1].TrimStart().Replace(",", "");
                                //discounts = discounts.Replace(".", ",");
                                discount = Math.Round(double.Parse(discounts),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of basic prices at zero discount"))
                            {
                                string[] res = line.Split(' ');
                                var total_price_zero_discount = res[res.Length - 1].TrimStart().Replace(",", "");
                                //total_price_zero_discount = total_price_zero_discount.Replace(".", ",");
                                total_price_zero_disc = Math.Round(double.Parse(total_price_zero_discount),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("sub total of basic prices"))
                            {
                                string[] res = line.Split(' ');
                                var sub_total_prices = res[res.Length - 1].TrimStart().Replace(",", "");
                                //sub_total_prices = sub_total_prices.Replace(".", ",");
                                sub_total_price = Math.Round(double.Parse(sub_total_prices),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("out of pocket expenses"))
                            {
                                string[] res = line.Split(' ');
                                var out_pocket_expense = res[res.Length - 1].TrimStart().Replace(",", "");
                                //out_pocket_expense = out_pocket_expense.Replace(".", ",");
                                out_pocket_expenses = Math.Round(double.Parse(out_pocket_expense),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("payment for consumables"))
                            {
                                string[] res = line.Split(' ');
                                for (var k = 1; k < res.Length; k++)
                                {
                                    if (res[k].Equals("p"))
                                    {
                                        var pay_consumable_per_items = res[k-1].TrimStart().TrimEnd();
                                        //pay_consumable_per_items = pay_consumable_per_items.Replace(".", ",");
                                        pay_consumable_per_item = Math.Round(double.Parse(pay_consumable_per_items),2,MidpointRounding.AwayFromZero);
                                    }
                                        
                                }
                                var pay_consumables = res[res.Length - 1].TrimStart().Replace(",", "");
                                //pay_consumables = pay_consumables.Replace(".", ",");
                                pay_consumable = Math.Round(double.Parse(pay_consumables),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("payment for containers"))
                            {
                                string[] res = line.Split(' ');
                                var pay_containers = res[res.Length - 1].TrimStart().Replace(",", "");
                                //pay_containers = pay_containers.Replace(".", ",");
                                pay_container = Math.Round(double.Parse(pay_containers),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of drug and appliance costs"))
                            {
                                string[] res = line.Split(' ');
                                var total_cost = res[res.Length - 1].TrimStart().Replace(",", "");
                                //total_cost = total_cost.Replace(".", ",");
                                total_costs = Math.Round(double.Parse(total_cost),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("activity payment"))
                            {
                                string[] res = line.Split(' ');
                                for (var k = 1; k < res.Length; k++)
                                {
                                    if (res[k].Equals("p"))
                                    {
                                        var presc_activity_pay_per_items = res[k-1].TrimStart().TrimEnd();
                                        //presc_activity_pay_per_items = presc_activity_pay_per_items.Replace(".", ",");
                                        presc_activity_pay_per_item = Math.Round(double.Parse(presc_activity_pay_per_items),2,MidpointRounding.AwayFromZero);
                                    }
                                        
                                }
                                var presc_activity_payment = res[res.Length - 1].TrimStart().Replace(",", "");
                                //presc_activity_payment = presc_activity_payment.Replace(".", ",");
                                presc_activity_pay = Math.Round(double.Parse(presc_activity_payment),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("additional fees") && line.ToLower().Contains("2a unlicensed medicines"))
                            {
                                string[] res = line.Split(' ');
                                var presc_addfee_2a_unlicensed_med = res[res.Length - 1].TrimStart().Replace(",", "");
                                //presc_addfee_2a_unlicensed_med = presc_addfee_2a_unlicensed_med.Replace(".", ",");
                                presc_addfee_2a_unlicensed_meds = Math.Round(double.Parse(presc_addfee_2a_unlicensed_med),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().Contains("2b appliances - measured and fitted"))
                            {
                                string[] res = line.Split(' ');
                                var presc_addfee_2b_appliance_measured = res[res.Length - 1].TrimStart().Replace(",", "");
                                //presc_addfee_2b_appliance_measured = presc_addfee_2b_appliance_measured.Replace(".", ",");
                                presc_addfee_2b_appliance_measure = Math.Round(double.Parse(presc_addfee_2b_appliance_measured),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().Contains("appliances - home delivery"))
                            {
                                string[] res = line.Split(' ');
                                var presc_addfee_2b_appliance_home_d = res[res.Length - 1].TrimStart().Replace(",", "");
                                //presc_addfee_2b_appliance_home_d = presc_addfee_2b_appliance_home_d.Replace(".", ",");
                                presc_addfee_2b_appliance_home = Math.Round(double.Parse(presc_addfee_2b_appliance_home_d),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().Contains("2E controlled drug schedules 2 and 3"))
                            {
                                string[] res = line.Split(' ');
                                var presc_addfee_2e_controlled_drug = res[res.Length - 1].TrimStart().Replace(",", "");
                                //presc_addfee_2e_controlled_drug = presc_addfee_2e_controlled_drug.Replace(".", ",");
                                presc_addfee_2e_controlled = Math.Round(double.Parse(presc_addfee_2e_controlled_drug),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().Contains("methadone payment"))
                            {
                                string[] res = line.Split(' ');
                                var presc_addfee_methadone_payment = res[res.Length - 1].TrimStart().Replace(",", "");
                                //presc_addfee_methadone_payment = presc_addfee_methadone_payment.Replace(".", ",");
                                presc_addfee_methadone_pay = Math.Round(double.Parse(presc_addfee_methadone_payment),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().Contains("2f expensive prescription fees"))
                            {
                                string[] res = line.Split(' ');
                                presc_addfee_2f_expensive_fee_item = int.Parse(res[res.Length - 2].TrimStart().TrimEnd());
                                var presc_addfee_2f_expensive_fee = res[res.Length - 1].TrimStart().Replace(",", "");
                                //presc_addfee_2f_expensive_fee = presc_addfee_2f_expensive_fee.Replace(".", ",");
                                presc_addfee_2f_expensive_fees = Math.Round(double.Parse(presc_addfee_2f_expensive_fee),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().Contains("manually priced"))
                            {
                                string[] res = line.Split(' ');
                                var presc_addfee_manual_priced = res[res.Length - 1].TrimStart().Replace(",", "");
                                //presc_addfee_manual_priced = presc_addfee_manual_priced.Replace(".", ",");
                                presc_addfee_manual_price = Math.Round(double.Parse(presc_addfee_manual_priced),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("transitional Payment"))
                            {
                                string[] res = line.Split(' ');
                                var transitional_payment = res[res.Length - 1].TrimStart().Replace(",", "");
                                //transitional_payment = transitional_payment.Replace(".", ",");
                                transitional_pay = Math.Round(double.Parse(transitional_payment),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("sub total of prescription fees"))
                            {
                                string[] res = line.Split(' ');
                                var sub_total_presc_fees = res[res.Length - 1].TrimStart().Replace(",", "");
                                //sub_total_presc_fees = sub_total_presc_fees.Replace(".", ",");
                                sub_total_presc_fee = Math.Round(double.Parse(sub_total_presc_fees),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("other fees") && line.ToLower().Contains("medicines use reviews"))
                            {
                                string[] res = line.Split(' ');
                                var other_fee_medicines = res[res.Length - 1].TrimStart().Replace(",", "");
                                //other_fee_medicines = other_fee_medicines.Replace(".", ",");
                                other_fee_medicine = Math.Round(double.Parse(other_fee_medicines),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().Contains("appliance use reviews carried out at patients home"))
                            {
                                string[] res = line.Split(' ');
                                var other_fee_appliance_patients = res[res.Length - 1].TrimStart().Replace(",", "");
                                //other_fee_appliance_patients = other_fee_appliance_patients.Replace(".", ",");
                                other_fee_appliance_patient = Math.Round(double.Parse(other_fee_appliance_patients),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().Contains("appliance use reviews carried out at premises"))
                            {
                                string[] res = line.Split(' ');
                                var other_fee_appliance_premises = res[res.Length - 1].TrimStart().Replace(",", "");
                                //other_fee_appliance_premises = other_fee_appliance_premises.Replace(".", ",");
                                other_fee_appliance_premise = Math.Round(double.Parse(other_fee_appliance_premises),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().Contains("stoma customisation"))
                            {
                                string[] res = line.Split(' ');
                                var other_fee_stoma_customi = res[res.Length - 1].TrimStart().Replace(",", "");
                                //other_fee_stoma_customi = other_fee_stoma_customi.Replace(".", ",");
                                other_fee_stoma_custom = Math.Round(double.Parse(other_fee_stoma_customi),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().Contains("new medicine service"))
                            {
                                string[] res = line.Split(' ');
                                var other_fee_medicine_services = res[res.Length - 1].TrimStart().Replace(",", "");
                                //other_fee_medicine_services = other_fee_medicine_services.Replace(".", ",");
                                other_fee_medicine_service = Math.Round(double.Parse(other_fee_medicine_services),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of all fees"))
                            {
                                string[] res = line.Split(' ');
                                var total_all_fee = res[res.Length - 1].TrimStart().Replace(",", "");
                                //total_all_fee = total_all_fee.Replace(".", ",");
                                total_all_fees = Math.Round(double.Parse(total_all_fee),2,MidpointRounding.AwayFromZero);
                            }
                        }
                    } else if (i == 2) {
                        for (int j = 0, len = words.Length; j < len; j++)
                        {
                            var line = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(words[j]));
                            if (line.ToLower().StartsWith("collected excluding elastic hosiery"))
                            {
                                string[] res = line.Split(' ');
                                for (int k = 0; k < res.Length; k++)
                                {
                                    if (res[k].Equals("@"))
                                    {
                                        charges_collected_excl_hosiery_1_items = int.Parse(res[k-1].TrimStart().TrimEnd());
                                        charges_collected_excl_hosiery_1_per_item = Math.Round(double.Parse(res[k+1].TrimStart().TrimEnd()),2,MidpointRounding.AwayFromZero);
                                    }
                                }
                                var charges_collected_exclu_hosiery_1 = res[res.Length - 1].TrimStart().Replace(",", "");
                                //charges_collected_exclu_hosiery_1 = charges_collected_exclu_hosiery_1.Replace(".", ",");
                                charges_collected_excl_hosiery_1 = Math.Round(double.Parse(charges_collected_exclu_hosiery_1),2,MidpointRounding.AwayFromZero);
                                var linenext = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(words[j+1]));
                                
                                string[] res2 = linenext.Split(' ');
                                for (int k = 0; k < res2.Length; k++)
                                {
                                    if (res2[k].Equals("@"))
                                    {
                                        charges_collected_excl_hosiery_2_items = int.Parse(res2[k-1].TrimStart().TrimEnd());
                                        charges_collected_excl_hosiery_2_per_item = Math.Round(double.Parse(res2[k+1].TrimStart().TrimEnd()),2,MidpointRounding.AwayFromZero);
                                    }
                                }
                                var charges_collected_exclu_hosiery_2 = res2[res2.Length - 1].TrimStart().Replace(",", "");
                                //charges_collected_exclu_hosiery_2 = charges_collected_exclu_hosiery_2.Replace(".", ",");
                                charges_collected_excl_hosiery_2 = Math.Round(double.Parse(charges_collected_exclu_hosiery_2),2,MidpointRounding.AwayFromZero);
                                j++;
                            }
                            if (line.ToLower().StartsWith("collected elastic hosiery"))
                            {
                                string[] res = line.Split(' ');
                                var charges_collected_elastic_hosier = res[res.Length - 1].TrimStart().Replace(",", "");
                                //charges_collected_elastic_hosier = charges_collected_elastic_hosier.Replace(".", ",");
                                charges_collected_elastic_hosiery = Math.Round(double.Parse(charges_collected_elastic_hosier),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("fp57 refunds"))
                            {
                                string[] res = line.Split(' ');
                                var charges_fp57_refunds = res[res.Length - 1].TrimStart().Replace(",", "");
                                //charges_fp57_refunds = charges_fp57_refunds.Replace(".", ",");
                                charges_fp57_refund = Math.Round(double.Parse(charges_fp57_refunds),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("local scheme 1"))
                            {
                                string[] res = line.Split(' ');
                                var local_scheme1 = res[res.Length - 1].TrimStart().Replace(",", "");
                                //local_scheme1 = local_scheme1.Replace(".", ",");
                                local_scheme = Math.Round(double.Parse(local_scheme1),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("lpc - statutory levy"))
                            {
                                string[] res = line.Split(' ');
                                var lpc_statutory_levys = res[res.Length - 1].TrimStart().Replace(",", "");
                                //lpc_statutory_levys = lpc_statutory_levys.Replace(".", ",");
                                lpc_statutory_levy = Math.Round(double.Parse(lpc_statutory_levys),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("covid premises and refrigeration"))
                            {
                                string[] res = line.Split(' ');
                                var covid_premise_refrigeration = res[res.Length - 1].TrimStart().Replace(",", "");
                                //covid_premise_refrigeration = covid_premise_refrigeration.Replace(".", ",");
                                covid_premises_refrigeration = Math.Round(double.Parse(covid_premise_refrigeration),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("reimbursement of covid-19 costs"))
                            {
                                string[] res = line.Split(' ');
                                var reimbursement_covid_cost = res[res.Length - 1].TrimStart().Replace(",", "");
                                //reimbursement_covid_cost = reimbursement_covid_cost.Replace(".", ",");
                                reimbursement_covid_costs = Math.Round(double.Parse(reimbursement_covid_cost),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total forms received (including electronic prescriptions)"))
                            {
                                string[] res = line.Split(' ');                                
                                total_forms_received = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("total electronic prescription forms received"))
                            {
                                string[] res = line.Split(' ');                                
                                total_electronic_prescription_received = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("total electronic prescription items received"))
                            {
                                string[] res = line.Split(' ');                                
                                total_electronic_prescription_items = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("items at zero discount rate, for which a fee is paid"))
                            {
                                string[] res = line.Split(' ');                                
                                items_zero_disc = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("items at standard discount rate, for which a fee is paid (including oxygen)"))
                            {
                                string[] res = line.Split(' ');                                
                                items_standard_disc = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("total of items, for which a fee is paid"))
                            {
                                string[] res = line.Split(' ');                                
                                total_paid_fee_items = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("average item value"))
                            {
                                string[] res = line.Split(' ');
                                var avge_item_value = res[res.Length - 1].TrimStart().Replace(",", "");
                                //avge_item_value = avge_item_value.Replace(".", ",");
                                avg_item_value = Math.Round(double.Parse(avge_item_value),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("referred back items"))
                            {
                                string[] res = line.Split(' ');                                
                                referred_back_items = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("referred back forms"))
                            {
                                string[] res = line.Split(' ');                                
                                referred_back_forms = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("medicines use reviews declared"))
                            {
                                string[] res = line.Split(' ');                                
                                medicines_reviews_declared = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("year to date mur declarations"))
                            {
                                string[] res = line.Split(' ');                                
                                ytd_mur_declaration = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("fp57 forms declared"))
                            {
                                string[] res = line.Split(' ');                                
                                fp57_declared = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("appliance use reviews carried out at patients home declared"))
                            {
                                string[] res = line.Split(' ');                                
                                appliance_reviews_carried_patients_home = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("appliance use reviews carried out at premises declared"))
                            {
                                string[] res = line.Split(' ');                                
                                appliance_reviews_carried_premises = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("new medicine services undertaken"))
                            {
                                string[] res = line.Split(' ');                                
                                new_medicine_service_undertaken = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("new medicine service items"))
                            {
                                string[] res = line.Split(' ');                                
                                new_medicine_service_items = int.Parse(res[res.Length - 1].TrimStart());
                            }
                        }
                    } else if (i == 3) {
                        for (int j = 0, len = words.Length; j < len; j++)
                        {
                            var line = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(words[j]));
                            if (line.ToLower().StartsWith("exempt to chargeable"))
                            {
                                string[] res = line.Split(' ');                                
                                exempt_chargeable = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("exempt to chargeable (old rate)"))
                            {
                                string[] res = line.Split(' ');                                
                                exempt_chargeable_old_rate = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("chargeable to exempt"))
                            {
                                string[] res = line.Split(' ');                                
                                chargeable_exempt = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("chargeable (old rate) to exempt"))
                            {
                                string[] res = line.Split(' ');                                
                                chargeable_old_rate_exempt = int.Parse(res[res.Length - 1].TrimStart());
                            }
                            if (line.ToLower().StartsWith("number of items over £ 100 and up to £ 300"))
                            {
                                string[] res = line.Split(' ');
                                items_over_100 = int.Parse(res[res.Length - 2].TrimStart());
                                var items_over_100_basic_prices = res[res.Length - 1].TrimStart().Replace(",", "");
                                //items_over_100_basic_prices = items_over_100_basic_prices.Replace(".", ",");
                                items_over_100_basic_price = Math.Round(double.Parse(items_over_100_basic_prices),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("number of items over £ 300"))
                            {
                                string[] res = line.Split(' ');
                                items_over_300 = int.Parse(res[res.Length - 2].TrimStart());
                                var items_over_300_basic_prices = res[res.Length - 1].TrimStart().Replace(",", "");
                                //items_over_300_basic_prices = items_over_300_basic_prices.Replace(".", ",");
                                items_over_300_basic_price = Math.Round(double.Parse(items_over_300_basic_prices),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of items over £ 100"))
                            {
                                string[] res = line.Split(' ');
                                total_items_over_100 = int.Parse(res[res.Length - 2].TrimStart());
                                var total_items_over_100_basic_prices = res[res.Length - 1].TrimStart().Replace(",", "");
                                //total_items_over_100_basic_prices = total_items_over_100_basic_prices.Replace(".", ",");
                                total_items_over_100_basic_price = Math.Round(double.Parse(total_items_over_100_basic_prices),2,MidpointRounding.AwayFromZero);
                            }
                        }
                        
                    }   
                }
                var pay = new ScheduleOfPayment
                {
                    OCS_Code = ocs_code,
                    Net_Payment_Made = net_pay,
                    Dispensing_Month = dmonth,
                    Net_Payment = sales,
                    Total_Drug = total_drug,
                    Total_Fees = total_fees,
                    Total_Costs_wFees = total_costs_fee,
                    Total_Charges = total_charges,
                    Total_Account = total_account,
                    Recovery_Adv_Payment = recover_adv_payment,
                    Recovery_Adv_Payment_Late_Registered = recover_adv_payment_late,
                    Balance_Due_Month = balance_due_month,
                    Balance_Due = balance_due,
                    Account_Number = account_number,
                    Account_Item = account_item,
                    Payment_Account = payment_account,
                    Adv_Payment_Late = adv_payment_late,
                    Total_Authorised = total_authorised,
                    Total_Authorised_LPP = total_authorised_lpp,
                    Total_Other = total_other,
                    Total_Price_Standrt_Disc = total_price_standrt_disc,
                    Discount_Percent = discount_percent,
                    Discount = discount,
                    Total_Price_Zero_Disc = total_price_zero_disc,
                    Sub_Total_Price = sub_total_price,
                    Out_Pocket_Expenses = out_pocket_expenses,
                    Pay_Consumable_Per_Item = pay_consumable_per_item,
                    Pay_Consumable = pay_consumable,
                    Pay_Container = pay_container,
                    Total_Costs = total_costs,
                    Presc_Activity_Pay_Per_Item = presc_activity_pay_per_item,
                    Presc_Activity_Pay = presc_activity_pay,
                    Presc_AddFee_2A_Unlicensed_Meds = presc_addfee_2a_unlicensed_meds,
                    Presc_AddFee_2B_Appliance_Measure = presc_addfee_2b_appliance_measure,
                    Presc_AddFee_2B_Appliance_Home = presc_addfee_2b_appliance_home,
                    Presc_AddFee_2E_Controlled = presc_addfee_2e_controlled,
                    Presc_AddFee_Methadone_Pay = presc_addfee_methadone_pay,
                    Presc_AddFee_2F_Expensive_Fee_Item = presc_addfee_2f_expensive_fee_item,
                    Presc_AddFee_2F_Expensive_Fees = presc_addfee_2f_expensive_fees,
                    Presc_AddFee_Manual_Price = presc_addfee_manual_price,
                    Transitional_Pay = transitional_pay,
                    Sub_Total_Presc_Fee = sub_total_presc_fee,
                    Other_Fee_Medicine = other_fee_medicine,
                    Other_Fee_Appliance_Patient = other_fee_appliance_patient,
                    Other_Fee_Appliance_Premise = other_fee_appliance_premise,
                    Other_Fee_Stoma_Custom = other_fee_stoma_custom,
                    Other_Fee_Medicine_Service = other_fee_medicine_service,
                    Total_All_Fees = total_all_fees,
                    Charges_Collected_Excl_Hosiery_1_Items = charges_collected_excl_hosiery_1_items,
                    Charges_Collected_Excl_Hosiery_1_Per_Item = charges_collected_excl_hosiery_1_per_item,
                    Charges_Collected_Excl_Hosiery_1 = charges_collected_excl_hosiery_1,
                    Charges_Collected_Excl_Hosiery_2_Items = charges_collected_excl_hosiery_2_items,
                    Charges_Collected_Excl_Hosiery_2_Per_Item = charges_collected_excl_hosiery_2_per_item,
                    Charges_Collected_Excl_Hosiery_2 = charges_collected_excl_hosiery_2,
                    Charges_Collected_Elastic_Hosiery = charges_collected_elastic_hosiery,
                    Charges_FP57_Refund = charges_fp57_refund,
                    Local_Scheme = local_scheme,
                    LPC_Statutory_Levy = lpc_statutory_levy,
                    COVID_Premises_Refrigeration = covid_premises_refrigeration,
                    Reimbursement_Covid_Costs = reimbursement_covid_costs,
                    Total_Forms_Received = total_forms_received,
                    Total_Electronic_Prescription_Received = total_electronic_prescription_received,
                    Total_Electronic_Prescription_Items = total_electronic_prescription_items,
                    Items_Zero_Disc = items_zero_disc,
                    Items_Standard_Disc = items_standard_disc,
                    Total_Paid_Fee_Items = total_paid_fee_items,
                    Avg_Item_Value = avg_item_value,
                    Referred_Back_Items = referred_back_items,
                    Referred_Back_Forms = referred_back_forms,
                    Medicines_Reviews_Declared = medicines_reviews_declared,
                    YTD_MUR_Declaration = ytd_mur_declaration,
                    FP57_Declared = fp57_declared,
                    Appliance_Reviews_Carried_Patients_Home = appliance_reviews_carried_patients_home,
                    Appliance_Reviews_Carried_Premises = appliance_reviews_carried_premises,
                    New_Medicine_Service_Undertaken = new_medicine_service_undertaken,
                    New_Medicine_Service_Items = new_medicine_service_items,
                    Exempt_Chargeable = exempt_chargeable,
                    Exempt_Chargeable_Old_Rate = exempt_chargeable_old_rate,
                    Chargeable_Exempt = chargeable_exempt,
                    Chargeable_Old_Rate_Exempt = chargeable_old_rate_exempt,
                    Items_Over_100 = items_over_100,
                    Items_Over_100_Basic_Price = items_over_100_basic_price,
                    Items_Over_300 = items_over_300,
                    Items_Over_300_Basic_Price = items_over_300_basic_price,
                    Total_Items_Over_100 = total_items_over_100,
                    Total_Items_Over_100_Basic_Price = total_items_over_100_basic_price,
                    DocsId = docOut
                };
                document.ScheduleOfPayment.Add(pay);               
            }

            if (await _versionRepository.SaveAllAsync()) return _mapper.Map<DocsDto>(doc);

            return BadRequest("Cannot add new file");
        }
    }
}