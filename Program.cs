using Slint;
using System.Device.Gpio.Drivers;
using System.Device.Gpio;
using AppWindow;
using Iot.Device.Button;

// gpio controller
var gpiod = new LibGpiodDriver(0);
var gpioController = new GpioController(PinNumberingScheme.Logical, gpiod);

/**
* I'm using Raspberry Pi pins 18 and 23
**/
// button
var button = new GpioButton(23, false, false, gpioController);
// led
var led = gpioController.OpenPin(18, PinMode.Output, PinValue.Low);
var ledState = PinValue.Low;

var win = new Window();

button.Press += (obj, e) => {
    Console.WriteLine("Toggling...");
    // toggle
    ledState = !ledState;
    led.Write(ledState);

    win.RunOnUiThread(() => {
        win.isLedOn = ledState == PinValue.High;
    });
};

Console.WriteLine("Hello Torizon!");

win.ToggleLed = () => {
    Console.WriteLine("Toggling...");
    // toggle
    ledState = !ledState;
    led.Write(ledState);
    win.isLedOn = ledState == PinValue.High;
};

win.Run();
