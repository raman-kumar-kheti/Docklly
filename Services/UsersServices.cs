using Docklly.Database;
using Docklly.Models;
namespace Docklly.Services
{
    public class UsersServices
    {

        // Inject data database connection
        private readonly AppDbContext _context;
        public UsersServices(AppDbContext dbContext)
        {
            _context = dbContext;
        }
    
        public List<Users> GetAllUsers()
        {

            var getAllUser = _context.Users.ToList();
            return getAllUser;
        }

    }
}