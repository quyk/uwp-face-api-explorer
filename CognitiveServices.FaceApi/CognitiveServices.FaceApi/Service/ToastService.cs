using System;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;

namespace CognitiveServices.FaceApi.Service
{
    public static class ToastService
    {
        public static void Show(string text, string text1)
        {

            var content = new ToastContent
            {
                Launch = "app-defined-string",
                Visual = new ToastVisual
                {
                    BindingGeneric = new ToastBindingGeneric
                    {
                        Children =
                        {
                            new AdaptiveText
                            {
                                Text = text,
                                HintMaxLines = 1
                            },
                            new AdaptiveText()
                            {
                                Text = text1
                            },
                        }
                    }
                },
                Audio = new ToastAudio
                {
                    Src = new Uri("ms-winsoundevent:Notification.Reminder")
                }
            };

            var toast = new ToastNotification(content.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}