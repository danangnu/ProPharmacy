using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IVersionRepository _versionRepository;

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper, IVersionRepository versionRepository)
        {
            _versionRepository = versionRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();

            return Ok(users);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<MemberDto>> GetUser(string email)
        {
            return await _userRepository.GetMemberAsync(email);
        }

        [HttpPost("add-report")]
        public async Task<ActionResult<UserReportDto>> AddVersion(AddUserReportDto addUserReportDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(addUserReportDto.Email);

            var userreport = new UserReport
            {
                ReportName = addUserReportDto.ReportName,
                AppUserId = user.Id
            };

            user.ReportCreated.Add(userreport);

            if (await _userRepository.SaveAllAsync()) return _mapper.Map<UserReportDto>(userreport);

            return BadRequest("Cannot add new report");
        }

        public ActionResult Private()
        {
            return BadRequest("Hello from a private endpoint! You need to be authenticated to see this.");
        }
    }
}