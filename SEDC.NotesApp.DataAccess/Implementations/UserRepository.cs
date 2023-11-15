using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SEDC.NotesApp.Domain.Models;

namespace SEDC.NotesApp.DataAccess.Implementations
{
    public class UserRepository : IRepository<User>
    {
        private NotesAppDbContext _notesAppDbContext;

        public UserRepository(NotesAppDbContext notesAppDbContext)
        {
            _notesAppDbContext = notesAppDbContext;
        }

        public void Add(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _notesAppDbContext.Users.Add(entity);   
            _notesAppDbContext.SaveChanges();   
        }

        public void Delete(User entity)
        {
            var user = _notesAppDbContext.Users.Find(entity);
            if(user != null)
            {
                _notesAppDbContext.Users.Remove(user);
                _notesAppDbContext.SaveChanges();
            }
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            return _notesAppDbContext.Users
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _notesAppDbContext.Update(entity);
            _notesAppDbContext.SaveChanges();
        }
    }
}
