using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class MurController : BaseApiController
  {
    private readonly IMurRepository _murRepository;
    private readonly IMapper _mapper;
    public MurController(IMurRepository murRepository, IMapper mapper)
    {
      _mapper = mapper;
      _murRepository = murRepository;
    }

    [HttpGet("{year}/{id}")]
    public async Task<MurDto> GetPrescriptionSummary(int year, int id)
    {
      return await _murRepository.GetMurSum(year, id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMur(int id, UpdateMurDto updateMurDto)
    {
      var mur = await _murRepository.GetMur(updateMurDto.MurYear, id);

      _mapper.Map(updateMurDto, mur);

      _murRepository.Update(mur);

      if (await _murRepository.SaveAllAsync()) return NoContent();

      return BadRequest("Failed to update Mur");
    }
  }
}