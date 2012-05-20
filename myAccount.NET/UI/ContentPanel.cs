﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using myAccount.NET.Logic;

namespace myAccount.NET.UI
{
    class ContentPanel: Grid, IObserver
    {
        const string BACKGROUND_SOURCE = @"images/myAccount-big.png";
        const string NOT_IMPLEMENTED_SOURCE = @"images/not-implemented.png";

        private Context context;
        public ContentPanel(Context context)
        {
            this.context = context;
            Init();
            context.subject.registerObserver(this);
        }

        private void Init() { 
            
        }

        public void notify(ISubject subject) {
            ShowPage(context.actualAction);
        }

        public void ShowPage(string page) {
            this.Children.RemoveRange(0, 10);
            switch (page) {
                case Context.MAIN:
                    ShowMain();
                    break;
                case Context.PAYMENT:
                    ShowPayment();
                    break;
                default:
                    ShowNotImplemented();
                    break;
            }
        }

        private void ShowMain() {
            Image image = GetBackgroundImage(BACKGROUND_SOURCE);
            //Grid.SetRow(image, 2);
            Children.Add(image);
        }

        private void ShowPayment() {
            AddForm addPayment = new AddForm();
            Children.Add(addPayment);

            Map paymentMap = new Map();
            Grid.SetRow(paymentMap, 2);
            Children.Add(paymentMap);
        }

        private void ShowNotImplemented() {
            Image image = GetBackgroundImage(NOT_IMPLEMENTED_SOURCE);
            //Grid.SetRow(image, 2);
            Children.Add(image);
        }


        private Image GetBackgroundImage(String source) {
            // Open a Stream and decode a PNG image
            string url = context.basePath + "/" + source;
            Stream imageStreamSource = new FileStream(url, FileMode.Open, FileAccess.Read, FileShare.Read);
            PngBitmapDecoder decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource bitmapSource = decoder.Frames[0];

            Image image = new Image();
            //BitmapImage imageSource = new BitmapImage();
            //Uri uri = new Uri(url);
            //imageSource.UriSource = uri;
            //imageSource.EndInit();
            image.Source = bitmapSource;
            image.Width = 200;
            image.Height = 200;
            return image;
        }
    }


}
