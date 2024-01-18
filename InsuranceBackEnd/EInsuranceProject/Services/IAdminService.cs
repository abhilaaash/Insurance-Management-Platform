using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;

namespace EInsuranceProject.Services
{
    public interface IAdminService
    {
        public Task<IEnumerable<Admin>> GetAll(string[] innerTables);
        public Task<Admin> GetById(int Id);
        public Task AddAdmin(Admin agent);
        public Task<bool> UpdateAdmin(Admin agent);
        public Task<bool> DeleteAdmin(int id);
        public Task AddAdminWithUser(Admin admin, User user);

        //---------------------------------------------

    }
}
