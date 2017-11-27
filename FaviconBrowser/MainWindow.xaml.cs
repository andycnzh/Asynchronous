//------------------------------------------------------------
// Copyright (c) 2017 AndyCnZh.  All rights reserved.
//------------------------------------------------------------

namespace FaviconBrowser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

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

        private void btnExceptionTest_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Caller());
        }

        private async Task Caller()
        {
            try
            {
                await Thrower();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("Caller - Catch");
            }
        }

        private async Task Thrower()
        {
            await Task.Delay(1000);
            throw new Exception("Thrower");
        }

        private void btnCurlAsync_Click(object sender, RoutedEventArgs e)
        {
            //var bynderContents = await DoCurlAsync();

            // There is a dead lock here
            //var bynderContents1 = DoCurlAsyncNoConfigureContext().Result;
            //MessageBox.Show(bynderContents1.Length.ToString());

            // There is no dead lock here
            var bynderContents2 = DoCurlAsync().Result;
            MessageBox.Show(bynderContents2.Length.ToString());
        }

        private async Task<string> DoCurlAsyncNoConfigureContext()
        {
            using (var httpClient = new HttpClient())
            {
                using (var httpResponse = await httpClient.GetAsync("https://www.bynder.com"))
                {
                    return await httpResponse.Content.ReadAsStringAsync();
                }
            }
        }

        private async Task<string> DoCurlAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var httpResponse = await httpClient.GetAsync("https://www.bynder.com").ConfigureAwait(false))
                {
                    return await httpResponse.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
