
using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRepository
{
    public interface IAccountRepository
    {
        public StaffAccount GetStaffAccount(string id);
        public List<StaffAccount> GetStaffAccounts();

        public bool AddStaffAccount(StaffAccount account);

        public bool DeleteStaffAccount(string id);
        public bool EditStaffAccount(StaffAccount updatedStaffAccount);
        public bool IsValidLogin(string email, string password);
        public int GetRole(string email);
    }
}
