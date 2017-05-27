using System;
using System.Collections;
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
        private Thermometer _thermometer = new Thermometer();

        public MainWindow()
        {
            InitializeComponent();
            LoadComboBoxes();
            LoadTextBoxes();
            ReloadThresholdsDataGrid();
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
            UpdateTemperatureLabels();
        }

        private void ReloadThresholdsDataGrid()
        {
            string thresholdTitles = "";
            foreach (var threshold in _thermometer._thermometerProperties.Thresholds)
            {
               thresholdTitles = $"{thresholdTitles} \n {threshold.ThresholdName}, {threshold.ThresholdValueCelsius.ToString()} , {threshold.TempTolerance.ToleranceValue.ToString()} , {threshold.SensitiveToRisingEdge.ToString()} , {threshold.SensitiveToFallingEdge.ToString()}";
            }
            TB_TestThresholds.Text = thresholdTitles;
        }

        private void SH_temperature_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RegisterTemperatureChange();
        }

        private void RegisterTemperatureChange()
        {
            double externalTemperature;
            // Establish the input temperature units.
            if (CB_MeasurementUnits.SelectedItem as string == "Fahrenheit")
            {
                externalTemperature = Convert.ToDouble(TB_SliderValueFahrenheit.Text);
            }
            else
            {
                externalTemperature = Convert.ToDouble(TB_SliderValueCelsius.Text);
            }
            try
            {
                // Attempt to read the temperature and get back the result in the desired units. Display the result in a text box.                
                _thermometer.RegisterTemperatureChange(externalTemperature, CB_MeasurementUnits.SelectedItem.ToString(), CB_DisplayUnits.SelectedItem.ToString());
                // Determine if the temperature is rising or falling. Not very interested in whether it has no change.
                foreach (var threshold in _thermometer._newlyReachedThresholds)
                {
                    MessageBox.Show($"{threshold.ThresholdName} Reached!");
                }
            }
            catch (Exception conversionException)
            {
                // Dislpay the unit conversion error message in a messagebox.
                MessageBox.Show(conversionException.Message);
                return;
            }
            UpdateTemperatureLabels();
        }

        private void UpdateTemperatureLabels()
        {
            TB_SliderValueCelsius.Text = Math.Round(SH_Temperature.Value, 2).ToString();
            TB_SliderValueFahrenheit.Text = _thermometer.ConvertUnits(SH_Temperature.Value, "Celsius", "Fahrenheit").ToString();
            TB_TemperatureDisplay.Text = _thermometer._currentTemperature.ToString();
        }

        private void CB_measurementUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_MeasurementUnits.Text != "" && CB_DisplayUnits.Text != "")
            {
                _thermometer.UpdateThermometerUnits(CB_MeasurementUnits.SelectedItem as string, CB_DisplayUnits.SelectedItem as string);
                RegisterTemperatureChange();
            }
        }

        private void BT_NewThreshold_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Open new page which has all necessary input fields for creating a threshold
            // Update the display of thresholds
            var NewThreshold = new NewThreshold(_thermometer);
            NewThreshold.Show();
        }

        private void BT_DeleteThreshold_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            ReloadThresholdsDataGrid();
        }

        private void BT_LoadDefaultThresholds_Click(object sender, RoutedEventArgs e)
        {
            _thermometer.SetupDefaultThresholds();
            ReloadThresholdsDataGrid();
        }

        private void CB_DisplayUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_MeasurementUnits.Text != "" && CB_DisplayUnits.Text != "")
            {
                _thermometer.UpdateThermometerUnits(CB_MeasurementUnits.SelectedItem as string, CB_DisplayUnits.SelectedItem as string);
                RegisterTemperatureChange();
            }
        }
    }
}
