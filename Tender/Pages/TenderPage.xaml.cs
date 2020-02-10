﻿using System;
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
    /// Логика взаимодействия для TenderPage.xaml
    /// </summary>
    public partial class TenderPage : Page
    {
        TenderDBEntities db = new TenderDBEntities();
        public TenderPage()
        {
            InitializeComponent();
            TenderList.ItemsSource = db.Tender.ToList();
        }
    }
}
