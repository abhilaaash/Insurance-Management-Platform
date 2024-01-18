using EInsuranceProject.Data;
using EInsuranceProject.Model;

namespace EInsuranceProject.Repository
{
    public class AdminRepository:EntityRepository<Admin>,IAdminRepository
    {
        private InsuranceContext _context;
        public AdminRepository(InsuranceContext context):base(context)
        {
            _context = context;
        }

        public async Task<int> AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.UserId; // Return the generated UserId
        }
        public async Task AddAdminWithUser(Admin admin)
        {
          //  admin.User = user; // Associate the user with the admin
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
        }
    }
}
