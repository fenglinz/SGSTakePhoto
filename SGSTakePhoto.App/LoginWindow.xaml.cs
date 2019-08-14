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
using System.Windows.Shapes;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        #region 密码框改变

        /// <summary>
        /// 密码框改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordtext = (PasswordBox)sender;
            PasswordBoxBindingHelper.SetPasswordBoxSelection(passwordtext, passwordtext.Password.Length + 1, passwordtext.Password.Length + 1);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
            };

            main.Show();

            this.Close();
        }
    }
}
