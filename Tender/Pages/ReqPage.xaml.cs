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
    /// Логика взаимодействия для ReqPage.xaml
    /// </summary>
    public partial class ReqPage : Page
    {
        int tendid; // сохраняет id тендера

        public ReqPage(Tenders sel)
        {
            InitializeComponent();

            if (sel != null)
            {
                tendid = sel.Id;
                textINN.Text = AuthPage.AuthUser.INN;
            }
            this.DataContext = sel;
        }

        private void Btnreq_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    Request Newrequest = new Request();
                    Newrequest.INN = textINN.Text;
                    Newrequest.Mail = textmail.Text;
                    Newrequest.Phone = textphone.Text;
                    Newrequest.Condition = Descriptionb.Text;
                    Newrequest.ParticipantId = AuthPage.AuthUser.Id;
                    Newrequest.TenderId = tendid;
                    Newrequest.StatusId = 1;
                    TenderPage.db.Request.Add(Newrequest);

                TenderPage.db.SaveChanges();
                MessageBox.Show("Успешно", "Сообщение", MessageBoxButton.OK);

                textmail.Text = "";
                textphone.Text = "";
                Descriptionb.Text = "";

            }
            catch { MessageBox.Show("Не все данные введены", "Ошибка", MessageBoxButton.OK); }
        }
    }
}
