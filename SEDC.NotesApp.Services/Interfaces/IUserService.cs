using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Services.Interfaces
{
    public interface IUserService
    {
        User GetById(int userId);
        IEnumerable<User> GetAll();
        void Add(User user);
        void Update(User user);
        void Delete(int userId);
    }
}
