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

namespace VejrudsigtenProgrammering
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

            WeatherService service = new(by);
            await service.UpdateWeatherAsync();

            double temperatur = service.TodayTemperature;
            string beskrivelse = service.TodayDescription;

            string besked = $"Vejret er {beskrivelse} og det er {temperatur} grader";
            besked += "Temperaturerne de kommende dage bliver: ";

            for (int dag = 0; dag < 3; dag++)
            {
                // dag + 1 fordi dag 0 er i dag, og vi starter ved i morgen
                double nyTemperatur = service.FutureTemperature(dag + 1);
                besked += nyTemperatur + " ";
            }

            Vejrudsigten.Content = besked;

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
            Uri uri = new(filnavn, UriKind.Relative);
            BitmapImage image = new(uri);

            VejrType.Source = image;

            string[] kommendeDage = service.GetAllFutureDescriptions();
            var billeder = new[] { TypeDag1, TypeDag2, TypeDag3 };

            for(int dag = 0; dag < kommendeDage.Length; dag++)
            {
                beskrivelse = kommendeDage[dag];
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
                uri = new(filnavn, UriKind.Relative);
                image = new(uri);
                billeder[dag].Source = image;
            }

            double[] kommendeTemperaturer = service.GetAllFutureTemperatures();
            var temperaturer = new[] { TemperaturDag1, TemperaturDag2, TemperaturDag3 };

            for (int dag = 0; dag < kommendeTemperaturer.Length; dag++)
            {
                temperaturer[dag].Content = $"{kommendeTemperaturer[dag]} C";
            }
        }
    }
}
