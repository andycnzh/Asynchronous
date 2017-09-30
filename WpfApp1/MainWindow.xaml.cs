﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            string[] domains = new string[3] { "www.bing.com", "www.google.com", "www.baidu.com" };
            for (int i = 0; i < domains.Length; i++)
            {
                AddAFavicon(domains[i]);
            }
        }

        private void btnEAP_Click(object sender, RoutedEventArgs e)
        {
            string[] domains = new string[3] { "www.bing.com", "www.google.com", "www.baidu.com" };
            for (int i = 0; i < domains.Length; i++)
            {
                AddAFaviconEAP(domains[i]);
            }
        }

        private void btnAsync_Click(object sender, RoutedEventArgs e)
        {
            string[] domains = new string[3] { "www.bing.com", "www.google.com", "www.baidu.com" };
            for (int i = 0; i < domains.Length; i++)
            {
                AddAFaviconAsync(domains[i]);
            }
        }

        private void AddAFavicon(string domain)
        {
            WebClient webClient = new WebClient();
            byte[] bytes = webClient.DownloadData("http://" + domain + "/favicon.ico");
            Image imageControl = MakeImageControl(bytes);
            WrapPanel1.Children.Add(imageControl);
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
            WrapPanel1.Children.Add(imageContrl);
        }

        private async void AddAFaviconAsync(string domain)
        {
            WebClient webClient = new WebClient();
            byte[] bytes = await webClient.DownloadDataTaskAsync("http://" + domain + "/favicon.ico");
            Image imageControl = MakeImageControl(bytes);
            WrapPanel1.Children.Add(imageControl);
        }

        private Image MakeImageControl(byte[] imageBytes)
        {
            var image = new BitmapImage();

            using (var memoryStream = new MemoryStream(imageBytes))
            {
                memoryStream.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = memoryStream;
                image.EndInit();
            }

            image.Freeze();

            ImageSource imgSource = image;

            Image img = new Image();
            img.Source = imgSource;

            //Task.Delay((new Random()).Next(1000,3000));
            //Thread.Sleep((new Random()).Next(1000, 3000));

            // DO time-costing calcuation, wait several seconds.
            //FibonacciCalc fc = new FibonacciCalc();
            //for (int i = 1; i < 40; i++)
            //{
            //    fc.RecurrenceCalc(i);
            //}

            return img;
        }
    }
}
