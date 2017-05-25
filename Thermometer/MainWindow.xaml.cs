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

namespace ThermometerNS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, double> thresholds = new Dictionary<string, double>()
        {
            { "DefaultThreshold", 50 }
        };
        public Thermometer thermometer = new Thermometer();

        public MainWindow()
        {
            InitializeComponent();
            LoadComboBoxes();
            LoadTextBoxes();

            //TODO: have these values established from a text box in the UI
            thermometer.CreateThermometerThreshold("Boiling", 100, 0, true, false);
        }

        private void LoadComboBoxes()
        {
            var units = new List<string>()
            {
                "Celsius",
                "Fahrenheit"
            };

            foreach (var unit in units)
            {
                CB_MeasurementUnits.Items.Add(unit);
                CB_DisplayUnits.Items.Add(unit);
            }
            CB_MeasurementUnits.SelectedIndex = 0;
            CB_DisplayUnits.SelectedIndex = 0;
        }

        private void LoadTextBoxes()
        {
            TB_SliderValue.Text = SH_Temperature.Value.ToString();
            TB_Tolerance.Text = "0.1";
        }

        private void SH_temperature_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Display the slider value in a text box.
            TB_SliderValue.Text = Math.Round(SH_Temperature.Value, 2).ToString();

            try
            {
                // Attempt to read the temperature and get back the result in the desired units. Display the result in a text box.                
                thermometer.RegisterTemperatureChange(SH_Temperature.Value, CB_MeasurementUnits.SelectedItem.ToString(), CB_DisplayUnits.SelectedItem.ToString());
                // Determine if the temperature is rising or falling. Not very interested in whether it has no change.
                TB_TemperatureDisplay.Text = thermometer._currentTemperature.ToString();
                thermometer.HasThresholdBeenReached();
            }
            catch (Exception conversionException)
            {
                // Dislpay the unit conversion error message in a messagebox.
                MessageBox.Show(conversionException.Message);
            }
            
        }

        private void RegisterTemperatureChange()
        {
            
        }

        private void CB_measurementUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: switch the slider to the appropriate slider for the unit selection.
        }

        private void BT_NewThreshold_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
