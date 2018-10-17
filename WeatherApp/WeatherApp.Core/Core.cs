using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core
{
    public class Core
    {
        public static async Task<Weather> GetWeather(string zipCode)
        {
            string key = "0e6b814cd035fc2308d424f77a66c57d";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?q=" + zipCode + "&APPID=" + key + "&units=metric";

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);

            var weather = new Weather();
            weather.Temperature = (string)results["main"]["temp"] + " C (average)";
            weather.Pressure = (string)results["main"]["pressure"] + " hPa";
            weather.Windspeed = (string)results["wind"]["speed"] + " m/s";
            weather.Cityname = (string)results["name"];
            weather.Tempavg = (string)results["main"]["temp_min"] + "C / " + (string)results["main"]["temp_max"] + "C";
            weather.Type = (string)results["weather"][0]["main"] + " ";
            weather.Icon = (string)results["weather"][0]["icon"];
            return weather;


        }
    }
}
