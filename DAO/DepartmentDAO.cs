using BO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DepartmentDAO
    {
        private static DepartmentDAO instance = null;
        private readonly HospitalManagementDBContext dbContext = null;
        public DepartmentDAO()
        {
            if (dbContext == null)
            {
                dbContext = new HospitalManagementDBContext();
            }
        }
        public static DepartmentDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DepartmentDAO();
                }
                return instance;
            }
        }
        public Department GetDepartment(int id)
        {
            return dbContext.Departments.FirstOrDefault(d => d.DepartmentId == id);
        }
        public List<Department> GetDepartments()
        {
            return dbContext.Departments.ToList();
        }

        public bool AddDepartment(Department department)
        {
            try
            {
                int nextDepartmentId = dbContext.Departments.Max(d => d.DepartmentId) + 1; 
                department.DepartmentId = nextDepartmentId;
                dbContext.Entry(department).State = EntityState.Added;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Department: {ex.Message}");
                return false;
            }
        }
        public bool DeleteDepartment(int id)
        {
            try
            {
                Department department = GetDepartment(id);
                if (department != null)
                {
                    dbContext.Remove(department);
                    dbContext.SaveChanges();
                    return true;
                }
                return false; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deletingDepartment: {ex.Message}");
                return false; 
            }
        }

        public bool EditDepartment(Department updatedDepartment)
        {
            try
            {
                var existingStaffAccount = GetDepartment(updatedDepartment.DepartmentId);
                if (existingStaffAccount != null)
                {
                    existingStaffAccount.DepartmentName= updatedDepartment.DepartmentName;
                    existingStaffAccount.DepartmentLocation = updatedDepartment.DepartmentLocation;
                    existingStaffAccount.TelephoneNumber = updatedDepartment.TelephoneNumber;
                    existingStaffAccount.ShortDescription = updatedDepartment.ShortDescription;

                    dbContext.Update(existingStaffAccount);
                    dbContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updatingDepartment: {ex.Message}");
                return false;
            }
        }
    }
}
