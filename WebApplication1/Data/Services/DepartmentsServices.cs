using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Models;
using WebApplication1.Data.ViewModel;

namespace WebApplication1.Data.Services
{
    public class DepartmentsServices : IDepartmentsServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public DepartmentsServices(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<DepartmentViewModel>> GetAllAsync()
        {
            var departments = await _dbContext.Departments
                .Select(d => _mapper.Map<DepartmentViewModel>(d)).ToListAsync();

            return departments;

        }

        public async Task<DepartmentViewModel> GetByIdAsync(int id)
        {
            var department = await _dbContext.Departments.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<DepartmentViewModel>(department);
        }

        public async Task CreateAsync(DepartmentViewModel departmentViewModel)
        {
            var departmentDataModel = _mapper.Map<Department>(departmentViewModel);

            _dbContext.Add(departmentDataModel);
            await _dbContext.SaveChangesAsync();
        }
    }
}
