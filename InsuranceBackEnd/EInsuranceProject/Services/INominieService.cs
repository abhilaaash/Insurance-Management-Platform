using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using System.Linq.Expressions;

namespace EInsuranceProject.Services
{
    public interface INominieService
    {
        public  Task AddNominie(Nominie nominie);
        public Task<bool> DeleteNominie(int id);
        public  Task<IEnumerable<Nominie>> GetAll(string[] innerTables);
        public Task<Nominie> GetById(int Id);
        public  Task<bool> UpdateNominie(Nominie nominie);
        public Task<List<Nominie>> GetNominieeByCustomerIdAsync(int customerId);
    }
}
