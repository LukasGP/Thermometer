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
        private readonly Thermometer CurrentThermometer;
        /// <summary>
        /// A new instance of the form for creating a new threshold.
        /// </summary>
        /// <param name="currentThermometer">The current thermometer instance.</param>
        public NewThreshold(Thermometer currentThermometer)
        {
            // Note, that the textboxes are not limited to what their input can be. 
            // This is purely for demonstration purposes and should not be deployed.
            InitializeComponent();
            CurrentThermometer = currentThermometer;
        }

        /// <summary>
        /// Create a threshold with all of the specified values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_Submit_Click(object sender, RoutedEventArgs e)
        {
            var thresholdName = TB_ThresholdName.Text;
            var thresholdValue = Convert.ToDouble(TB_ThresholdValue.Text);
            var thresholdTolerance = Convert.ToDouble(TB_ThresholdTolerance.Text);
            var risingEdgeSensitive = CB_ThresholdRisingEdgeSensitive.IsChecked != null && 
                (bool)CB_ThresholdRisingEdgeSensitive.IsChecked;
            var fallingEdgeSensitive = CB_ThresholdFallingEdgeSensitive.IsChecked != null && 
                (bool)CB_ThresholdFallingEdgeSensitive.IsChecked;
            CurrentThermometer.CreateThermometerThreshold(thresholdName, thresholdValue, 
                thresholdTolerance, risingEdgeSensitive, fallingEdgeSensitive);
            var homePage = Owner as MainWindow;
            homePage?.RefreshThresholdsDisplay();
            Close();
        }
    }
}
