using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.BusinessEntities;
using WebApplication1.BusinessLayer;
using WebApplication1.Filters;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        public string GetString()
        {
            return "Hello World";
        }

        //public Customer GetCustomer()
        //{
        //    Customer c = new Customer();
        //    c.CustomerName = "Customer Name";
        //    c.Address = "Äddress 1";

        //    return c;
        //}

        //public ActionResult GetView()
        //{
        //    //return View("MyView");


        //    EmployeeViewModel employeeVM = new EmployeeViewModel();
        //    employeeVM.EmployeeName = employee.FirstName + " " + employee.LastName;
        //    employeeVM.Salary = employee.Salary.ToString("C");
        //    employeeVM.SalaryColor = employee.Salary > 15000 ? "yellow" : "green";
        //    employeeVM.UserName = "Admin";

        //    return View("MyView", employeeVM);
        //}

        [Authorize]
        [HeaderFooterFilter]
        public ActionResult Index()
        {
            EmployeeListViewModel employeeLVM = new EmployeeListViewModel();

            EmployeeBusinessLayer employeeBL = new EmployeeBusinessLayer();
            List<Employee> employees = employeeBL.GetEmployees();

            List<EmployeeViewModel> employeeVMs = new List<EmployeeViewModel>();

            foreach (Employee emp in employees)
            {
                EmployeeViewModel empVM = new EmployeeViewModel();
                empVM.EmployeeName = emp.FirstName + " " + emp.LastName;
                empVM.Salary = emp.Salary.ToString();
                empVM.SalaryColor = emp.Salary > 15000 ? "yellow" : "green";

                employeeVMs.Add(empVM);
            }

            employeeLVM.Employees = employeeVMs;

            return View("Index", employeeLVM);
        }

        //public string SaveEmployee(Employee e)
        //{
        //    return e.FirstName + "|" + e.LastName + "|" + e.Salary;
        //}

        //public ActionResult SaveEmployee([ModelBinder(typeof(EmployeeModelBinder))]Employee e, string btnSave)
        //{
        //    switch (btnSave)
        //    {
        //        case "Save Employee":
        //            return Content(e.FirstName + "|" + e.LastName + "|" + e.Salary);
        //        case "Cancel":
        //            return RedirectToAction("Index");

        //    }

        //    return new EmptyResult();
        //}

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult SaveEmployee(Employee e)
        {
            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer employeeBL = new EmployeeBusinessLayer();
                employeeBL.SaveEmployee(e);
                return RedirectToAction("Index");

            }
            else
            {
                CreateEmployeeViewModel vm = new CreateEmployeeViewModel();
                vm.FirstName = e.FirstName;
                vm.LastName = e.LastName;
                if (e.Salary.HasValue)
                {
                    vm.Salary = e.Salary.ToString();
                }
                else
                {
                    vm.Salary = ModelState["Salary"].Value.AttemptedValue;
                }
                return View("CreateEmployee", vm);
            }
        }

        //public ActionResult SaveEmployee()
        //{
        //    Employee e = new Employee();
        //    e.FirstName = Request.Form["FirstName"];
        //    e.LastName = Request.Form["LastName"];
        //    e.Salary = int.Parse(Request.Form["Salary"]);

        //    return Content(e.FirstName + "|" + e.LastName + "|" + e.Salary);
        //}

        //public ActionResult SaveEmployee(string firstName, string lastName, int Salary)
        //{
        //    Employee e = new Employee();
        //    e.FirstName = firstName;
        //    e.LastName = lastName;
        //    e.Salary = Salary;

        //    return Content(e.FirstName + "|" + e.LastName + "|" + e.Salary);
        //}

        public ActionResult CancelSave()
        {
            return RedirectToAction("Index");
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel createEmployeeVM = new CreateEmployeeViewModel();

            return View("CreateEmployee", createEmployeeVM);
        }

        [NonAction]
        public string SimpleMethod()
        {
            return "I am not an action method";
        }

        public ActionResult Footer()
        {
            FooterViewModel footerVM = new FooterViewModel();
            footerVM.CompanyName = "StepByStepSchool";
            footerVM.Year = DateTime.Now.Year.ToString();

            return View("Footer", footerVM);
        }

        [ChildActionOnly]
        public ActionResult GetAddNewLink()
        {
            bool isAdmin = Convert.ToBoolean(Session["Admin"]);
            if (isAdmin)
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }


    //public class Customer
    //{
    //    public string CustomerName { get; set; }
    //    public string Address { get; set; }

    //    public override string ToString()
    //    {
    //        return this.CustomerName + " " + this.Address;
    //    }
    //}

    public class EmployeeModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            Employee e = new Employee();
            e.FirstName = controllerContext.HttpContext.Request.Form["FName"];
            e.LastName = controllerContext.HttpContext.Request.Form["LName"];
            e.Salary = int.Parse(controllerContext.HttpContext.Request.Form["Salary"]);

            return e;

            //return base.CreateModel(controllerContext, bindingContext, modelType);
        }
    }
}