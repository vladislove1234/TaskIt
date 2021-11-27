using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Taskit_server.Model.Entities;
using Taskit_server.Model.Entities.UserModels;
using Taskit_server.Services.Interfaces;

namespace Taskit_server.Controllers
{
    public class TeamController : Controller
    {
        private readonly IRepository<Team> _teamRepository;
        private readonly IUserService _userService;
        public TeamController(IRepository<Team> teamRepository, IUserService userService)
        {
            _teamRepository = teamRepository;
            _userService = userService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetTeams()
        {
            var user = (User)HttpContext.Items["User"];

            if (user == null)
                return StatusCode(401);

            return Ok(user.Teams);
        }
    }
}
