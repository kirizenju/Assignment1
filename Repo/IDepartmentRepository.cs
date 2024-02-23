using BO.Models;

namespace Repo
{
    public interface IDepartmentRepository
    {
        public Department GetDepartment(int id);
        public List<Department> GetDepartments();
        public bool AddDepartment(Department department);
        public bool DeleteDepartment(int id);
        public bool EditDepartment(Department updatedDepartment);
    }
}