using BO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance = null;
        private readonly HospitalManagementDBContext dbContext = null;
        public AccountDAO()
        {
            if (dbContext == null)
            {
                dbContext = new HospitalManagementDBContext();
            }
        }
        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }



        public bool IsValidLogin(string email, string password)
        {
 
            var account = dbContext.StaffAccounts.FirstOrDefault(a => a.Hremail == email && a.Hrpassword == password);

            return account != null;
        }

      
        public int GetRole(string email)
        {
            var account = dbContext.StaffAccounts.FirstOrDefault(a => a.Hremail == email);

            return account?.StaffRole ?? -1; 
        }
        public StaffAccount GetStaffAccount(string id)
        {
            return dbContext.StaffAccounts.FirstOrDefault(m => m.HraccountId.Equals(id));
        }
        public List<StaffAccount> GetStaffAccounts()
        {
            return dbContext.StaffAccounts.ToList();
        }

        public bool AddStaffAccount(StaffAccount account)
        {
            try
            {
                account.HraccountId = GenerateNewStaffAccountId();
                dbContext.Entry(account).State = EntityState.Added;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding staff account: {ex.Message}");
                return false;
            }
        }
        public bool DeleteStaffAccount(string id)
        {
            try
            {
                StaffAccount staff = GetStaffAccount(id);
                if (staff != null)
                {
                    dbContext.Remove(staff);
                    dbContext.SaveChanges();
                    return true; // Deletion successful
                }
                return false; // Staff account not found
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting staff account: {ex.Message}");
                return false; // Error occurred during deletion
            }
        }

        public bool EditStaffAccount(StaffAccount updatedStaffAccount)
        {
            try
            {
                var existingStaffAccount = GetStaffAccount(updatedStaffAccount.HraccountId);
                if (existingStaffAccount != null)
                {
                    existingStaffAccount.Hremail = updatedStaffAccount.Hremail;
                    existingStaffAccount.Hrfullname = updatedStaffAccount.Hrfullname;
                    existingStaffAccount.Hrpassword = updatedStaffAccount.Hrpassword;
                    existingStaffAccount.StaffRole = updatedStaffAccount.StaffRole;

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
                Console.WriteLine($"Error updating staff account: {ex.Message}");
                return false;
            }
        }

        private string GenerateNewStaffAccountId()
        {
    
            var maxId = dbContext.StaffAccounts.Max(s => s.HraccountId);

            int nextId = int.Parse(maxId.Substring(2)) + 1;

            string newId = $"SA{nextId:D4}";

            return newId;
        }
    }
}

