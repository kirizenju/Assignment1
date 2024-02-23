using HospitalRepository;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAccountRepository _accountRepository;

        public MainWindow()
        {
            InitializeComponent();
            _accountRepository = new AccountRepository();
        }



        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            string email = txtEmail.Text;
            string password = txtPass.Password;

            try
            {
                
                bool isValidLogin = _accountRepository.IsValidLogin(email, password);

                if (isValidLogin)
                {
                
                    int role = _accountRepository.GetRole(email);

         
                    switch (role)
                    {
                        case 0:
                            Admin adminWindow = new Admin();
                            adminWindow.Show();
                            Close();
                            break;
                        case 1:
                            Manager managerWindow= new Manager();
                            managerWindow.Show();
                            Close();
                            break;
                        case 2:
                            User user = new User();
                            user.Show();
                            Close();
                            
                            break;
                        default:
                            MessageBox.Show("Unknown role.");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid email or password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

    }
}
