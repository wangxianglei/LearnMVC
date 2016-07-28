using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.BusinessEntities;
using WebApplication1.DataAccessLayer;

namespace WebApplication1.BusinessLayer
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            //Employee employee = new Employee();
            //employee.FirstName = "Xiang Lei";
            //employee.LastName = "Wang";
            //employee.Salary = 6412;

            //Employee employee2 = new Employee();
            //employee2.FirstName = "Juan";
            //employee2.LastName = "Luo";
            //employee2.Salary = 100000;

            //employees.Add(employee);
            //employees.Add(employee2);

            SalesERPDAL dal = new SalesERPDAL();
            return dal.Employees.ToList<Employee>();
        }

        public Employee SaveEmployee(Employee e)
        {
            SalesERPDAL dal = new SalesERPDAL();
            dal.Employees.Add(e);
            dal.SaveChanges();

            return e;
        }

        public void UploadEmployee(List<Employee> employees)
        {
            SalesERPDAL dal = new SalesERPDAL();
            dal.Employees.AddRange(employees);
            dal.SaveChanges();
        }

        public bool IsValidUser(UserDetails u)
        {
            if (u != null && u.UserName == "Admin" && u.Password == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public UserStatus GetUserValidity(UserDetails u)
        {
            if (u.UserName == "Admin" && u.Password == "Admin")
            {
                return UserStatus.AuthenciatedAdmin;
            }
            else if (u.UserName == "Wang" && u.Password == "Wang")
            {
                return UserStatus.AuthenciatedUser;
            }
            else
            {
                return UserStatus.NonAuthenciatedUser;
            }
        }
    }
}