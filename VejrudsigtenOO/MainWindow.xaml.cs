using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VejrudsigtenOO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string by = By.Text;

            WeatherService service = new (by);
            await service.UpdateWeatherAsync();

            Weather weather = new();

            string beskrivelse = service.TodayDescription;
            double temperatur = service.TodayTemperature;
            string besked = $"Vejret er {temperatur} grader og {beskrivelse}";

            weather.Description = besked;
            string filnavn = GetFilename(beskrivelse);
            weather.Type = filnavn;

            DayData[] data = new DayData[3];
            for (int dag = 0; dag < 3; dag++)
            {
                int activeDay = dag + 1;
                DayData currentDay = new()
                {
                    Temperature = service.FutureTemperature(activeDay),
                    Type = GetFilename(service.FutureDescription(activeDay))
                };
                data[dag] = currentDay;
            }
            weather.Future = data;

            Info info = new(weather);
            info.Show();
        }

        private string GetFilename(string beskrivelse)
        {
            string filnavn;

            switch (beskrivelse)
            {
                case "Klart vejr":
                    filnavn = "Klart.png";
                    break;
                case "Regn":
                    filnavn = "regn.png";
                    break;
                case "Sne":
                    filnavn = "sne.png";
                    break;
                case "Skyet":
                    filnavn = "skyet.png";
                    break;
                default:
                    filnavn = "andet.png";
                    break;
            }
            filnavn = $"/Billeder/{filnavn}";

            return filnavn;

        }
    }
}
