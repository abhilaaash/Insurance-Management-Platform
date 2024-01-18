using EInsuranceProject.Data;
using EInsuranceProject.Model;
using EInsuranceProject.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace EInsuranceProject.Repository
{
    public class CustomerRepository: EntityRepository<Customer>, ICustomerRepository
    {
        private InsuranceContext _context;
        public CustomerRepository(InsuranceContext context):base(context) 
        {
            _context = context;
        }
        
        public async Task<List<Customer>> GetCustomersByAgentIdAsync(int agentId)
        {
            return await _context.Customers
                .Where(c => c.AgentId == agentId)
                .ToListAsync();
        }
        public async Task<int> AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.UserId; // Return the generated UserId
        }


        public async Task AddCustomerWithUser(Customer cust)
        {
            //  admin.User = user; // Associate the user with the admin
            _context.Customers.Add(cust);
            await _context.SaveChangesAsync();
        }
        //---------------------------------------------------------------------


        public async Task<List<Policy>> GetCustomerPoliciesAsync(int userId, Parameter filterParameter)
        {
            var customer = await _context.Customers.Where(c => c.UserId == userId && c.Status == true).Include(c => c.Policies).FirstOrDefaultAsync();

            if (customer == null)
            {
                return new List<Policy>();
            }

            if (filterParameter.Id == null)
            {
                return customer.Policies.FindAll(p => p.Status == true).ToList();
            }

            return customer.Policies.FindAll(p => p.Status == true && p.PolicyId == filterParameter.Id).ToList();
        }
        public Customer GetByPhone(string username)
        {
            //  int? roleId = _context.Users.Where(u=> u.Username == username).Select(r=>r.RoleId).FirstOrDefault();
            var user = _context.Customers.FirstOrDefault(u => u.Phone == username);

            //if (user != null && user.RoleId != null)
            //{
            //    // return  (int) roleId;
            //    return GetRoleNameByRoleId(user.RoleId);
            //}

            return user;
        }
        public Customer GetByEmail(string username)
        {
            //  int? roleId = _context.Users.Where(u=> u.Username == username).Select(r=>r.RoleId).FirstOrDefault();
            var user = _context.Customers.FirstOrDefault(u => u.Email == username);

            //if (user != null && user.RoleId != null)
            //{
            //    // return  (int) roleId;
            //    return GetRoleNameByRoleId(user.RoleId);
            //}

            return user;
        }
    }
}
