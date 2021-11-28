using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Taskit_server.Model.Entities;
using Taskit_server.Model.Entities.RoleModels;
using Taskit_server.Model.Entities.TakenTaskModels;
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
        private readonly IRepository<TakenTask> _takenTasksRepository;
        public TeamController(IRepository<Team> teamRepository,
            IUserService userService,
            IRepository<Role> roleRepository,
            IRepository<TeamMember> teamMemberRepository,
            IRepository<Model.Entities.TaskModels.Task> taskRepository,
            IRepository<TakenTask> takenTasksRepository,
            IMapper mapper)
        {
            _teamRepository = teamRepository;
            _userService = userService;
            _roleRepository = roleRepository;
            _teamMemberRepository = teamMemberRepository;
            _mapper = mapper;
            _taskRepository = taskRepository;
            _takenTasksRepository = takenTasksRepository;
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
                var members = new List<TeamMemberInfo>();
                foreach(var member in team.TeamMembers)
                {
                    var user = _userService.GetById(member.UserId);
                    if (user != null)
                        members.Add(new TeamMemberInfo() { Name = user.Username, Roles = member.Roles, UserId = user.Id });
                }
                teamInfo.Members = members;
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
            if(teamMember == null || teamMember.Roles.Where(x => x.IsAdmin).FirstOrDefault() == null)
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
                    roles.Add(role);
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
        }//всі задачі
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
        [Route("{teamId}/getMembers")]
        public async Task<IActionResult> GetMembers(int teamId)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            if (team == null)
                return StatusCode(400);

            var members = new List<TeamMemberInfo>();
            foreach (var member in team.TeamMembers)
            {
                var user = _userService.GetById(member.UserId);
                if (user != null)
                    members.Add(new TeamMemberInfo() { Name = user.Username, Roles = member.Roles, UserId = user.Id });
            }

            return Ok(members);
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

            var teamInfo = _mapper.Map<Team, TeamInfo>(team);
            var members = new List<TeamMemberInfo>();
            foreach (var member in team.TeamMembers)
            {
                var user = _userService.GetById(member.UserId);
                if (user != null)
                    members.Add(new TeamMemberInfo() { Name = user.Username, Roles = member.Roles, UserId = user.Id });
            }
            teamInfo.Members = members;

            return Ok(teamInfo);
        }//вся інфа по тімі
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
        }//кількість очків виконані/всі
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
            team.Tasks.Remove(task);
            _teamRepository.Update(team);

            return Ok();
        }//видалити завдання
        [Authorize]
        [HttpPut]
        [Route("{teamId}/takeTask")]
        public async Task<IActionResult> TakeTask(int teamId, [FromBody] AddTakenTaskRequest request)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            if (team == null)
                return StatusCode(400);

            var task = team.Tasks.Where(t => t.Id == request.TaskId).FirstOrDefault();
            if (task == null)
                return StatusCode(400);

            var teamMember = team.TeamMembers.Where(t => t.UserId == author.Id).FirstOrDefault();
            if(teamMember == null)
                return StatusCode(400);

            if(teamMember.UserId != author.Id)
            {
                if (teamMember == null && teamMember.Roles.Where(x => x.IsAdmin).FirstOrDefault() == null)
                    return StatusCode(400);
            }

            task.Performers.Add(teamMember);
            _taskRepository.Update(task);

            var takenTask = new TakenTask()
            {
                Color = request.Color,
                EndTime = request.EndTime,
                StartTime = request.StartTime,
                TaskId = task.Id
            };

            var user = _userService.GetById(request.MemberId);
            if(user == null)
                return StatusCode(400);
            takenTask = await _takenTasksRepository.Add(takenTask);


            user.TaskenTasks.Add(takenTask);
            _userService.Update(user);

            var members = team.TeamMembers.Where(x => x.Tasks.Where(t => t.Id == task.Id).FirstOrDefault() != null).ToList();
            var teamMembersInfo = new List<TeamMemberInfo>();
            foreach (var m in members)
            {
                var use = _userService.GetById(m.UserId);
                if (use != null)
                    teamMembersInfo.Add(new TeamMemberInfo() { Name = user.Username, UserId = m.UserId, Roles = m.Roles });
            }
            return Ok(new TaskInfo(task, teamMembersInfo));
        }//взяти собі завдання
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
        }//вигнати когось з команди
        [Authorize]
        [HttpGet]
        [Route("{teamId}/getTakenTasks")]
        public async Task<IActionResult> GetTakenTasks(int teamId)
        {
            var author = (User)HttpContext.Items["User"];
            var team = _teamRepository.GetById(teamId);

            var takenTasks = new List<TakenTaskInfo>();
            foreach(var takentask in author.TaskenTasks)
            {
                var task = _taskRepository.GetById(takentask.TaskId);
                if (task != null
                    || (takentask.EndTime.DayOfYear == DateTime.Now.DayOfYear || takentask.StartTime.DayOfYear != DateTime.Now.DayOfYear)
                    || task.Team.Id != teamId)
                {
                    var teamMembersInfo = new List<TeamMemberInfo>();
                    foreach(var teamMember in task.Performers)
                    {
                        var user = _userService.GetById(teamMember.UserId);
                        teamMembersInfo.Add(new TeamMemberInfo()
                        {
                            Name = user.Username,
                            UserId = user.Id,
                            Roles = teamMember.Roles
                        });
                    }
                    var taskInfo = new TaskInfo(task,teamMembersInfo);
                    takenTasks.Add(new TakenTaskInfo()
                    {
                        Color = takentask.Color,
                        StartTime = takentask.StartTime,
                        EndTime = takentask.EndTime,
                        Task = taskInfo
                    });
                }
            }
            return Ok(takenTasks);
        }
    } 
}
