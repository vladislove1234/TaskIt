using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Taskit_server.Model.Entities;
using Taskit_server.Model.Entities.RoleModels;
using Taskit_server.Model.Entities.TeamModels;
using Taskit_server.Model.Entities.UserModels;
using Taskit_server.Model.Helpers;
using Taskit_server.Services.Interfaces;

namespace Taskit_server.Controllers
{
    [ApiController]
    [Route("api/team")]
    public class TeamController : Controller
    {
        private readonly IRepository<Team> _teamRepository;
        private readonly IUserService _userService;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<TeamMember> _teamMemberRepository;
        private readonly IMapper _mapper;
        public TeamController(IRepository<Team> teamRepository,
            IUserService userService,
            IRepository<Role> roleRepository,
            IRepository<TeamMember> teamMemberRepository,
            IMapper mapper)
        {
            _teamRepository = teamRepository;
            _userService = userService;
            _roleRepository = roleRepository;
            _teamMemberRepository = teamMemberRepository;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        [Route("getTeams")]
        public IActionResult GetTeams()
        {
            var author = (User)HttpContext.Items["User"];
            var teams = new List<TeamInfo>();
            foreach (var team in author.Teams)
            {
                var teamInfo = _mapper.Map<Team, TeamInfo>(team);
                teams.Add(teamInfo);
            }
            return Ok(teams);
        }
        [Authorize]
        [HttpPost]
        [Route("addTeam")]
        public async Task<IActionResult> AddTeam(TeamAddRequest request)
        {
            var author = (User)HttpContext.Items["User"];
            var authorTeamMember = await _teamMemberRepository.Add(new TeamMember() { UserId = author.Id, Roles = new List<Role>(), Tasks = new List<Model.Entities.TaskModels.Task>() });
            author = _userService.GetById(author.Id);
            var team = new Team()
            { Name = request.Name };
            team = await _teamRepository.Add(team);
            var adminRole = await _roleRepository.Add(new Role() { Name = "Admin", Color = ColorGenerator.GenerateColor(), IsAdmin = true });
            team.Roles.Add(adminRole);
            authorTeamMember.Roles.Add(adminRole);
            team.TeamMembers.Add(authorTeamMember);
            foreach (var memberId in request.UsersId)
            {
                var user = _userService.GetById(memberId);
                if (user != null && team.TeamMembers.Where(x => x.Id == memberId).FirstOrDefault() == null)
                {
                    var userTeamMember = await _teamMemberRepository.Add(new TeamMember() {
                        UserId = memberId,
                        Roles = new List<Role>(),
                        Tasks = new List<Model.Entities.TaskModels.Task>() });
                    team.TeamMembers.Add(userTeamMember);
                }
            }
            _teamRepository.Update(team);
            if (author.Teams == null)
                author.Teams = new List<Team>();
            author.Teams.Add(team);
            _userService.Update(author);
            return Ok(team);
        }
        [Authorize]
        [HttpPut]
        [Route("{teamId}/addRole")]
        public async Task<IActionResult> AddRole(int teamId,[FromBody] AddRoleRequest request)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            if (team == null)
                return StatusCode(400);

            var teamMember = team.TeamMembers.Where(x => x.UserId == author.Id).FirstOrDefault();
            if(teamMember == null && teamMember.Roles.Where(x => x.IsAdmin).FirstOrDefault() == null)
                return StatusCode(400);

            var role = await _roleRepository.Add(new Role() { Name = request.Name, Color = request.Color, IsAdmin = request.IsAdmin });
            team.Roles.Add(role);
            _teamRepository.Update(team);

            return Ok(role);
        }
        [Authorize]
        [HttpPut]
        [Route("{teamId}/addRole")]
        public async Task<IActionResult> AddRole(int teamId, [FromBody] AddRoleRequest request)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            if (team == null)
                return StatusCode(400);

            var teamMember = team.TeamMembers.Where(x => x.UserId == author.Id).FirstOrDefault();
            if (teamMember == null && teamMember.Roles.Where(x => x.IsAdmin).FirstOrDefault() == null)
                return StatusCode(400);

            var role = await _roleRepository.Add(new Role() { Name = request.Name, Color = request.Color, IsAdmin = request.IsAdmin });
            team.Roles.Add(role);
            _teamRepository.Update(team);

            return Ok(role);
        }

    }
}
