using SGSTakePhoto.Infrastructure;
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

namespace SGSTakePhoto.App
{
    /// <summary>
    /// UserConfigModule.xaml 的交互逻辑
    /// </summary>
    public partial class UserConfigModule : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userConfig"></param>
        public UserConfigModule(UserConfig userConfig)
        {
            InitializeComponent();
            dgUserConfig.DataContext = userConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            CommonHelper.MainWindow.brMain.Child = CommonHelper.MainWindow.settingModule;
            //CommonHelper.UserControls.Remove("UserConfig");
        }
    }
}
