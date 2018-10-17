using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WeatherApp.Core;

namespace WeatherApp
{
    public class CustomAdapter : BaseAdapter<Weather>
    {
        List<Weather> items;
        Activity context;

        public CustomAdapter(Activity context, List<Weather> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override Weather this[int position]
        {
            get { return items[position];  }
        }

        public override int Count { get { return items.Count;  } }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomRow, null);

            view.FindViewById<TextView>(Resource.Id.date).Text = items[position].Date;

            var image = view.FindViewById<ImageView>(Resource.Id.icon);

            string weatherIconUrl = "http://openweathermap.org/img/w/" + items[position].Icon + ".png";
            var imageBitmap = GetImageBitmapFromUrl(weatherIconUrl);
            image.SetImageBitmap(imageBitmap);
            image.SetScaleType(ImageView.ScaleType.FitXy);

            view.FindViewById<TextView>(Resource.Id.tempHigh).Text = items[position].TemperatureHigh;
            view.FindViewById<TextView>(Resource.Id.tempLow).Text = items[position].TemperatureLow;
            view.FindViewById<TextView>(Resource.Id.date).Text = items[position].Date;

            return view;
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