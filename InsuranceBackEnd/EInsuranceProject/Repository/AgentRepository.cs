using EInsuranceProject.Data;
using EInsuranceProject.DTO;
using EInsuranceProject.Model;

namespace EInsuranceProject.Repository
{
    public class AgentRepository : EntityRepository<Agent>, IAgentRepository
    {

        private  InsuranceContext _context;
        public AgentRepository(InsuranceContext context):base(context)
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
        public async Task AddAgentWithUser(Agent agent)
        {
            //  admin.User = user; // Associate the user with the admin
            _context.Agents.Add(agent);
            await _context.SaveChangesAsync();
        }

    }
}
