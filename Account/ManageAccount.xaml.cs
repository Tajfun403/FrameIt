using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrameIt.Account
{
    /// <summary>
    /// Interaction logic for ManageAccount.xaml
    /// </summary>
    public partial class ManageAccount : Page
    {
        public ManageAccount()
        {
            InitializeComponent();
        }

        public bool IsRegisteringAccount
        {
            get; set;
        } = false;
    }
}
