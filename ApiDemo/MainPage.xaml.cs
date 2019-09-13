using DemoLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ApiDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int maxNumber = 0;
        private int currentNumber = 0;
        public MainPage()
        {
            this.InitializeComponent();
            ApiHelper.InitializeClient();
            nextImageButton.IsEnabled = false;

        }
        private async Task LoadImage(int imageNumber = 0)
        {
            var comic = await ComicProcessor.LoadComic(imageNumber);

            if (imageNumber == 0)
            {
                maxNumber = comic.Num;
            }

            currentNumber = comic.Num;

            var uriSource = new Uri(comic.Img, UriKind.Absolute);
            comicImage.Source = new BitmapImage(uriSource);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadImage();
        }
        private async void PreviousImageButton_Click(object sender, RoutedEventArgs e)
        {
            comicImage.Visibility = Visibility.Visible;
            sunInfo.Visibility = Visibility.Collapsed;
            if (currentNumber > 1)
            {
                currentNumber -= 1;
                nextImageButton.IsEnabled = true;
                await LoadImage(currentNumber);

                if (currentNumber == 1)
                {
                    previousImageButton.IsEnabled = false;
                }
            }
        }

        private async void NextImageButton_Click(object sender, RoutedEventArgs e)
        {
            comicImage.Visibility = Visibility.Visible;
            sunInfo.Visibility = Visibility.Collapsed;
            if (currentNumber < maxNumber)
            {
                currentNumber += 1;
                previousImageButton.IsEnabled = true;
                await LoadImage(currentNumber);

                if (currentNumber == maxNumber)
                {
                    nextImageButton.IsEnabled = false;
                }
            }
        }

        private void SunInformationButton_Click(object sender, RoutedEventArgs e)
        {
            comicImage.Visibility = Visibility.Collapsed;
            sunInfo.Visibility = Visibility.Visible;

        }

        private async void LoadSunInfo_Click(object sender, RoutedEventArgs e)
        {
            var sunInformation = await SunProcessor.LoadSunInformation();
            sunriseText.Text = $"Sunrise is at { sunInformation.Sunrise.ToLocalTime().ToShortTimeString() } ";
            sunsetText.Text = $"Sunset is at { sunInformation.Sunset.ToLocalTime().ToShortTimeString() } ";
        }
    }
}
