using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Taskit_server.Model.Entities.TakenTaskModels;
using Taskit_server.Model.Entities.TaskModels;
using Taskit_server.Model.Entities.UserModels;
using Taskit_server.Services.Interfaces;

namespace Taskit_server.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRepository<Model.Entities.TaskModels.Task> _taskRepository;
        public UserController(IUserService userService,
             IRepository<Model.Entities.TaskModels.Task> taskRepository,
             IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(UserAuthentificationRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationRequest userModel)
        {
            var response = await _userService.Register(userModel);

            if (response == null)
            {
                return BadRequest(new { message = "Didn't register!" });
            }

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("getByName")]
        public IActionResult GetByName(string name)
        {
            var users = _userService.GetAll().Where(u => u.Username.Contains(name)
            || u.Email.Contains(name)).Take(10).ToList();
            return Ok(_mapper.Map<List<User>,List<UserInfo>>(users));
        }

        [Authorize]
        [HttpGet]
        [Route("getById")]
        public IActionResult GetById(int Id)
        {
            var users = _userService.GetAll().Where(u => u.Id == Id).FirstOrDefault();
            return Ok(_mapper.Map<User, UserInfo>(users));
        }

        [Authorize]
        [HttpGet]
        [Route("getTakenTasks")]
        public async Task<IActionResult> GetTakenTasks()
        {
            var author = (User)HttpContext.Items["User"];

            var takenTasks = new List<TakenTaskInfo>();
            foreach (var takentask in author.TaskenTasks)
            {
                var task = _taskRepository.GetById(takentask.TaskId);
                if (task != null || (takentask.EndTime.DayOfYear != DateTime.Now.DayOfYear && takentask.StartTime.DayOfYear != DateTime.Now.DayOfYear))
                {
                    var teamMembersInfo = new List<TeamMemberInfo>();
                    foreach (var teamMember in task.Performers)
                    {
                        var user = _userService.GetById(teamMember.UserId);
                        teamMembersInfo.Add(new TeamMemberInfo()
                        {
                            Name = user.Username,
                            UserId = user.Id,
                            Roles = teamMember.Roles
                        });
                    }
                    var taskInfo = new TaskInfo(task, teamMembersInfo);
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
