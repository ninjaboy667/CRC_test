
// See https://aka.ms/new-console-template for more information
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Crc;
using System;
using System.Text;
using SpiCommunication;
using Microsoft.VisualBasic;


SpiController mycontroller = new SpiController();





Console.WriteLine("Global config create SPI ");
string mycheckstring = mycontroller.GlobalConfig(1,1,0,0,0,0,1,0,0);
Console.WriteLine(mycheckstring);

Console.WriteLine("Global diagnosis registers 0 ");
string mycheckstring2 = mycontroller.GlobalDiag0(0,1,1,0,0,0,0,0,0,0,1,1,0);
Console.WriteLine(mycheckstring);