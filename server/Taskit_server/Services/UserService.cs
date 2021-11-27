using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Taskit_server.Model.Helpers;
using Taskit_server.Services.Interfaces;
using Taskit_server.Model.Entities;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public UserAuthentificationResponse Authenticate(UserAuthentificationRequest model)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null)
            {
                // todo: need to add logger
                return null;
            }

            var token = _configuration.GenerateJwtToken(user);

            return new UserAuthentificationResponse(user, token);
        }

        public async Task<UserAuthentificationResponse> Register(UserRegistrationRequest userModel)
        {
            var user = _mapper.Map<User>(userModel);

            var addedUser = await _userRepository.Add(user);

            var response = Authenticate(new UserAuthentificationRequest
            {
                Username = user.Username,
                Password = user.Password
            });

            return response;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }
        public void Update(User user)
        {
            _userRepository.Update(user);
        }

    }
}
