using System.IO.Ports;
using System.Threading;

public class currentControl {
    public void stepup(
        SerialPort port,
        decimal currentStart,
        decimal currentEnd,
        int time,
        int steps
    ){        
        
        SpiController mycontroller = new SpiController();

        int currentStartBinary = (int)((decimal)Math.Round(currentStart) * (decimal)Math.Pow(2, 15) - 1) / 2;
        int currentEndBinary = (int)((decimal)Math.Round(currentEnd) * (decimal)Math.Pow(2, 15) - 1) / 2;
        
        int currentStep = (currentEndBinary - currentStartBinary) / steps;

        int sleepDuration = time / steps;

        int[] currentControlValues = new int[steps];

        // Populate the array with equally spaced values
        for (int i = 0; i < steps; i++) {
            currentControlValues[i] = currentStartBinary + i * currentStep;
        }

        foreach (int value in currentControlValues) {
            Console.WriteLine("CurrentControl Value: " + value);
            string commandinput = mycontroller.SetPoint(1,0,value,1);
            Console.WriteLine(commandinput);
            port.Write($"spi(1,0,{commandinput})\r\n");
            Thread.Sleep(sleepDuration);
        }


    }
}