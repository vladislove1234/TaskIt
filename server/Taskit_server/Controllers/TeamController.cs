using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public TeamController(IRepository<Team> teamRepository, IUserService userService, IRepository<Role> roleRepository)
        {
            _teamRepository = teamRepository;
            _userService = userService;
            _roleRepository = roleRepository;
        }
        [HttpGet]
        [Route("getTeams")]
        public IActionResult GetTeams()
        {
            var teams = _teamRepository.GetAll();
            return Ok(teams);
        }
        [Authorize]
        [HttpPost]
        [Route("addTeam")]
        public async Task<IActionResult> AddTeam(TeamAddRequest request)
        {
            var author = (User)HttpContext.Items["User"];
            author = _userService.GetById(author.Id);
            if (author == null)
                return StatusCode(401);
            var team = new Team()
            {
                Name = request.Name
            };
            team = await _teamRepository.Add(team);
            team.Users.Add(author);
            var adminRole = await _roleRepository.Add(new Role() { Name = "Admin", Color = ColorGenerator.GenerateColor(), IsAdmin = true });
            team.Roles.Add(adminRole);
            _teamRepository.Update(team);
            if (author.Teams == null)
                author.Teams = new List<Team>();
            author.Teams.Add(team);
            _userService.Update(author);
            return Ok(team);
        }

    }
}
