using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace SGSTakePhoto.App
{
    /// <summary>
    /// /
    /// </summary>
    public static class ImageDecoder
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty SourceProperty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string GetSource(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("Image");
            }
            return (string)image.GetValue(ImageDecoder.SourceProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="value"></param>
        public static void SetSource(Image image, string value)
        {
            if (image == null)
            {
                throw new ArgumentNullException("Image");
            }
            image.SetValue(ImageDecoder.SourceProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        static ImageDecoder()
        {
            ImageDecoder.SourceProperty = DependencyProperty.RegisterAttached("Source", typeof(string), typeof(ImageDecoder), new PropertyMetadata(new PropertyChangedCallback(ImageDecoder.OnSourceWithSourceChanged)));
            ImageQueue.OnComplate += new ImageQueue.ComplateDelegate(ImageDecoder.ImageQueue_OnComplate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="u"></param>
        /// <param name="b"></param>
        private static void ImageQueue_OnComplate(Image i, string u, BitmapImage b)
        {
            //System.Windows.MessageBox.Show(u);
            string source = ImageDecoder.GetSource(i);
            if (source == u.ToString())
            {
                i.Source = b;
                Storyboard storyboard = new Storyboard();
                DoubleAnimation doubleAnimation = new DoubleAnimation(0.0, 1.0, new Duration(TimeSpan.FromMilliseconds(500.0)));
                Storyboard.SetTarget(doubleAnimation, i);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity", new object[0]));
                storyboard.Children.Add(doubleAnimation);
                storyboard.Begin();
                if (i.Parent is Grid)
                {
                    Grid grid = i.Parent as Grid;
                    foreach (var c in grid.Children)
                    {
                        if (c is WaitingProgress && c != null)
                        {
                            (c as WaitingProgress).Stop();
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private static void OnSourceWithSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ImageQueue.Queue((Image)o, (string)e.NewValue);
        }
    }
}
