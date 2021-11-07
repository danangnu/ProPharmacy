using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
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
        public VersionsController(IVersionRepository versionRepository, IMapper mapper)
        {
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

            var path = string.Empty;
            if (file.FileName.EndsWith(".csv"))
            {
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
                                SSP_Fee_Value = rows[70].ToString()
                            };
                            //version.Prescription.Add(prescription);
                        }
                        i++;

                        if (i == 200) break;
                    }
                    await _versionRepository.SaveAllAsync();
                }
            }

            var doc = new Docs
            {
                FileName = file.FileName,
                FileType = file.ContentType,
                FilesVersionId = version.Id
            };

            version.Documents.Add(doc);

            if (await _versionRepository.SaveAllAsync()) return _mapper.Map<DocsDto>(doc);

            return BadRequest("Cannot add new file");
        }
    }
}