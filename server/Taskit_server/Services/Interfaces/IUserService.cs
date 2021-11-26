using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taskit_server.Model.Entities;
using Taskit_server.Model.Entities.UserModels;

namespace Taskit_server.Services.Interfaces
{
    public interface IUserService
    {
        UserAuthentificationResponse Authenticate(UserAuthentificationRequest model);
        Task<UserAuthentificationResponse> Register(UserRegistrationRequest userModel);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
