using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserService _userService;

        public UserService(IUserService userService)
        {
            _userService = userService;
        }
        public void Add(User user)
        {
            _userService.Add(user);
        }

        public void Delete(int userId)
        {
            _userService.Delete(userId);
        }

        public IEnumerable<User> GetAll()
        {
            return _userService.GetAll();   
        }

        public User GetById(int userId)
        {
            return _userService.GetById(userId);
        }

        public void Update(User user)
        {
            _userService.Update(user);
        }
    }
}
