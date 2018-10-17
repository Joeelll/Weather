using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core
{
    public class Core
    {
        const string key = "0e6b814cd035fc2308d424f77a66c57d";

        public static async Task<Weather> GetWeather(string location)
        {
            
            string queryString = "http://api.openweathermap.org/data/2.5/weather?q=" + location + "&APPID=" + key + "&units=metric";

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);

            var weather = new Weather();
            weather.Temperature = (string)results["main"]["temp"] + " C (average)";
            weather.Pressure = (string)results["main"]["pressure"] + " hPa";
            weather.Windspeed = (string)results["wind"]["speed"] + " m/s";
            weather.Cityname = (string)results["name"];
            weather.TemperatureLow = (string)results["main"]["temp_min"] + "C";
            weather.TemperatureHigh = (string)results["main"]["temp_max"] + "C";
            weather.Type = (string)results["weather"][0]["main"] + " ";
            weather.Icon = (string)results["weather"][0]["icon"];
            return weather;
        }

        public static async Task<List<Weather>> Get5DaysWeather(string location)
        {
            string queryString = "http://api.openweathermap.org/data/2.5/forecast?q=" + location + "&APPID=" + key + "&units=metric";

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            if (results == null) return null;

            List<Weather> weathers = new List<Weather>();

            int currentIterator = 0;
            for (int i = 0; i < 5; i++)
            {
                Weather weather = new Weather();
                weather.TemperatureHigh = (string)results["list"][currentIterator]["main"]["temp_max"] + "C";
                weather.TemperatureLow = (string)results["list"][currentIterator]["main"]["temp_min"] + "C";
                weather.Icon = (string)results["list"][currentIterator]["weather"][0]["icon"];
                weather.Date = UnixTimeToString((long)results["list"][currentIterator]["dt"]);

                currentIterator += 8;

                weathers.Add(weather);
            }
            return weathers;
        }

        public static string UnixTimeToString(long dt)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(dt).ToLocalTime();

            var a = dateTime.ToString();

            return dateTime.ToString();
        }
    }
}
