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
                                Item_Number = rows[4].ToString()
                            };
                            version.Prescription.Add(prescription);
                            await _versionRepository.SaveAllAsync();

                            if (i == 100) break;
                        }
                        i++;
                    }
                }
            }

            var doc = new Docs
            {
                FileName = file.FileName,
                FilePath = file.ContentType,
                FilesVersionId = version.Id
            };

            version.Documents.Add(doc);

            if (await _versionRepository.SaveAllAsync()) return _mapper.Map<DocsDto>(doc);

            return BadRequest("Cannot add new file");
        }
    }
}