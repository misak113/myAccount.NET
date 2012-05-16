using System;
using System.Collections.Generic;
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

        public void ShowPage(int page) { 
            switch (page) {
                case Context.MAIN:
                    ShowMain();
                    break;
                default:
                    ShowNotImplemented();
                    break;
            }
        }

        private void ShowMain() {
            Image image = GetBackgroundImage();
            Grid.SetRow(image, 1);
            Children.Add(image);
        }

        private void ShowNotImplemented() { 
            
        }


        private Image GetBackgroundImage() {
            Image image = new Image();
            BitmapImage imageSource = new BitmapImage();
            imageSource.UriSource = new Uri(context.basePath + "/" + BACKGROUND_SOURCE);
            imageSource.EndInit();
            image.Source = imageSource;
            return image;
        }
    }


}
