using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.Model;
using EInsuranceProject.Wrapper;

namespace EInsuranceProject.Services
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<Employee>> GetAll(string[] innerTables);
        public Task<Employee> GetById(int Id);
        public Task AddEmployee(Employee employee);
        public Task<bool> UpdateEmployee(Employee employee);
        public Task<bool> DeleteEmployee(int id);
        public Task AddEmployeeWithUser(Employee employee, User user);
        public Task<Employee> GetByEmployeeId(int Id);
        public Task<PageList<Employee>> GetAsync(Parameter pageParameter);

    }
}
