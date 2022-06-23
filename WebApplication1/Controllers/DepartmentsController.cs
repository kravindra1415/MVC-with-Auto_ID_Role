using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Services;
using WebApplication1.Data.ViewModel;

namespace WebApplication1.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentsServices _departmentsService;

        public DepartmentsController(IDepartmentsServices departmentsService)
        {
            _departmentsService = departmentsService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var departments = await _departmentsService.GetAllAsync();
            return View(departments);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var department = await _departmentsService.GetByIdAsync((int)id);

            if (department == null)
                return NotFound();

            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel department)
        {
            if (ModelState.IsValid)
            {
                await _departmentsService.CreateAsync(department);
                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }
    }
}
