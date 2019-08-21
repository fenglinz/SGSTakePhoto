using SGSTakePhoto.Infrastructure;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
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

        #region 域用户登录

        /// <summary>
        /// 域用户登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
            };
            App.CurrentWindow = main;
            //if (!ValidateInput()) return;
            App.CurrentUser = txtUserName.Text.Trim();
            main.Show();
            this.Close();

            //ActiveDirectoryService directoryService = new ActiveDirectoryService("http://mysoft.com.cn", txtUserName.Text, txtPwd.Password);
            //var result = directoryService.ValidateDomainUser();
            //if (result.Success)
            //{
            //    main.Show();
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show(result.ErrorMessage);
            //}
        }

        /// <summary>
        /// 校验输入是否为空
        /// </summary>
        /// <returns></returns>
        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                MessageBox.Show("Please input user name!", "Notice");
                return false;
            }
            if (string.IsNullOrEmpty(txtPwd.Password))
            {
                MessageBox.Show("Please input password!", "Notice");
                return false;
            }

            return true;
        }

        #endregion
    }
}
