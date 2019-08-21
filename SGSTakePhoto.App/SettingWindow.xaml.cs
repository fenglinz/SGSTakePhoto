using SGSTakePhoto.Infrastructure;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : UserControl
    {
        #region 属性

        /// <summary>
        /// UserConfig
        /// </summary>
        private ObservableCollection<UserConfig> userConfigs;

        /// <summary>
        /// UserServices
        /// </summary>
        private UserServices userServices;

        /// <summary>
        /// SelectedItem
        /// </summary>
        private UserConfig SelectedItem => dgUserConfig.SelectedItem as UserConfig;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SettingWindow()
        {
            InitializeComponent();
            userServices = new UserServices();
        }

        /// <summary>
        /// SettingWindow_Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var result = userServices.GetList("SELECT * FROM [UserConfig]");
            if (result.Success)
            {
                userConfigs = result.Datas;
            }

            dgUserConfig.ItemsSource = userConfigs;
        }

        #endregion

        #region 选中数据行

        /// <summary>
        /// 选中数据行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgUserConfig_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItem == null) return;
            UserConfigModule module = new UserConfigModule(SelectedItem);
            App.CurrentWindow.brMain.Child = module;
        } 

        #endregion
    }
}
