using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WeatherApp.Core
{
    public class Weather
    {
        public string Temperature { get; set; } = " ";
        public string Pressure { get; set; } = " ";
        public string Windspeed { get; set; } = " ";
        public string Cityname { get; set; } = " ";
        public string TemperatureLow { get; set; } = " ";
        public string TemperatureHigh { get; set; } = " ";
        public string Type { get; set; } = " ";
        public string Icon { get; set; } = " ";
        public string Date { get; set; } = " ";
    }

}