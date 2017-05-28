using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ThermometerNS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Thermometer _thermometer = new Thermometer();
        public MainWindow()
        {
            InitializeComponent();
            LoadComboBoxes();
            LoadTextBoxes();
            RefreshThresholdsDisplay();
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

        /// <summary>
        /// Fetches an updated list of all of the thresholds and prints their details into a textbox.
        /// </summary>
        public void RefreshThresholdsDisplay()
        {
            var thresholdTitles = _thermometer.ThermometerProperties.Thresholds.Aggregate("", (current, threshold) => $"{current} \n {threshold.ThresholdName}, {threshold.ThresholdValueCelsius} , {threshold.TempTolerance.ToleranceValue} , {threshold.SensitiveToRisingEdge} , {threshold.SensitiveToFallingEdge}");
            TB_TestThresholds.Text = thresholdTitles;
        }

        /// <summary>
        /// Mimics an external temperature source. Triggers the thermometer to handle an updated temperature.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SH_temperature_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RegisterTemperatureChange();
        }

        /// <summary>
        /// Registers the temperature change on all UI elements: takes in the proper external temperature with its specified
        /// units (Celsius or Fahrenheit). Passes the new temperature to the thermometer. Displays a messagebox for any newly
        /// reached thresholds and updates the output temperature after conversions in the appropriate text box.
        /// </summary>
        private void RegisterTemperatureChange()
        {
            // Establish the input temperature units.
            var externalTemperature = Convert.ToDouble(CB_MeasurementUnits.SelectedItem as string == "Fahrenheit" ? TB_SliderValueFahrenheit.Text : TB_SliderValueCelsius.Text);
            try
            {
                // Attempt to read the temperature and get back the result in the desired units. Display the result in a text box.                
                _thermometer.RegisterTemperatureChange(externalTemperature, CB_MeasurementUnits.SelectedItem.ToString(), CB_DisplayUnits.SelectedItem.ToString());
                // Determine if the temperature is rising or falling. Not very interested in whether it has no change.
                foreach (var threshold in _thermometer.NewlyReachedThresholds)
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

        /// <summary>
        /// Updates all temperatures labels: Slider value in Celsius and Fahrenheit, as well as the output temperature from the thermometer.
        /// </summary>
        private void UpdateTemperatureLabels()
        {
            TB_SliderValueCelsius.Text = Math.Round(SH_Temperature.Value, 2).ToString(CultureInfo.InvariantCulture);
            TB_SliderValueFahrenheit.Text = _thermometer.ConvertUnits(SH_Temperature.Value, "Celsius", "Fahrenheit").ToString(CultureInfo.InvariantCulture);
            TB_TemperatureDisplay.Text = _thermometer.CurrentTemperature.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Registers a change in the units of the input external temperature source. Updates the thermometer's properties
        /// and calls for the output temperature reading to be refreshed to reflect this change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_measurementUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_MeasurementUnits.Text == "" || CB_DisplayUnits.Text == "") return;
            _thermometer.UpdateThermometerUnits(CB_MeasurementUnits.SelectedItem as string, CB_DisplayUnits.SelectedItem as string);
            RegisterTemperatureChange();
        }

        /// <summary>
        /// Registers a request to create a new threshold. Opens the form required for creating a new threshold.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_NewThreshold_Click(object sender, RoutedEventArgs e)
        {
            // Update the display of thresholds
            var newThresholdPage = new NewThreshold(_thermometer) {Owner = this};
            newThresholdPage.Show();
        }

        private void BT_LoadDefaultThresholds_Click(object sender, RoutedEventArgs e)
        {
            _thermometer.SetupDefaultThresholds();
            RefreshThresholdsDisplay();
        }

        private void CB_DisplayUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_MeasurementUnits.Text == "" || CB_DisplayUnits.Text == "") return;
            _thermometer.UpdateThermometerUnits(CB_MeasurementUnits.SelectedItem as string, CB_DisplayUnits.SelectedItem as string);
            RegisterTemperatureChange();
        }

        private void BT_DeleteAllThresholds_Click(object sender, RoutedEventArgs e)
        {
            var diaglogResult = MessageBox.Show("This action will clear all currently established thresholds.", "Delete All Thresholds",
                MessageBoxButton.OKCancel);
            if (diaglogResult == MessageBoxResult.Cancel) return;
            _thermometer.ThermometerProperties.Thresholds.Clear();
            RefreshThresholdsDisplay();
        }
    }
}
