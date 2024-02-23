using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BO.Models
{
    public partial class Department
    {
        public Department()
        {
            DoctorInformations = new HashSet<DoctorInformation>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string? DepartmentLocation { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? ShortDescription { get; set; }

        public virtual ICollection<DoctorInformation> DoctorInformations { get; set; }
    }
}
