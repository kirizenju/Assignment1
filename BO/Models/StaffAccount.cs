using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BO.Models
{
    public partial class StaffAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string HraccountId { get; set; } = null!;
        public string Hrfullname { get; set; } = null!;
        public string Hremail { get; set; } = null!;
        public string? Hrpassword { get; set; }
        public int? StaffRole { get; set; }
    }
}
