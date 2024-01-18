using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using EInsuranceProject.Repository;
using EInsuranceProject.Wrapper;
using System.Linq.Expressions;

namespace EInsuranceProject.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEntityRepository<Employee> _repository;
        private IEmployeeRepository _employeeRepository;
        public EmployeeService(IEntityRepository<Employee> repository, IEmployeeRepository employeeRepository)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
        }
        public async Task AddEmployee(Employee employee)
        {
           await _repository.Insert(employee);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var EmployeeToDelete = await _repository.GetById(id);
            if (EmployeeToDelete != null)
                return await _repository.Delete(id);
            throw new DataNotFoundExcpetion("No Employee Data found");
        }

        public async Task<IEnumerable<Employee>> GetAll(string[] innerTables)
        {
            Expression<Func<Employee, bool>> filterPredicate = e => e.Status !=null;
            var Employees = await _repository.GetAll(innerTables,filterPredicate);
            if (Employees.Count() > 0)
            {
                return Employees;
            }
            throw new DataNotFoundExcpetion("No Employee Data found");
        }

        public async Task<Employee> GetById(int Id)
        {
            string[] innerTables = { };
            Expression<Func<Employee, bool>> filterPredicate = e => e.Status != null && e.UserId==Id;
            var Employee = await _repository.GetById(filterPredicate,innerTables);
            if (Employee != null)
            {
                return Employee;
            }
            throw new DataNotFoundExcpetion("No Employee Data found");
        }
        public async Task<Employee> GetByEmployeeId(int Id)
        {
            string[] innerTables = { };
            Expression<Func<Employee, bool>> filterPredicate = e => e.Status != null && e.EmployeeId == Id;
            var Employee = await _repository.GetById(filterPredicate, innerTables);
            if (Employee != null)
            {
                return Employee;
            }
            throw new DataNotFoundExcpetion("No Employee Data found");
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            var EmployeeToUpdate = await _repository.GetById(employee.EmployeeId);
            if (EmployeeToUpdate != null)
            {
                return await _repository.Update(employee, employee.EmployeeId);
            }
            throw new DataNotFoundExcpetion("No Policy Data found");
        }
        public async Task AddEmployeeWithUser(Employee employee, User user)
        {
            int id = await _employeeRepository.AddUser(user);
            employee.UserId = id;
            await _employeeRepository.AddEmployeeWithUser(employee);
        }

        public async Task<PageList<Employee>> GetAsync(Parameter parameter)
        {
            Expression<Func<Employee, bool>> filterPredicate = e => e.Status != null;
            if (parameter.Id != null)
            {
                filterPredicate = e => e.Status != null && e.EmployeeId == parameter.Id;
            }
            else if (parameter.FirstName != null)
            {
                filterPredicate = e => e.Status != null && e.EmployeeFirstName.Contains(parameter.FirstName);
            }
            else if (parameter.PhoneNumber != null)
            {
                filterPredicate = e => e.Status != null && e.Phone.Contains(parameter.PhoneNumber);
            }
            else if (parameter.Email != null)
            {
                filterPredicate = e => e.Status != null && e.Email.Contains(parameter.Email);
            }

            else
            {
                filterPredicate = e => e.Status != null;
            }

            var customers = await _repository.GetAsync(filterPredicate);
            return PageList<Employee>.ToPagedList(customers, parameter.PageNumber, parameter.PageSize);
        }
        //public async Task<PageList<Employee>> GetAll(EmployeeParameter employeeParameter)
        //{
        //    Expression<Func<Employee, bool>> filterPredicate = e => e.Status == true;
            

        //    var employees = await _repository.GetAsync(filterPredicate);
        //    return PageList<Employee>.ToPagedList(employees, employeeParameter.PageNumber, employeeParameter.PageSize);
        //}
    }
}

