using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using System;

namespace WeatherAPI.Manager
{
    public class WeatherApiManager : MonoBehaviour
    {
        [Serializable]
        public struct Hourly
        {
            public List<string> time;
            public List<float> temperature_2m;
            public List<int> weathercode;
        }
        [Serializable]
        public struct WeatherResult
        {
            public float latitude;
            public float longitude;
            public float generationtime_ms;
            public float utc_offset_seconds;
            public string timezone;
            public string timezone_abbreviation;
            public float elevation;
            public Hourly hourly; 
        }

        WeatherResult weatherResult;
        private static string URL = "https://api.open-meteo.com/v1/forecast?";
        // Start is called before the first frame update

        public void GetAPIData(float latitude, float longitude, Action<bool> callback)
        {
            string query = $"{URL}latitude={latitude}&longitude={longitude}&hourly=temperature_2m,weathercode&forecast_days=1";
            RestClient.Get(query).Then(response =>
            {
                weatherResult = JsonUtility.FromJson<WeatherResult>(response.Text);
                callback?.Invoke(true);

            }).Catch(err =>
            {
                Debug.LogError("Error: " + err.Message);
                callback?.Invoke(false);
            });
        }

        public byte GetWeatherCode(byte hour)
        {
            byte code = (byte) weatherResult.hourly.weathercode[hour];
            switch (code)
            {
                //rain
                case 61:
                case 63:
                case 65: 
                case 66:
                case 67:
                case 80:
                case 81:
                case 82:
                case 95:
                case 96:
                case 99:
                    {
                        return 1;
                    }

                //snow
                case 71:
                case 73:
                case 75:
                case 77:
                case 85:
                case 86:
                    {
                        return 2;
                    }

                //sunny day!
                default:
                    {
                        return 0;
                    }

            }
        }

        public float GetTemperature(byte hour)
        {
            return weatherResult.hourly.temperature_2m[hour];
        }
    }
}
