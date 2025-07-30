using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NotificationApp
{
    public static class NotificationManager
    {
        private static List<NotificationPopup> activeNotifications = new List<NotificationPopup>();
        private const int NotificationSpacing = 10;

        public static void ShowNotification(string appName, string title, string message, string actionText = null, string imagePath = null)
        {
            var notification = new NotificationPopup(appName, title, message, actionText, imagePath);
            
            // Position the notification
            PositionNotification(notification);
            
            // Add to active notifications list
            activeNotifications.Add(notification);
            
            // Remove from list when closed
            notification.Closed += (s, e) => {
                activeNotifications.Remove(notification);
                RepositionNotifications();
            };
            
            // Show the notification
            notification.Show();
        }

        public static void ShowNotification(string appName, string title, string message, string actionText, BitmapImage image)
        {
            var notification = new NotificationPopup(appName, title, message, actionText);
            notification.SetNotificationImage(image);
            
            // Position the notification
            PositionNotification(notification);
            
            // Add to active notifications list
            activeNotifications.Add(notification);
            
            // Remove from list when closed
            notification.Closed += (s, e) => {
                activeNotifications.Remove(notification);
                RepositionNotifications();
            };
            
            // Show the notification
            notification.Show();
        }

        public static void ShowNotification(string appName, string title, string message, string actionText, ImageSource imageSource)
        {
            var notification = new NotificationPopup(appName, title, message, actionText);
            notification.SetNotificationImage(imageSource);
            
            // Position the notification
            PositionNotification(notification);
            
            // Add to active notifications list
            activeNotifications.Add(notification);
            
            // Remove from list when closed
            notification.Closed += (s, e) => {
                activeNotifications.Remove(notification);
                RepositionNotifications();
            };
            
            // Show the notification
            notification.Show();
        }

        private static void PositionNotification(NotificationPopup notification)
        {
            var workingArea = Screen.PrimaryScreen.WorkingArea;
            var rightMargin = 20;
            var bottomMargin = 20;

            // Calculate position based on existing notifications
            var totalHeight = activeNotifications.Sum(n => n.Height + NotificationSpacing);
            
            notification.Left = workingArea.Right - notification.Width - rightMargin;
            notification.Top = workingArea.Bottom - notification.Height - bottomMargin - totalHeight;
        }

        private static void RepositionNotifications()
        {
            var workingArea = Screen.PrimaryScreen.WorkingArea;
            var rightMargin = 20;
            var bottomMargin = 20;
            var currentBottom = workingArea.Bottom - bottomMargin;

            // Reposition all active notifications from bottom up
            for (int i = activeNotifications.Count - 1; i >= 0; i--)
            {
                var notification = activeNotifications[i];
                notification.Top = currentBottom - notification.Height;
                currentBottom -= notification.Height + NotificationSpacing;
            }
        }

        public static void CloseAllNotifications()
        {
            var notifications = activeNotifications.ToList();
            foreach (var notification in notifications)
            {
                notification.Close();
            }
        }
    }
}
