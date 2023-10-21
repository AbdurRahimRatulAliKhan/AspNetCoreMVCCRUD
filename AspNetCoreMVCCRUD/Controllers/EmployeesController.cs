using AspNetCoreMVCCRUD.Data;
using AspNetCoreMVCCRUD.Models;
using AspNetCoreMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreMVCCRUD.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeesController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly MVCDemoDbContext mvcDemoDbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mvcDemoDbContext"></param>
        public EmployeesController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoDbContext.Employees.ToListAsync();
            return View(employees);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        /// <param name="addEmployeeRequest"></param>
        //        /// <returns></returns>
        //        [HttpPost]
        //        public IActionResult Add(AddEmployeeViewModel addEmployeeRequest)
        //        {
        //            var employee = new Employee()
        //            {
        //                Id = Guid.NewGuid(),
        //                Name = addEmployeeRequest.Name,
        //                Email = addEmployeeRequest.Email,
        //                Salary = addEmployeeRequest.Salary,
        //                Department = addEmployeeRequest.Department,
        //                DateofBirth = addEmployeeRequest.DateofBirth
        //            };

        //            mvcDemoDbContext.Employees.Add(employee);
        //            mvcDemoDbContext.SaveChanges();
        //            return RedirectToAction("Add");
        ////            return View(addEmployeeRequest);
        //        }

        /// <summary>
        /// async conversion
        /// </summary>
        /// <param name="addEmployeeRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateofBirth = addEmployeeRequest.DateofBirth
            };

            await mvcDemoDbContext.Employees.AddAsync(employee);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
            //            return View(addEmployeeRequest);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateofBirth = employee.DateofBirth
                };
                return await Task.Run(() => View("View",viewModel));
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);
            if (employee != null) 
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateofBirth = model.DateofBirth;
                employee.Department = model.Department;

                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                mvcDemoDbContext.Employees.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}