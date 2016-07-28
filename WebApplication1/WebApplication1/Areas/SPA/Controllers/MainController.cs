using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OldViewModel = WebApplication1.ViewModels;
using WebApplication1.ViewModels.SPA;
using WebApplication1.BusinessLayer;
using WebApplication1.BusinessEntities;
using WebApplication1.Filters;

namespace WebApplication1.Areas.SPA.Controllers
{
    public class MainController : Controller
    {
        // GET: SPA/Main
        public ActionResult Index()
        {
            MainViewModel vm = new MainViewModel();
            vm.UserName = User.Identity.Name;
            vm.FooterData = new OldViewModel.FooterViewModel();
            vm.FooterData.CompanyName = "StepByStpeSchool";
            vm.FooterData.Year = DateTime.Now.Year.ToString();

            return View("Index", vm);
        }

        public ActionResult EmployeeList()
        {
            EmployeeListViewModel employeesVM = new EmployeeListViewModel();
            EmployeeBusinessLayer employeeBL = new EmployeeBusinessLayer();
            List<Employee> employees = employeeBL.GetEmployees();

            List<EmployeeViewModel> employeeVMs = new List<EmployeeViewModel>();

            foreach (Employee e in employees)
            {
                EmployeeViewModel vm = new EmployeeViewModel();
                vm.EmployeeName = e.FirstName + " " + e.LastName;
                vm.Salary = e.Salary.Value.ToString("C");

                if (e.Salary > 15000)
                {
                    vm.SalaryColor = "yellow";
                }
                else
                {
                    vm.SalaryColor = "green";
                }

                employeeVMs.Add(vm);
            }

            employeesVM.Employees = employeeVMs;

            return View("EmployeeList", employeesVM);
        }

        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["Admin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}