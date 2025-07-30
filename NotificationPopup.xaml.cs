using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace NotificationApp
{
    public partial class NotificationPopup : Window
    {
        private DispatcherTimer autoCloseTimer;
        private Storyboard slideInAnimation;
        private Storyboard slideOutAnimation;

        public NotificationPopup()
        {
            InitializeComponent();
            LoadAnimations();
            SetupAutoClose();
            PositionWindow();
            this.Loaded += NotificationPopup_Loaded;
        }

        public NotificationPopup(string appName, string title, string message, string actionText = null, string imagePath = null) : this()
        {
            AppNameText.Text = appName;
            TitleText.Text = title;
            MessageText.Text = message;
            
            if (!string.IsNullOrEmpty(actionText))
            {
                ActionText.Text = actionText;
                ActionText.Visibility = Visibility.Visible;
            }
            else
            {
                ActionText.Visibility = Visibility.Collapsed;
            }

            SetNotificationImage(imagePath);
        }

        private void LoadAnimations()
        {
            slideInAnimation = FindResource("SlideInAnimation") as Storyboard;
            slideOutAnimation = FindResource("SlideOutAnimation") as Storyboard;
            
            if (slideOutAnimation != null)
            {
                slideOutAnimation.Completed += (s, e) => this.Close();
            }
        }

        private void SetupAutoClose()
        {
            autoCloseTimer = new DispatcherTimer();
            autoCloseTimer.Interval = TimeSpan.FromSeconds(5); // Auto close after 5 seconds
            autoCloseTimer.Tick += (s, e) => {
                autoCloseTimer.Stop();
                CloseNotification();
            };
        }

        private void PositionWindow()
        {
            // Position at bottom right of screen
            var workingArea = Screen.PrimaryScreen.WorkingArea;
            this.Left = workingArea.Right - this.Width - 20;
            this.Top = workingArea.Bottom - this.Height - 20;
        }

        private void NotificationPopup_Loaded(object sender, RoutedEventArgs e)
        {
            // Start slide-in animation
            slideInAnimation?.Begin(this);
            
            // Start auto-close timer
            autoCloseTimer?.Start();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseNotification();
        }

        private void ActionText_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Handle action link click
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://docs.telerik.com/devtools/wpf/",
                UseShellExecute = true
            });
            
            CloseNotification();
        }

        private void CloseNotification()
        {
            autoCloseTimer?.Stop();
            
            if (slideOutAnimation != null)
            {
                slideOutAnimation.Begin(this);
            }
            else
            {
                this.Close();
            }
        }

        // Method to pause auto-close when mouse is over
        protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            autoCloseTimer?.Stop();
        }

        // Method to resume auto-close when mouse leaves
        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            autoCloseTimer?.Start();
        }

        public void SetNotificationImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                try
                {
                    // Handle different image sources
                    if (imagePath.StartsWith("http://") || imagePath.StartsWith("https://"))
                    {
                        // Web URL
                        NotificationImage.Source = new BitmapImage(new Uri(imagePath));
                    }
                    else if (imagePath.StartsWith("pack://"))
                    {
                        // Resource URI
                        NotificationImage.Source = new BitmapImage(new Uri(imagePath));
                    }
                    else
                    {
                        // File path
                        NotificationImage.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
                    }
                    
                    ImageContainer.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    // If image loading fails, hide the image container
                    ImageContainer.Visibility = Visibility.Collapsed;
                    System.Diagnostics.Debug.WriteLine($"Failed to load notification image: {ex.Message}");
                }
            }
            else
            {
                ImageContainer.Visibility = Visibility.Collapsed;
            }
        }

        public void SetNotificationImage(BitmapImage bitmapImage)
        {
            if (bitmapImage != null)
            {
                NotificationImage.Source = bitmapImage;
                ImageContainer.Visibility = Visibility.Visible;
            }
            else
            {
                ImageContainer.Visibility = Visibility.Collapsed;
            }
        }

        public void SetNotificationImage(ImageSource imageSource)
        {
            if (imageSource != null)
            {
                NotificationImage.Source = imageSource;
                ImageContainer.Visibility = Visibility.Visible;
            }
            else
            {
                ImageContainer.Visibility = Visibility.Collapsed;
            }
        }
    }
}
