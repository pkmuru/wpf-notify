using System.Windows;
using System.Windows.Media.Imaging;
using System;

namespace NotificationApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Example usage - call this method to show a notification
        private void ShowNotificationExample()
        {
            NotificationManager.ShowNotification(
                appName: "Your App Name",
                title: "Notification Title",
                message: "This is the notification message that will be displayed to the user.",
                actionText: "Click here for more details",
                imagePath: "https://via.placeholder.com/64x64/4A90E2/FFFFFF?text=App" // Example web image
            );
        }

        // You can also show multiple notifications with different image sources
        private void ShowMultipleNotifications()
        {
            // Web image URL
            NotificationManager.ShowNotification(
                "Email Client", 
                "New Email Received", 
                "You have received a new email from john@example.com",
                "Open Email",
                "https://via.placeholder.com/64x64/FF6B6B/FFFFFF?text=📧"
            );

            // Local file path (make sure the file exists)
            NotificationManager.ShowNotification(
                "System Update", 
                "Update Available", 
                "A new system update is available for download.",
                "Download Now",
                "/Images/update-icon.png" // Local image path
            );

            // Resource URI (embedded resource)
            NotificationManager.ShowNotification(
                "Calendar", 
                "Meeting Reminder", 
                "You have a meeting in 15 minutes: Project Review",
                "View Calendar",
                "pack://application:,,,/Images/calendar-icon.png" // Resource image
            );
        }

        // Example with BitmapImage
        private void ShowNotificationWithBitmapImage()
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("https://via.placeholder.com/64x64/52C41A/FFFFFF?text=✓");
            bitmap.EndInit();

            NotificationManager.ShowNotification(
                "Task Manager",
                "Task Completed",
                "Your backup task has been completed successfully.",
                "View Details",
                bitmap
            );
        }

        // Example creating a gradient background when no image is provided
        private void ShowNotificationWithoutImage()
        {
            NotificationManager.ShowNotification(
                "System", 
                "No Image Example", 
                "This notification doesn't have an image and will hide the image container.",
                "Learn More"
            );
        }

        // Example button click handlers (add buttons to your MainWindow.xaml to test)
        private void ShowSingleNotification_Click(object sender, RoutedEventArgs e)
        {
            ShowNotificationExample();
        }

        private void ShowMultipleNotifications_Click(object sender, RoutedEventArgs e)
        {
            ShowMultipleNotifications();
        }

        private void CloseAllNotifications_Click(object sender, RoutedEventArgs e)
        {
            NotificationManager.CloseAllNotifications();
        }

        private void ShowNotificationWithImage_Click(object sender, RoutedEventArgs e)
        {
            ShowNotificationWithBitmapImage();
        }

        private void ShowNotificationWithoutImage_Click(object sender, RoutedEventArgs e)
        {
            ShowNotificationWithoutImage();
        }
    }
}
