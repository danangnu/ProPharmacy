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
                for (int i = 1; i <= PageNum; i++)  
                {
                    if (i == 1)
                    {
                        var text = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i)); 
                        
                        words = text.Split('\n');
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
                                sale = sale.Replace(".", ",");
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
                                total_drugs = total_drugs.Replace(".", ",");
                                total_drug = Math.Round(double.Parse(total_drugs),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of all fees"))
                            {
                                string[] res = line.Split(' ');
                                var total_fee = res[res.Length - 1].TrimStart().Replace(",", "");
                                total_fee = total_fee.Replace(".", ",");
                                total_fees = Math.Round(double.Parse(total_fee),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of drug and appliance costs plus fees"))
                            {
                                string[] res = line.Split(' ');
                                var total_cost = res[res.Length - 1].TrimStart().Replace(",", "");
                                total_cost = total_cost.Replace(".", ",");
                                total_costs_fee = Math.Round(double.Parse(total_cost),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of charges (including fp57 refunds)"))
                            {
                                string[] res = line.Split(' ');
                                var total_charge = res[res.Length - 1].TrimStart().Replace(",", "");
                                total_charge = total_charge.Replace(".", ",");
                                total_charges = Math.Round(double.Parse(total_charge),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of account"))
                            {
                                string[] res = line.Split(' ');
                                var total_accounts = res[res.Length - 1].TrimStart().Replace(",", "");
                                total_accounts = total_accounts.Replace(".", ",");
                                total_account = Math.Round(double.Parse(total_accounts),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("recovery of advance payment"))
                            {
                                if (line.ToLower().StartsWith("recovery of advance payment in respect of a late registered batch"))
                                {
                                    string[] rs = line.Split(' ');
                                    var recover_adv_payment_late_reg = rs[rs.Length - 1].TrimStart().Replace(",", "");
                                    recover_adv_payment_late_reg = recover_adv_payment_late_reg.Replace(".", ",");
                                    recover_adv_payment_late = Math.Round(double.Parse(recover_adv_payment_late_reg),2,MidpointRounding.AwayFromZero);
                                } else {
                                    string[] res = line.Split(' ');
                                    var recovery_adv_payment = res[res.Length - 1].TrimStart().Replace(",", "");
                                    recovery_adv_payment = recovery_adv_payment.Replace(".", ",");
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
                                balance_dues = balance_dues.Replace(".", ",");
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
                                payment_accounts = payment_accounts.Replace(".", ",");
                                payment_account = Math.Round(double.Parse(payment_accounts),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("advance payment in respect of a late registered batch"))
                            {
                                string[] res = line.Split(' ');
                                var adv_payment_late_reg = res[res.Length - 1].TrimStart().Replace(",", "");
                                adv_payment_late_reg = adv_payment_late_reg.Replace(".", ",");
                                adv_payment_late = Math.Round(double.Parse(adv_payment_late_reg),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total amount authorised by nhsbsa"))
                            {
                                string[] res = line.Split(' ');
                                var total_authorise = res[res.Length - 1].TrimStart().Replace(",", "");
                                total_authorise = total_authorise.Replace(".", ",");
                                total_authorised = Math.Round(double.Parse(total_authorise),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total amount authorised by lpp"))
                            {
                                string[] res = line.Split(' ');
                                var total_authorise_lpp = res[res.Length - 1].TrimStart().Replace(",", "");
                                total_authorise_lpp = total_authorise_lpp.Replace(".", ",");
                                total_authorised_lpp = Math.Round(double.Parse(total_authorise_lpp),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of other amounts authorised"))
                            {
                                string[] res = line.Split(' ');
                                var total_others = res[res.Length - 1].TrimStart().Replace(",", "");
                                total_others = total_others.Replace(".", ",");
                                total_other = Math.Round(double.Parse(total_others),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("total of basic prices at standard discount rate"))
                            {
                                string[] res = line.Split(' ');
                                var total_price_standrt_discount = res[res.Length - 1].TrimStart().Replace(",", "");
                                total_price_standrt_discount = total_price_standrt_discount.Replace(".", ",");
                                total_price_standrt_disc = Math.Round(double.Parse(total_price_standrt_discount),2,MidpointRounding.AwayFromZero);
                            }
                            if (line.ToLower().StartsWith("discount"))
                            {
                                string[] res = line.Split(' ');
                                var total_price_standrt_discount = res[res.Length - 1].TrimStart().Replace(",", "");
                                total_price_standrt_discount = total_price_standrt_discount.Replace(".", ",");
                                total_price_standrt_disc = Math.Round(double.Parse(total_price_standrt_discount),2,MidpointRounding.AwayFromZero);
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
                    DocsId = docOut
                };
                document.ScheduleOfPayment.Add(pay);               
            }

            if (await _versionRepository.SaveAllAsync()) return _mapper.Map<DocsDto>(doc);

            return BadRequest("Cannot add new file");
        }
    }
}