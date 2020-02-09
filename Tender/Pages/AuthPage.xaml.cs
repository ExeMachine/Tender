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
using Tender.DataBase;

namespace Tender.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        TenderDBEntities db = new TenderDBEntities();
        public static User AuthUser = null;

        public AuthPage()
        {
            InitializeComponent();
        }

        private void Btnlogin_Click(object sender, RoutedEventArgs e)
        {
            errortext.Text = "";
            string lg = textlogin.Text.Trim();
            string pw = textpassword.Password.Trim();

            if (lg.Length == 0 || pw.Length == 0 || lg.Length == 0 && pw.Length == 0)
            {
                errortext.Text = "Введите данные!";
                return;
            }

            AuthUser = db.User.ToList().Find(c => c.Login == lg && c.Password == pw);

            if (AuthUser != null)
            {
                errortext.Foreground = Brushes.Green;
                errortext.Text = $"Пользователь {AuthUser.Name} успешно вошел в систему!";
                NavigationService.Navigate(new MenuPage());
            }
            else
            {
                errortext.Text = "Пользователь не найден!";
                textlogin.Text = "";
                textpassword.Password = "";
                return;
            }
        }
    }
}
