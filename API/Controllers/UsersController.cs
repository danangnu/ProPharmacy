using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("add-version")]
        public async Task<ActionResult<FilesVersionDto>> AddVersion(AddFilesVersionDto addFilesVersionDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(addFilesVersionDto.Email);

            var version = new FilesVersion
            {
                VersionName = addFilesVersionDto.VersionName,
                AppUserId = user.Id
            };

            user.VersionCreated.Add(version);

            if (await _userRepository.SaveAllAsync()) return _mapper.Map<FilesVersionDto>(version);

            return BadRequest("Cannot add new version");
        }

        public ActionResult Private()
        {
            return BadRequest("Hello from a private endpoint! You need to be authenticated to see this.");
        }
    }
}