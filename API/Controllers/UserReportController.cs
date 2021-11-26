using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class UserReportController : BaseApiController
  {
    private readonly IUserReportRepository _userReportRepository;
    private readonly IMapper _mapper;
    public UserReportController(IUserReportRepository userReportRepository, IMapper mapper)
    {
      _mapper = mapper;
      _userReportRepository = userReportRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserReport>>> GetFVersion()
    {
        return Ok(await _userReportRepository.GetFVersions());
    }

    [HttpGet("{id}")]
        public async Task<ActionResult<UserReport>> GetUserReport(int id)
        {
            return await _userReportRepository.GetUserReportByIdAsync(id);
        }

    [HttpPost("add-version/{id}")]
    public async Task<ActionResult<FilesVersionDto>> AddVersion(int id, AddFilesVersionDto addFilesVersionDto)
    {
      var userreport = await _userReportRepository.GetUserReportByIdAsync(id);

      var version = new FilesVersion
      {
        VersionName = addFilesVersionDto.VersionName,
        UserReportId = userreport.Id
      };

      userreport.VersionCreated.Add(version);

      if (await _userReportRepository.SaveAllAsync()) return _mapper.Map<FilesVersionDto>(version);

      return BadRequest("Cannot add new report");
    }

    [HttpDelete("{id}")]
      public async Task<ActionResult> DeleteVersion(int id)
      {
          var userreport = await _userReportRepository.GetUserReportByIdAsync(id);

          _userReportRepository.DeleteReport(userreport);

          if (await _userReportRepository.SaveAllAsync()) return Ok();

          return BadRequest("Error Deleting Report");
      }
  }
}