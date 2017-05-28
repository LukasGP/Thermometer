Thermometer Solution:

This solution provides a basic thermometer which accepts some external temperature as a double. 
	It can have arbitrary thresholds set which have various properties.

To get started you need to create a new instance of the Thermometer class. At this point the thermometer 
	is ready to accept input temperatures. This can be done by calling the 
	RegisterTemperatureChange(double temperatureReading, string measurementUnits, 
	string displayUnits) method. The measurementUnits variable is the units of the input 
	temperature (Celsius or Fahrenheit) and the displayUnits is the units of the output of the 
	thermometer after any necessary conversions (Celsius or Fahrenheit).

To establish thresholds call the CreateThermometerThreshold(string thresholdName, double thresholdValue, 
	double temperatureTolerance, bool sensativeToRisingEdge, bool sensativeToFallingEdge).
	The temperatureTolerance variable represents the sensitivity of the thermometer around a threshold. 
	It controls the upper and lower bands of the threshold, within those bounds you won't receive any 
	additional messages if the threshold is based. For example, if the temperature reaches freezing you 
	will be alerted, but if you have a temperatureTolerance of 0.5 you won't receive any additional 
	alerts unless the temperature changes by greater than 0.5 degrees prior to freezing again. 

	sensativeToRisingEdge describes if the threshold cares if reached while temperatures were increasing.

	sensativeToFallingEdge describes if the threshold cares if reached while temperatures were decreasing.

If a change in temperature results in a threshold being reached that threshold can be found in 
the List<Threshold> NewlyReachedThresholds object in the Thermometer class.

Example:
If you would like to see a working UI that's been added onto the Thermometer class please see the "Published" folder.
The setup inside published should install a working copy of the Thermometer. Note: You will likely need to be on
windows or using an emulator to install these files.