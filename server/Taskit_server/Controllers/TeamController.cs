using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Taskit_server.Model.Entities;
using Taskit_server.Model.Entities.RoleModels;
using Taskit_server.Model.Entities.TaskModels;
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
        private readonly IRepository<Model.Entities.TaskModels.Task> _taskRepository;
        public TeamController(IRepository<Team> teamRepository,
            IUserService userService,
            IRepository<Role> roleRepository,
            IRepository<TeamMember> teamMemberRepository,
            IRepository<Model.Entities.TaskModels.Task> taskRepository,
            IMapper mapper)
        {
            _teamRepository = teamRepository;
            _userService = userService;
            _roleRepository = roleRepository;
            _teamMemberRepository = teamMemberRepository;
            _mapper = mapper;
            _taskRepository = taskRepository;
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
        [Route("{teamId}/addMember")]
        public async Task<IActionResult> AddMember(int teamId, [FromQuery] int memberId)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            if (team == null)
                return StatusCode(400);

            var teamMember = team.TeamMembers.Where(x => x.UserId == author.Id).FirstOrDefault();
            if (teamMember == null && teamMember.Roles.Where(x => x.IsAdmin).FirstOrDefault() == null)
                return StatusCode(400);


            var newUser = _userService.GetById(memberId);
            if (newUser == null)
                return StatusCode(400);

            var newTeamMember = await _teamMemberRepository.Add(new TeamMember() { UserId = memberId });
            team.TeamMembers.Add(newTeamMember);

            var TeamMemberInfo = new TeamMemberInfo() { Name = author.Username, Roles = newTeamMember.Roles, UserId = author.Id };
            return Ok(TeamMemberInfo);
        }
        [Authorize]
        [HttpPut]
        [Route("{teamId}/addTask")]
        public async Task<IActionResult> AddTask(int teamId, [FromBody] TaskAddRequest request)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            if (team == null)
                return StatusCode(400);

            var teamMember = team.TeamMembers.Where(x => x.UserId == author.Id).FirstOrDefault();
            if (teamMember == null && teamMember.Roles.Where(x => x.IsAdmin).FirstOrDefault() == null)
                return StatusCode(400);

            var roles = new List<Role>();
            foreach(var roleid in request.RolesId)
            {
                var role = _roleRepository.GetById(roleid);
                if (role != null && team.Roles.Where(x => x.Id == roleid).FirstOrDefault() == null)
                {
                    roles.Add(role);
                }
            }
            var task = await _taskRepository.Add(new Model.Entities.TaskModels.Task()
            {
                Content = request.Content,
                Price = request.Price,
                Deadline = request.Deadline,
                Name = request.Name,
                State = TaskState.Todo,
                Roles = roles,
                Team = team
            });
            var members = team.TeamMembers.Where(x => x.Tasks.Where(t => t.Id == task.Id).FirstOrDefault() != null).ToList();
            team.Tasks.Add(task);
            _teamRepository.Update(team);
             _taskRepository.Update(task);
            var teamMembersInfo = new List<TeamMemberInfo>();
            foreach (var m in members)
            {
                var user = _userService.GetById(m.UserId);
                if (user != null)
                    teamMembersInfo.Add(new TeamMemberInfo() { Name = user.Username, UserId = m.UserId, Roles = m.Roles });
            }
            return Ok(new TaskInfo(task, teamMembersInfo));
        }
        [Authorize]
        [HttpGet]
        [Route("{teamId}/getTasks")]
        public async Task<IActionResult> GetTasks(int teamId)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            if (team == null)
                return StatusCode(400);

            var tasks = new List<TaskInfo>();
            foreach(var task in team.Tasks)
            {
                var members = team.TeamMembers.Where(x => x.Tasks.Where(t => t.Id == task.Id).FirstOrDefault() != null).ToList();
                var teamMembersInfo = new List<TeamMemberInfo>();
                foreach (var m in members)
                {
                    var user = _userService.GetById(m.UserId);
                    if (user != null)
                        teamMembersInfo.Add(new TeamMemberInfo() { Name = user.Username, UserId = m.UserId, Roles = m.Roles });
                }
                tasks.Add(new TaskInfo(task, teamMembersInfo));
            }
            return Ok(tasks);
        }
        [Authorize]
        [HttpGet]
        [Route("{teamId}/getRoles")]
        public async Task<IActionResult> GetRoles(int teamId)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            if (team == null)
                return StatusCode(400);

            return Ok(team.Roles);
        }
        [Authorize]
        [HttpGet]
        [Route("{teamId}/getTeam")]
        public async Task<IActionResult> GetTeam(int teamId)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            if (team == null)
                return StatusCode(400);

            return Ok(team);
        }
        [Authorize]
        [HttpGet]
        [Route("{teamId}/getPoints")]
        public async Task<IActionResult> GetPoints(int teamId)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            if (team == null)
                return StatusCode(400);

            int sum = 0, sumDone = 0;
            team.Tasks.ForEach(t => {
                sumDone += t.State == TaskState.Done ? t.Price : 0;
                sum += t.Price;
                }
            );

            return Ok(new Points(sumDone,sum));
        }
        [Authorize]
        [HttpPatch]
        [Route("{teamId}/updateTask")]
        public async Task<IActionResult> UpdateTask(int teamId,int taskId, int taskState)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            if (team == null || taskState > 2 || taskState < 0)
                return StatusCode(400);

            var task = team.Tasks.Where(t => t.Id == taskId).FirstOrDefault();

            if(task == null)
                return StatusCode(400);

            task.State = (TaskState)taskState;

            _taskRepository.Update(task);
            var members = team.TeamMembers.Where(x => x.Tasks.Where(t => t.Id == task.Id).FirstOrDefault() != null).ToList();
            var teamMembersInfo = new List<TeamMemberInfo>();
            foreach (var m in members)
            {
                var user = _userService.GetById(m.UserId);
                if (user != null)
                    teamMembersInfo.Add(new TeamMemberInfo() { Name = user.Username, UserId = m.UserId, Roles = m.Roles });
            }
            return Ok(new TaskInfo(task, teamMembersInfo));
        }
        [Authorize]
        [HttpDelete]
        [Route("{teamId}/deleteTask")]
        public async Task<IActionResult> DeleteTask(int teamId, int taskId)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            if (team == null)
                return StatusCode(400);

            var task = team.Tasks.Where(t => t.Id == taskId).FirstOrDefault();

            if (task == null)
                return StatusCode(400);

            _taskRepository.Remove(task);

            return Ok();
        }
        [Authorize]
        [HttpPut]
        [Route("{teamId}/takeTask")]
        public async Task<IActionResult> TakeTask(int teamId, int taskId)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            if (team == null)
                return StatusCode(400);

            var task = team.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
            if (task == null)
                return StatusCode(400);

            var teamMember = team.TeamMembers.Where(t => t.UserId == author.Id).FirstOrDefault();
            if(teamMember == null)
                return StatusCode(400);

            task.Performers.Add(teamMember);

            _taskRepository.Update(task);
            var members = team.TeamMembers.Where(x => x.Tasks.Where(t => t.Id == task.Id).FirstOrDefault() != null).ToList();
            var teamMembersInfo = new List<TeamMemberInfo>();
            foreach (var m in members)
            {
                var user = _userService.GetById(m.UserId);
                if (user != null)
                    teamMembersInfo.Add(new TeamMemberInfo() { Name = user.Username, UserId = m.UserId, Roles = m.Roles });
            }
            return Ok(new TaskInfo(task, teamMembersInfo));
        }
        [Authorize]
        [HttpPut]
        [Route("{teamId}/giveTask")]
        public async Task<IActionResult> GiveTask(int teamId, int taskId, int memberId)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            if (team == null)
                return StatusCode(400);

            var task = team.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
            if (task == null)
                return StatusCode(400);

            var teamMember = team.TeamMembers.Where(x => x.UserId == author.Id).FirstOrDefault();
            if (teamMember == null && teamMember.Roles.Where(x => x.IsAdmin).FirstOrDefault() == null)
                return StatusCode(400);

            var member = _teamMemberRepository.GetById(memberId);
            if (member == null)
                return StatusCode(400);

            task.Performers.Add(member);

            _taskRepository.Update(task);
            var members = team.TeamMembers.Where(x => x.Tasks.Where(t => t.Id == task.Id).FirstOrDefault() != null).ToList();
            var teamMembersInfo = new List<TeamMemberInfo>();
            foreach(var m in members)
            {
                var user = _userService.GetById(m.UserId);
                if (user != null)
                    teamMembersInfo.Add(new TeamMemberInfo() { Name = user.Username, UserId = m.UserId, Roles = m.Roles });
            }
            return Ok(new TaskInfo(task,teamMembersInfo));
        }
        [Authorize]
        [HttpDelete]
        [Route("{teamId}/removeMember")]
        public async Task<IActionResult> RemoveMember(int teamId, int memberId)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            if (team == null)
                return StatusCode(400);

            var teamMember = team.TeamMembers.Where(x => x.UserId == author.Id).FirstOrDefault();
            if (teamMember == null && teamMember.Roles.Where(x => x.IsAdmin).FirstOrDefault() == null)
                return StatusCode(400);

            var member = _teamMemberRepository.GetById(memberId);
            if (member == null)
                return StatusCode(400);

            _teamMemberRepository.Remove(member);

            return Ok("");
        }
    }
}
