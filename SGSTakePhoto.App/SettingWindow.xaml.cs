using SGSTakePhoto.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : UserControl
    {
        public UIElement parentContiner;
        private ObservableCollection<UserConfig> userConfigs;

        /// <summary>
        /// 
        /// </summary>
        public SettingWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            userConfigs = new ObservableCollection<UserConfig>
            {
                new UserConfig
                {
                    Id="1",UserName="admin",Organization="VOC",ExecutionSystem="SLIM",DefaultWidth=1920,DefaultHeight=1080,DefaultDPI=96
                }
            };

            dgUserConfig.ItemsSource = userConfigs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgUserConfig_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserConfig model = dgUserConfig.SelectedItem as UserConfig;
            if (model == null) return;
            if (!CommonHelper.UserControls.ContainsKey("UserConfig"))
            {
                UserConfigModule module = new UserConfigModule(model);
                CommonHelper.MainWindow.brMain.Child = module;
                CommonHelper.UserControls.Add("UserConfig", module);
            }
            else
            {
                CommonHelper.MainWindow.brMain.Child = CommonHelper.UserControls["UserConfig"];
            }
        }
    }
}
