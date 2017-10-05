using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly List<string> s_Domains = new List<string>
                                                             {
                                                                 "www.google.com",
                                                                 "www.bing.com",
                                                                 "www.oreilly.com",
                                                                 "www.simple-talk.com",
                                                                 "www.microsoft.com",
                                                                 "www.reddit.com",
                                                                 "www.baidu.com",
                                                                 "www.bbc.co.uk"
                                                             };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            foreach (string domain in s_Domains)
            {
                AddAFavicon(domain);
            }
        }

        private void btnEAP_Click(object sender, RoutedEventArgs e)
        {
            foreach (string domain in s_Domains)
            {
                AddAFaviconEAP(domain);
            }
        }

        private void btnAsync_Click(object sender, RoutedEventArgs e)
        {
            foreach (string domain in s_Domains)
            {
                AddAFaviconAsync(domain);
            }
        }

        private void AddAFavicon(string domain)
        {
            WebClient webClient = new WebClient();
            byte[] bytes = webClient.DownloadData("http://" + domain + "/favicon.ico");
            Image imageControl = MakeImageControl(bytes);
            m_WrapPanel.Children.Add(imageControl);
        }

        private void AddAFaviconEAP(string domain)
        {
            WebClient webclient = new WebClient();
            webclient.DownloadDataCompleted += OnWebClientOnDownloadDataCompleted;
            webclient.DownloadDataAsync(new Uri("http://" + domain + "/favicon.ico"));
        }

        private void OnWebClientOnDownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            Image imageContrl = MakeImageControl(e.Result);
            m_WrapPanel.Children.Add(imageContrl);
        }

        private async void AddAFaviconAsync(string domain)
        {
            WebClient webClient = new WebClient();
            byte[] bytes = await webClient.DownloadDataTaskAsync("http://" + domain + "/favicon.ico");
            Image imageControl = MakeImageControl(bytes);
            m_WrapPanel.Children.Add(imageControl);
        }

        private Image MakeImageControl(byte[] imageBytes)
        {
            var bitmapImage = new BitmapImage();

            using (var memoryStream = new MemoryStream(imageBytes))
            {
                memoryStream.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.UriSource = null;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
            }

            bitmapImage.Freeze();

            Image imageControl = new Image();
            imageControl.Source = bitmapImage;
            imageControl.Height = 32;
            imageControl.Width = 32;

            return imageControl;
        }
    }
}
