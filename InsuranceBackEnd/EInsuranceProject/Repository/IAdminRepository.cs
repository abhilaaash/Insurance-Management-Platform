using EInsuranceProject.Model;
using Microsoft.EntityFrameworkCore;

namespace EInsuranceProject.Repository
{
    public interface IAdminRepository:IEntityRepository<Admin>
    {
        public  Task<int> AddUser(User user);

        public  Task AddAdminWithUser(Admin admin);
    }
}
