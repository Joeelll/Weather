using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WeatherApp.Core;

namespace WeatherApp
{
    [Activity(Label = "WeatherListActivity", Theme = "@style/AppTheme")]
    public class WeatherListActivity : Activity
    {
        ListView listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.weather_list);

            listView = FindViewById<ListView>(Resource.Id.listView1);

            var input = Intent.Extras.GetString("input");
            update_weather(input);
        }

        private async void update_weather(string input)
        {
            var weathers = await Core.Core.Get5DaysWeather(input);
            if (weathers == null)
            {
                throw new System.ArgumentException("Parameter cannot be null", "original");
            }

            listView.Adapter = new CustomAdapter(this, weathers);
        }
    }
}