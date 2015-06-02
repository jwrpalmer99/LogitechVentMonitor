using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenHardwareMonitor.Hardware;

namespace WhoIsSpeaking
{
    class HardwareMonitor
    {
        private static Computer _computer = null;
        public HardwareMonitor()
        {
            _computer = new Computer();
            _computer.CPUEnabled = true;
            _computer.Open();
        }

        internal float gettemp()
        {
            if (_computer == null)
            {

            }
            var temps = new List<decimal>();
            foreach (var hardware in _computer.Hardware)
            {
                if (hardware.HardwareType != HardwareType.CPU)
                    continue;
                hardware.Update();
                foreach (var sensor in hardware.Sensors)
                {
                    if (sensor.SensorType != SensorType.Temperature)
                    {
                        if (sensor.Value != null)
                            temps.Add((decimal)sensor.Value);
                    }
                }
            }

            foreach (decimal temp in temps)
            {
                Console.WriteLine(temp);
            }
            return (float)temps.Average();
        }
    }
}
