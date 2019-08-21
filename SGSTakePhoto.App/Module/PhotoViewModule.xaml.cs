using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// PhotoViewModule.xaml 的交互逻辑
    /// </summary>
    public partial class PhotoViewModule : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UserControl ParentControl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public PhotoViewModule(string fileName)
        {
            InitializeComponent();
            ImageComparePanel.DataContext = new { PicturePath = fileName };
        }


        /// <summary>
        /// 返回上层目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentWindow.brMain.Child = ParentControl;
        }

        private bool m_IsMouseLeftButtonDown;
        private Point m_PreviousMousePoint;
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            if (rectangle == null) return;
            rectangle.CaptureMouse();
            m_IsMouseLeftButtonDown = true;
            m_PreviousMousePoint = e.GetPosition(rectangle);
        }

        /// <summary>
        /// 鼠标拿起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            if (rectangle == null) return;
            rectangle.ReleaseMouseCapture();
            m_IsMouseLeftButtonDown = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentControl_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            if (rectangle == null)return;
            if (m_IsMouseLeftButtonDown)
                DoImageMove(rectangle, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            TransformGroup group = ImageComparePanel.FindResource("ImageCompareResources") as TransformGroup;
            Debug.Assert(group != null);
            ScaleTransform transform = group.Children[0] as ScaleTransform;
            transform.ScaleX += e.Delta * 0.001;
            transform.ScaleY += e.Delta * 0.001;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="position"></param>
        private void DoImageMove(Rectangle rectangle, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)return;
            TransformGroup group = ImageComparePanel.FindResource("ImageCompareResources") as TransformGroup;
            Debug.Assert(group != null);
            TranslateTransform transform = group.Children[1] as TranslateTransform;
            Point position = e.GetPosition(rectangle);
            transform.X += position.X - m_PreviousMousePoint.X;
            transform.Y += position.Y - m_PreviousMousePoint.Y;
            m_PreviousMousePoint = position;
        }
    }
}
