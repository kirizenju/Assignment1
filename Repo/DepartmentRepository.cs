using BO.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class DepartmentRepository : IDepartmentRepository
    {
        bool IDepartmentRepository.AddDepartment(Department department) => DepartmentDAO.Instance.AddDepartment(department);

        bool IDepartmentRepository.DeleteDepartment(int id)=>DepartmentDAO.Instance.DeleteDepartment(id);

        bool IDepartmentRepository.EditDepartment(Department updatedDepartment)=>DepartmentDAO.Instance.EditDepartment(updatedDepartment);

        Department IDepartmentRepository.GetDepartment(int id)=>DepartmentDAO.Instance.GetDepartment(id);


        List<Department> IDepartmentRepository.GetDepartments()=>DepartmentDAO.Instance.GetDepartments();
 
    }
}
