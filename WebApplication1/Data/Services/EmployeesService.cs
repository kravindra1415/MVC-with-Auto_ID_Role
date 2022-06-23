using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Models;
using WebApplication1.Data.ViewModel;

namespace WebApplication1.Data.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeesService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        //public async Task<List<EmployeeViewModel>> GetAllAsyncOldMethod()
        //{
        //    var employeesQuery = _dbContext.Employees
        //        .Include(nameof(Employee.DepartmentRef))
        //        .Include(nameof(Employee.NationalityRef))
        //        .Select(d => _mapper.Map<EmployeeViewModel>(d));

        //    var employees = await employeesQuery.ToListAsync();

        //    return employees;
        //}

        public async Task<List<EmployeeViewModel>> GetAllAsync()
        {
            var employees = await _mapper.ProjectTo<EmployeeViewModel>(_dbContext.Employees).ToListAsync();
            return employees;
        }

        //public async Task<EmployeeViewModel?> GetByIdAsyncOldMethod(int id)
        //{
        //    var employee = await _dbContext.Employees
        //        .Include(nameof(Employee.DepartmentRef))
        //        .Include(nameof(Employee.NationalityRef))
        //        .FirstOrDefaultAsync(m => m.Id == id);

        //    return _mapper.Map<EmployeeViewModel>(employee);
        //}

        public async Task<EmployeeViewModel?> GetByIdAsync(int id)
        {
            var employee = await _mapper.ProjectTo<EmployeeViewModel>(_dbContext.Employees).FirstOrDefaultAsync(m => m.Id == id);

            return employee;
        }

        public async Task CreateAsync(EmployeeViewModel employee)
        {
            var employeeDataModel = _mapper.Map<Employee>(employee);

            _dbContext.Add(employeeDataModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<DropdownViewModel>> GetDepartmentForDropDownAsync()
        {
            var departmentsForDropDown = await _dbContext.Departments.Select(e => new DropdownViewModel { Id = e.Id, Text = e.Name }).ToListAsync();
            return departmentsForDropDown;
        }

        public async Task<List<DropdownViewModel>> GetNationalityForDropDownAsync()
        {
            var nationalitiesForDropDown = await _dbContext.Nationalities.Select(e => new DropdownViewModel { Id = e.Id, Text = e.Text }).ToListAsync();
            return nationalitiesForDropDown;
        }

    }
}
