using WebApplication1.Data.ViewModel;

namespace WebApplication1.Data.Services
{
    public interface IDepartmentsServices
    {
        Task CreateAsync(DepartmentViewModel departmentViewModel);
        Task<List<DepartmentViewModel>> GetAllAsync();
        Task<DepartmentViewModel> GetByIdAsync(int id);
    }
}