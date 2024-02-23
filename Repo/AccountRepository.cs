
using BO.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRepository
{
    public class AccountRepository : IAccountRepository
    {
        public bool IsValidLogin(string email, string password)=>AccountDAO.Instance.IsValidLogin(email, password);
        public int GetRole(string email)=>AccountDAO.Instance.GetRole(email);
        public StaffAccount GetStaffAccount(string id)=>AccountDAO.Instance.GetStaffAccount(id);

        public List<StaffAccount> GetStaffAccounts()=>AccountDAO.Instance.GetStaffAccounts();

        public bool AddStaffAccount(StaffAccount account)=>AccountDAO.Instance.AddStaffAccount(account);

        public bool DeleteStaffAccount(string id) => AccountDAO.Instance.DeleteStaffAccount(id);
        public bool EditStaffAccount(StaffAccount updatedStaffAccount) => AccountDAO.Instance.EditStaffAccount(updatedStaffAccount);

    }
}
