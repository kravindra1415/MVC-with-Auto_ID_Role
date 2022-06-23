using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Data.Services;
using WebApplication1.Data.ViewModel;

namespace WebApplication1.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var employees = await _employeesService.GetAllAsync();
            return View(employees);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _employeesService.GetByIdAsync((int)id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["DepartmentList"] = await GetSelectListDepartmentAsync();
            ViewData["NationalityList"] = await GetSelectListNationalityAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                await _employeesService.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentList"] = await GetSelectListDepartmentAsync();
            ViewData["NationalityList"] = await GetSelectListNationalityAsync();

            return View(employee);
        }

        private async Task<SelectList> GetSelectListDepartmentAsync()
        {
            return new SelectList(
                await _employeesService.GetDepartmentForDropDownAsync(),
                nameof(DropdownViewModel.Id), nameof(DropdownViewModel.Text));
        }

        private async Task<SelectList> GetSelectListNationalityAsync()
        {
            return new SelectList(
                await _employeesService.GetNationalityForDropDownAsync(),
                nameof(DropdownViewModel.Id), nameof(DropdownViewModel.Text));
        }
    }
}

//var d=new SelectList(data,"iD","Name");