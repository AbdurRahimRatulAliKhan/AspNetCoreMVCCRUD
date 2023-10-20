using AspNetCoreMVCCRUD.Data;
using AspNetCoreMVCCRUD.Models;
using AspNetCoreMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVCCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public EmployeesController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
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
            return RedirectToAction("Add");
            //            return View(addEmployeeRequest);
        }
    }
}
