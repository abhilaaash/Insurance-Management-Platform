using EInsuranceProject.Data;
using EInsuranceProject.Model;

namespace EInsuranceProject.Repository
{
    public class EmployeeRepository:EntityRepository<Employee>,IEmployeeRepository
    {
        private InsuranceContext _context;
        public EmployeeRepository(InsuranceContext context) : base(context)
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


        public async Task AddEmployeeWithUser(Employee agent)
        {
            //  admin.User = user; // Associate the user with the admin
            _context.Employees.Add(agent);
            await _context.SaveChangesAsync();
        }
    }
}
