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
using System.Windows.Shapes;

namespace ThermometerNS
{
    /// <summary>
    /// Interaction logic for NewThreshold.xaml
    /// </summary>
    public partial class NewThreshold : Window
    {
        Thermometer _currentThermometer;
        public NewThreshold(Thermometer currentThermometer)
        {
            // Note, that the textboxes are not limited to what their input can be. This is purely for demonstration purposes and should not be deployed.
            InitializeComponent();
            _currentThermometer = currentThermometer;
        }

        private void BT_Submit_Click(object sender, RoutedEventArgs e)
        {
            string thresholdName = TB_ThresholdName.Text;
            double thresholdValue = Convert.ToDouble(TB_ThresholdValue.Text);
            double thresholdTolerance = Convert.ToDouble(TB_ThresholdTolerance.Text);
            bool risingEdgeSensitive = (bool)CB_ThresholdRisingEdgeSensitive.IsChecked;
            bool fallingEdgeSensitive = (bool)CB_ThresholdFallingEdgeSensitive.IsChecked;
            _currentThermometer.CreateThermometerThreshold(thresholdName, thresholdValue, thresholdTolerance, risingEdgeSensitive, fallingEdgeSensitive);
            Close();
        }
    }
}
