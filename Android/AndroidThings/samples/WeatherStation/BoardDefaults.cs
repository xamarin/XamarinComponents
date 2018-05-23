using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Things.Pio;
using Android.Views;
using Android.Widget;

namespace WeatherStation
{
    public static class BoardDefaults
    {
        private const string DEVICE_EDISON_ARDUINO = "edison_arduino";
        private const string DEVICE_EDISON = "edison";
        private const string DEVICE_RPI3 = "rpi3";
        private const string DEVICE_NXP = "imx6ul";
        private static string sBoardVariant = "";

        public static string GetButtonGpioPin()
        {
            switch (GetBoardVariant())
            {
                case DEVICE_EDISON_ARDUINO:
                    return "IO12";
                case DEVICE_EDISON:
                    return "GP44";
                case DEVICE_RPI3:
                    return "BCM21";
                case DEVICE_NXP:
                    return "GPIO4_IO20";
                default:
                    throw new Exception("Unknown device: " + Build.Device);
            }
        }

        public static string GetLedGpioPin()
        {
            switch (GetBoardVariant())
            {
                case DEVICE_EDISON_ARDUINO:
                    return "IO13";
                case DEVICE_EDISON:
                    return "GP45";
                case DEVICE_RPI3:
                    return "BCM6";
                case DEVICE_NXP:
                    return "GPIO4_IO21";
                default:
                    throw new Exception("Unknown device: " + Build.Device);
            }
        }

        public static string GetI2cBus()
        {
            switch (GetBoardVariant())
            {
                case DEVICE_EDISON_ARDUINO:
                    return "I2C6";
                case DEVICE_EDISON:
                    return "I2C1";
                case DEVICE_RPI3:
                    return "I2C1";
                case DEVICE_NXP:
                    return "I2C2";
                default:
                    throw new Exception("Unknown device: " + Build.Device);
            }
        }

        public static string GetSpiBus()
        {
            switch (GetBoardVariant())
            {
                case DEVICE_EDISON_ARDUINO:
                    return "SPI1";
                case DEVICE_EDISON:
                    return "SPI2";
                case DEVICE_RPI3:
                    return "SPI0.0";
                case DEVICE_NXP:
                    return "SPI3_0";
                default:
                    throw new Exception("Unknown device: " + Build.Device);
            }
        }

        public static string GetSpeakerPwmPin()
        {
            switch (GetBoardVariant())
            {
                case DEVICE_EDISON_ARDUINO:
                    return "IO3";
                case DEVICE_EDISON:
                    return "GP13";
                case DEVICE_RPI3:
                    return "PWM1";
                case DEVICE_NXP:
                    return "PWM7";
                default:
                    throw new Exception("Unknown device: " + Build.Device);
            }
        }

        private static string GetBoardVariant()
        {
            if (sBoardVariant != string.Empty)
            {
                return sBoardVariant;
            }
            sBoardVariant = Build.Device;
            // For the edison check the pin prefix
            // to always return Edison Breakout pin name when applicable.
            if (sBoardVariant.Equals(DEVICE_EDISON))
            {
                var pioService = PeripheralManager.Instance;
                List<string> gpioList = pioService.GpioList.ToList();
                if (gpioList.Count != 0)
                {
                    String pin = gpioList[0];
                    if (pin.StartsWith("IO"))
                    {
                        sBoardVariant = DEVICE_EDISON_ARDUINO;
                    }
                }
            }
            return sBoardVariant;
        }


    }
}