using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        TenderDBEntities db = new TenderDBEntities();
        int proverka;
        public RegPage()
        {
            InitializeComponent();
            textrole.ItemsSource = db.Role.Select(c => c.Name).ToList();
        }

        private void Btnlogin_Click(object sender, RoutedEventArgs e)
        {
            errortext.Text = "";


            if (textmail.Text.Length != 0)
            {
                if (!Regex.IsMatch(textmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    errortext.Text = "Не правильно введен Email!";
                    textmail.Select(0, textmail.Text.Length);
                    textmail.Focus();
                }
            }

            if (textphone.Text.Length != 0)
            {
                if (!Regex.IsMatch(textphone.Text, @"(\+7|8|\b)[\(\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[)\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)"))
                {
                    errortext.Text = "Не правильно введен телефон!";
                    textmail.Select(0, textmail.Text.Length);
                    textmail.Focus();
                }
            }

            if (textINN.Text.Length != 0)
            {
                if(!Regex.IsMatch(textINN.Text, "^([0-9]{10}|[0-9]{12})$"))
                {
                    errortext.Text = "Не правильно введен ИНН!";
                    textINN.Select(0, textINN.Text.Length);
                    textINN.Focus();
                }
            }

            if (textlogin.Text.Length == 0)
            {
                errortext.Text = "Введите данные!";
                textlogin.Focus();
            }
            else if (textpassword.Password.Length == 0)
            {
                errortext.Text = "Введите данные!";
                textpassword.Focus();
            }
            else if (textname.Text.Length == 0)
            {
                errortext.Text = "Введите данные!";
                textname.Focus();
            }
            else if (textrole.Text.Length == 0)
            {
                errortext.Text = "Выберите роль!";
                textrole.Focus();
            }
            else
            {
               string lg = textlogin.Text;
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.TenderDBConnectionString))
                {
                    SqlCommand com = new SqlCommand("SELECT count(*) FROM [User] WHERE Login=@Login", con);
                    com.Parameters.AddWithValue("@Login", lg);
                    con.Open();
                    proverka = (int)com.ExecuteScalar();
                }


                if (proverka > 0)
                {
                    errortext.Text = "Логин уже занят!";
                }
                else
                {
                    db.User.Add(new DataBase.User
                    {
                        Login = textlogin.Text,
                        Password = textpassword.Password,
                        Mail = textmail.Text,
                        INN = textINN.Text,
                        Phone = textphone.Text,
                        Name = textname.Text,
                        RoleId = db.Role.FirstOrDefault(c => c.Name == textrole.Text).Id

                    });
                    db.SaveChanges();
                    errortext.Foreground = Brushes.Green;

                    errortext.Text = "Вы зарегистрировались!";
                }
                
            }
        }
    }
}
