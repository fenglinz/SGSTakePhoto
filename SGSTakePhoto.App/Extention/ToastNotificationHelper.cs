using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSTakePhoto.App.Extention
{
    /// <summary>
    /// 通知类
    /// </summary>
    public class ToastNotificationHelper
    {
        /// <summary>
        /// 
        /// </summary>
        //public void ShowMessage()
        //{
        //    ToastContent toastContent = new ToastContent();
        //    toastContent.Scenario = ToastScenario.Reminder;

        //    //<visual>
        //    ToastVisual toastVisual = new ToastVisual();

        //    //<text>
        //    ToastText toastTitle = new ToastText();
        //    toastTitle.Text = "Hello World .";

        //    ToastText toastTitle2 = new ToastText();
        //    toastTitle2.Text = "This is the 5 Example !";
        //    //</text>

        //    //<image>
        //    ToastAppLogo toastAppLogo = new ToastAppLogo();
        //    toastAppLogo.Source = new ToastImageSource("/Assets/Images/photo.jpg");

        //    ToastImage toastImageInline = new ToastImage();
        //    toastImageInline.Source = new ToastImageSource("/Assets/Images/demo.jpg");
        //    //</image>

        //    toastVisual.BodyTextLine1 = toastTitle;
        //    toastVisual.BodyTextLine2 = toastTitle2;
        //    toastVisual.AppLogoOverride = toastAppLogo;
        //    toastVisual.InlineImages.Add(toastImageInline);
        //    //</visual>

        //    //<actions>
        //    ToastActionsCustom toastActions = new ToastActionsCustom();

        //    ToastTextBox toastTextBox = new ToastTextBox("replyTextBox");
        //    toastTextBox.PlaceholderContent = "输入回复消息";

        //    ToastButton replyButton = new ToastButton("回复", "reply");
        //    replyButton.TextBoxId = "replyTextBox";

        //    toastActions.Inputs.Add(toastTextBox);
        //    toastActions.Buttons.Add(replyButton);
        //    //</actions>

        //    //<audio>
        //    ToastAudio toastAudio = new ToastAudio();
        //    toastAudio.Src = new Uri("ms-winsoundevent:Notification.Default");
        //    //</audio>

        //    toastContent.Visual = toastVisual;
        //    toastContent.Actions = toastActions;
        //    toastContent.Audio = toastAudio;

        //    ToastNotification toastNotification = new ToastNotification(toastContent.GetXml());
        //    ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        //}
    }
}
