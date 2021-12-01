using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Authorize]
  public class VersetController : BaseApiController
  {
    private readonly IVersionSettingRepository _versionSettingRepository;
    private readonly IMapper _mapper;
    public VersetController(IVersionSettingRepository versionSettingRepository, IMapper mapper)
    {
      _mapper = mapper;
      _versionSettingRepository = versionSettingRepository;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateVerset(int id, UpdateVersionSettingDto updateVersionSettingDto)
    {
        var verset = await _versionSettingRepository.GetVersionSetting(id);

        _mapper.Map(updateVersionSettingDto, verset);

        _versionSettingRepository.Update(verset);

        if (await _versionSettingRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update Version Setting");
    }
  }
}