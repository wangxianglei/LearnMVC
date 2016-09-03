using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.BusinessEntities;
using WebApplication1.BusinessLayer;
using WebApplication1.Filters;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class BulkUploadController : AsyncController
    {
        // GET: BulkUpload
        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult Index()
        {
            return View(new FileUploadViewModel());
        }

        [AdminFilter]
        public async Task<ActionResult> Upload(FileUploadViewModel vm)
        {
            int thread1 = Thread.CurrentThread.ManagedThreadId;
            //List<Employee> employees = GetEmployees(vm);
            List<Employee> employees = await Task.Factory.StartNew<List<Employee>>(() => GetEmployees(vm));
            int thread2 = Thread.CurrentThread.ManagedThreadId;
            EmployeeBusinessLayer bl = new EmployeeBusinessLayer();
            bl.UploadEmployee(employees);
            return RedirectToAction("Index", "Employee");
        }

        private List<Employee> GetEmployees(FileUploadViewModel vm)
        {
            List<Employee> employees = new List<Employee>();
            StreamReader reader = new StreamReader(vm.FileUpload.InputStream);
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                Employee employee = new Employee();
                employee.FirstName = values[0];
                employee.LastName = values[1];
                employee.Salary = int.Parse(values[2]);
                employees.Add(employee);
            }

            return employees;
        }
    }
}