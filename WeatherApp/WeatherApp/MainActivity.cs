using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using WeatherApp.Core;
using System.Drawing;
using System.Net;
using Android.Graphics;
using Android.Content;

namespace WeatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView textTemp;
        TextView textPres;
        TextView textWind;
        TextView textName;
        ImageView pictureWeather;
        SearchView cityName;
        TextView textAvg;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var button = FindViewById<Button>(Resource.Id.button1);
            var buttonForecast = FindViewById<Button>(Resource.Id.buttonForecast);
            textTemp = FindViewById<TextView>(Resource.Id.textTemperature);
            textPres = FindViewById<TextView>(Resource.Id.textPressure);
            textWind = FindViewById<TextView>(Resource.Id.textWindspeed);
            textName = FindViewById<TextView>(Resource.Id.textCityname);
            cityName = FindViewById<SearchView>(Resource.Id.searchCityname);
            textAvg = FindViewById<TextView>(Resource.Id.textTempavg);
            pictureWeather = FindViewById<ImageView>(Resource.Id.pictureWeather);
            //var background = FindViewById<RelativeLayout>(Resource.Id.relativeLayout);

            button.Click += Button_Click;
            buttonForecast.Click += buttonForecast_Click;
        }

        public async void Button_Click(object sender, System.EventArgs e)
        {
            var weather = await Core.Core.GetWeather(cityName.Query);
            textTemp.Text = weather.Temperature;
            textPres.Text = weather.Pressure;
            textWind.Text = weather.Windspeed;
            textName.Text = weather.Cityname;
            textAvg.Text = weather.TemperatureLow + " / " + weather.TemperatureHigh;
            //var imageBitmap = GetBitmapfromUrl("http://openweathermap.org/img/w/{weather.Icon}.png");
            string weatherIconUrl = "http://openweathermap.org/img/w/" + weather.Icon + ".png";
            var imageBitmap = GetImageBitmapFromUrl(weatherIconUrl);
            pictureWeather.SetImageBitmap(imageBitmap);
            pictureWeather.SetScaleType(ImageView.ScaleType.FitXy);

        }
        
        public async void buttonForecast_Click(object sender, EventArgs e)
        {
            var weather = await Core.Core.GetWeather(cityName.Query);
            Intent intent = new Intent(this, typeof(WeatherListActivity));
            intent.PutExtra("input", weather.Cityname);
            StartActivity(intent);
        }

        static Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;
            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            return imageBitmap;
        }
    }
}