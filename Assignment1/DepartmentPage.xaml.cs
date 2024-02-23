using BO.Models;
using HospitalRepository;
using Microsoft.Data.SqlClient;
using Repo;
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
    /// Interaction logic for DepartmentPage.xaml
    /// </summary>
    public partial class DepartmentPage : Window
    {
        private readonly IDepartmentRepository _departmentRepository;
        private ObservableCollection<Department> _departments;
        private CollectionView _collectionView;
        private int _pageSize = 10;
        private int _currentPageIndex = 0;
        public DepartmentPage()
        {
            InitializeComponent();
            _departmentRepository = new DepartmentRepository();
            //LoadData();
        }
        //private void LoadData()
        //{
        //    _departments = new ObservableCollection<Department>(_departmentRepository.GetDepartments());
        //    _collectionView = new CollectionView(_departments);     


        //    dtg_Staff.ItemsSource = _collectionView;

        //    Paginate();
        //}
        private void MainGUI_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dtg_Staff.ItemsSource = _departmentRepository.GetDepartments();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something is wrong !");
            }
        }
        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {

            Department department = new Department();
            department.DepartmentName = txt_Name.Text;
            department.DepartmentLocation = txt_Location.Text;
            department.TelephoneNumber = txt_TelephoneNumber.Text ;
            department.ShortDescription = txt_Description.Text;

            try
            {
                bool isSuccess = _departmentRepository.AddDepartment(department);
                if (isSuccess)
                {
                    MessageBox.Show("Insert Success!");
                    _departmentRepository.AddDepartment(department);
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
            txt_Id.Text = string.Empty;
            txt_Name.Text = string.Empty;
            txt_Location.Text = string.Empty;
            txt_TelephoneNumber.Text = string.Empty;
            txt_Description.Text=string.Empty;
        }
        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            Department selectedDepartment = dtg_Staff.SelectedItem as Department;

            if (selectedDepartment == null)
            {
                MessageBox.Show("Please select an account to delete.");
                return;
            }

            
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this account?", "Confirm Deletion", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                bool isSuccess = _departmentRepository.DeleteDepartment(selectedDepartment.DepartmentId);

                if (isSuccess)
                {
                    MessageBox.Show("Account deleted successfully.");
                 
                    dtg_Staff.ItemsSource = _departmentRepository.GetDepartments();
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
                Department selectedDepartment = (Department)dtg_Staff.SelectedItem;
                txt_Id.Text=selectedDepartment.DepartmentId.ToString();
                txt_Name.Text = selectedDepartment.DepartmentName;
                txt_Location.Text = selectedDepartment.DepartmentLocation;
                txt_TelephoneNumber.Text = selectedDepartment.TelephoneNumber;
                txt_Description.Text = selectedDepartment.ShortDescription;
            }
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_Staff.SelectedItem == null)
            {
                MessageBox.Show("Please select a Department to edit.");
                return;
            }

            Department selectedDepartment = (Department)dtg_Staff.SelectedItem;

         
            selectedDepartment.DepartmentName = txt_Name.Text;
            selectedDepartment.DepartmentLocation = txt_Location.Text;
            selectedDepartment.TelephoneNumber = txt_TelephoneNumber.Text;
            selectedDepartment.ShortDescription = txt_Description.Text;


            bool isSuccess = _departmentRepository.EditDepartment(selectedDepartment);
            if (isSuccess)
            {
                MessageBox.Show("department updated successfully.");
                dtg_Staff.ItemsSource = _departmentRepository.GetDepartments();
            }
            else
            {
                MessageBox.Show("Failed to update department.");
            }
        }
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
           
            if (!(Keyboard.IsKeyDown(Key.D0) || Keyboard.IsKeyDown(Key.D1) || Keyboard.IsKeyDown(Key.D2) || Keyboard.IsKeyDown(Key.D3) ||
                  Keyboard.IsKeyDown(Key.D4) || Keyboard.IsKeyDown(Key.D5) || Keyboard.IsKeyDown(Key.D6) || Keyboard.IsKeyDown(Key.D7) ||
                  Keyboard.IsKeyDown(Key.D8) || Keyboard.IsKeyDown(Key.D9) || e.Key == Key.Back || e.Key == Key.Delete))
            {
                e.Handled = true; 
            }
        }
        private void dtg_Staff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtg_Staff.SelectedItem != null)
            {

                Department selectedDepartment = (Department)dtg_Staff.SelectedItem;

              
                txt_Id.Text =selectedDepartment.DepartmentId.ToString();
                txt_Name.Text = selectedDepartment.DepartmentName;
                txt_TelephoneNumber.Text= selectedDepartment.TelephoneNumber;
                txt_Location.Text = selectedDepartment.DepartmentLocation;
            }
        }
        private void Paginate()
        {
            if (_collectionView != null)
            {
                _collectionView.Filter = null; 

         
                int startIndex = _currentPageIndex * _pageSize;
                int endIndex = startIndex + _pageSize;


                _collectionView.Filter = (object item) =>
                {
                    int index = _collectionView.IndexOf(item);
                    return index >= startIndex && index < endIndex;
                };
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPageIndex++;
            Paginate();
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPageIndex--;
            Paginate();
        }
        private void btn_LogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
