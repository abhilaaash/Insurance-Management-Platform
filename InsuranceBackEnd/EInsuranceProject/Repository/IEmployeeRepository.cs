using EInsuranceProject.Model;

namespace EInsuranceProject.Repository
{
    public interface IEmployeeRepository:IEntityRepository<Employee>
    {
        public Task<int> AddUser(User user);

        public Task AddEmployeeWithUser(Employee employee);
    }
}
