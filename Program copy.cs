
// // See https://aka.ms/new-console-template for more information
// using System.Drawing;
// using System.Linq.Expressions;
// using System.Runtime.InteropServices;
// using Crc;
// using System;
// using System.Text;
// using SpiCommunication;
// using Microsoft.VisualBasic;
// using System.IO.Ports;
// using System.Threading;
// using System.Configuration.Assemblies;




// public class Program
// {
//     private static ManualResetEvent dataReceivedEvent = new ManualResetEvent(true);
//     static void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
// {
//     Console.Write(((SerialPort)sender).ReadExisting());
//     dataReceivedEvent.Set(); // Signal that data has been received
// }

// static void Main()
// {

//     SpiController mycontroller = new SpiController();
//     SerialPort port = new SerialPort("COM4", 115200, Parity.None, 8, StopBits.One);


//     port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
//     port.Open();



//     // Write;GLOBAL_CONFIG;0x02;0x4004
//     // voltage 5.0 V SPI CRC Check
//     string commandinput = mycontroller.GlobalConfig(1,1,0,0,0,0,1,0,0);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();



//     // Write;GLOBAL_DIAG1;0x04;0x000
//     // no fault detections
//     commandinput = mycontroller.GlobalDiag0(1,0,0,0,0,0,0,0,0,0,0,0,0);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();



//     // Write;GLOBAL_DIAG2;0x05;0x000
//     // no fault detections more settings
//     commandinput = mycontroller.GlobalDiag1(1,0,0,0,0,0,0,0,0);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();



//     // Write;DIAG_ERR_CHGR0;0x0a;0x000
//     // no error detections 
//     commandinput = mycontroller.DiagnosisErrorRegister0(1,0,0,0,0,0,0,0,0,0,0);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();



//     // Write;DIAG_ERR_CHGR1;0x0b;0x000
//     // no error detections 
//     commandinput = mycontroller.DiagnosisErrorRegister1(1,0,0,0,0,0,0,0,0,0,0);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();



//     // Write;DIAG_WARN_CHGR0;0x10;0x000
//     // no warning detections 
//     commandinput = mycontroller.DiagnosisWarningRegister0(1,0,0,0,0,0,0,0,0,0,0);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();



//     // Write;DIAG_WARN_CHGR1;0x11;0x000
//     // no warning detections 
//     commandinput = mycontroller.DiagnosisWarningRegister0(1,0,0,0,0,0,0,0,0,0,0);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();



//     // Write;MODE;0x4c;0x001
//     // set channel 0 to ICC Current Control
//     commandinput = mycontroller.Mode(1,0,1);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();

//     // Write;MODE;0x5c;0x001
//     // set channel 1 to ICC Current Control
//     commandinput = mycontroller.Mode(1,1,1);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();


//     // Write;MODE;0x6c;0x001
//     // set channel 2 to ICC Current Control
//     commandinput = mycontroller.Mode(1,2,1);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();


//     // Write;MODE;0x7c;0x001
//     // set channel 3 to ICC Current Control
//     commandinput = mycontroller.Mode(1,3,1);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();





//     // Write;SETPOINT;0x40;0x2000
//     //Current Setpoint Register for channel 0 to 0x2000 or ~ 0.5 amps
//     commandinput = mycontroller.SetPoint(1,0,8192,1);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();



//     // Write;SETPOINT;0x50;0x2000
//     //Current Setpoint Register for channel 0 to 0x2000 or ~ 0.5 amps
//     commandinput = mycontroller.SetPoint(1,1,8192,1);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();



//     // Write;SETPOINT;0x60;0x2000
//     //Current Setpoint Register for channel 0 to 0x2000 or ~ 0.5 amps
//     commandinput = mycontroller.SetPoint(1,2,8192,1);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();


//     // Write;SETPOINT;0x70;0x2000
//     //Current Setpoint Register for channel 0 to 0x2000 or ~ 0.5 amps
//     commandinput = mycontroller.SetPoint(1,3,8192,1);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();




//     // Write;CH_CTRL;0x00;0x800f
//     // enable ALL channels, set chip from Config mode to Mission mode
//     commandinput = mycontroller.ChannelControl(1,1,1,1,1,0,0,1);
//     Console.WriteLine(commandinput);
//     port.Write($"spi(1,0,{commandinput})\r\n)");
//     Console.ReadLine();


    
//     Console.ReadLine();
//     port.Close();

// }

// }









// // Attempt
// // CMD;Name;Address;Value

// // Write;GLOBAL_DIAG0;0x03;0x000
// // Write;GLOBAL_DIAG1;0x04;0x000
// // Write;GLOBAL_DIAG2;0x05;0x000
// // Write;DIAG_ERR_CHGR0;0x0a;0x000
// // Write;DIAG_ERR_CHGR1;0x0b;0x000
// // Write;DIAG_WARN_CHGR0;0x10;0x000
// // Write;DIAG_WARN_CHGR1;0x11;0x000
// // Write;SETPOINT;0x40;0x0000
// // Write;MODE;0x4c;0x001
// // Write;CH_CONFIG;0x47;0x063
// // Write;CH_CTRL;0x00;0x8001
// // Read;DIAG_ERR_CHGR0;0x0a;0x002



// // Actually Command
// // CMD;Name;Address;Value
// // Write;GLOBAL_CONFIG;0x02;0x4004
// // Write;GLOBAL_DIAG0;0x03;0x000
// // Write;GLOBAL_DIAG1;0x04;0x000
// // Write;GLOBAL_DIAG2;0x05;0x000
// // Write;DIAG_ERR_CHGR0;0x0a;0x000
// // Write;DIAG_ERR_CHGR1;0x0b;0x000
// // Write;DIAG_WARN_CHGR0;0x10;0x000
// // Write;DIAG_WARN_CHGR1;0x11;0x000
// // Write;MODE;0x4c;0x001
// // Write;MODE;0x7c;0x001
// // Write;MODE;0x6c;0x001
// // Write;MODE;0x5c;0x001
// // Write;SETPOINT;0x40;0x2000
// // Write;SETPOINT;0x50;0x2000
// // Write;SETPOINT;0x60;0x2000
// // Write;SETPOINT;0x70;0x2000
// // Write;CH_CTRL;0x00;0x800f