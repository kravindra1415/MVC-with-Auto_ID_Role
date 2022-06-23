using WebApplication1.Data.ViewModel;

namespace WebApplication1.Data.Services
{
    public interface IEmployeesService
    {
        Task CreateAsync(EmployeeViewModel employee);
        Task<List<EmployeeViewModel>> GetAllAsync();
        Task<EmployeeViewModel?> GetByIdAsync(int id);
        Task<List<DropdownViewModel>> GetDepartmentForDropDownAsync();
        Task<List<DropdownViewModel>> GetNationalityForDropDownAsync();
    }
}