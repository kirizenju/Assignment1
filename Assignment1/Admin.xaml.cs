using BO.Models;
using HospitalRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Assignment1
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        private readonly IAccountRepository _accountRepository;
        public string[] roles { get; set; }
        public Admin()
        {
            InitializeComponent();
            _accountRepository = new AccountRepository();
           roles = new string[] { "Admin", "Manager", "User" };
        }
        private void MainGUI_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dtg_Staff.ItemsSource = _accountRepository.GetStaffAccounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something is wrong !");
            }
            cbx_Role.ItemsSource = roles;
        }


        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            if (cbx_Role.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txt_Email.Text) || string.IsNullOrWhiteSpace(txt_FullName.Text) || string.IsNullOrWhiteSpace(txt_Password.Password))
            {
                MessageBox.Show("Please fill in all fields and select a role.");
                return;
            }

            StaffAccount staff = new StaffAccount();
            staff.Hremail = txt_Email.Text;
            staff.Hrfullname = txt_FullName.Text;
            staff.Hrpassword = txt_Password.Password;
            staff.StaffRole = cbx_Role.SelectedIndex;

            try
            {
                bool isSuccess = _accountRepository.AddStaffAccount(staff);
                if (isSuccess)
                {
                    MessageBox.Show("Insert Success!");
                    _accountRepository.AddStaffAccount(staff);
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Insert Fail!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void ClearInputFields()
        {
            txt_Email.Text = string.Empty;
            txt_FullName.Text = string.Empty;
            txt_Password.Password = string.Empty;
            cbx_Role.SelectedIndex = -1;
        }
        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            StaffAccount selectedAccount = dtg_Staff.SelectedItem as StaffAccount;

            if (selectedAccount == null)
            {
                MessageBox.Show("Please select an account to delete.");
                return;
            }

            if (selectedAccount.StaffRole==0)
            {
                MessageBox.Show("The admin account cannot be deleted.");
                return;
            }


            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this account?", "Confirm Deletion", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                bool isSuccess = _accountRepository.DeleteStaffAccount(selectedAccount.HraccountId);

                if (isSuccess)
                {
                    MessageBox.Show("Account deleted successfully.");
          
                    dtg_Staff.ItemsSource = _accountRepository.GetStaffAccounts();
                }
                else
                {
                    MessageBox.Show("Failed to delete account.");
                }
            }
        }

        private void Dtg_Staff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtg_Staff.SelectedItem != null)
            {
                StaffAccount selectedStaff = (StaffAccount)dtg_Staff.SelectedItem;
                txt_Email.Text = selectedStaff.Hremail;
                txt_FullName.Text = selectedStaff.Hrfullname;
                txt_Password.Password = selectedStaff.Hrpassword;
                cbx_Role.SelectedIndex = (int)selectedStaff.StaffRole;
            }
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_Staff.SelectedItem == null)
            {
                MessageBox.Show("Please select a staff account to edit.");
                return;
            }

            StaffAccount selectedStaff = (StaffAccount)dtg_Staff.SelectedItem;

 
            selectedStaff.Hremail = txt_Email.Text;
            selectedStaff.Hrfullname = txt_FullName.Text;
            selectedStaff.Hrpassword = txt_Password.Password;
            selectedStaff.StaffRole = cbx_Role.SelectedIndex;

     
            bool isSuccess = _accountRepository.EditStaffAccount(selectedStaff);
            if (isSuccess)
            {
                MessageBox.Show("Staff account updated successfully.");
                dtg_Staff.ItemsSource = _accountRepository.GetStaffAccounts();
            }
            else
            {
                MessageBox.Show("Failed to update staff account.");
            }
        }
        private void dtg_Staff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtg_Staff.SelectedItem != null)
            {
      
                StaffAccount selectedStaff = (StaffAccount)dtg_Staff.SelectedItem;


                txt_Email.Text = selectedStaff.Hremail;
                txt_FullName.Text = selectedStaff.Hrfullname;
                txt_Password.Password = selectedStaff.Hrpassword;
                cbx_Role.SelectedIndex = (int)selectedStaff.StaffRole;
            }
        }


    }
}
