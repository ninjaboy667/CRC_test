
// See https://aka.ms/new-console-template for more information
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Crc;
using System;
using System.Text;

Console.WriteLine("Testing:");

CrcBase crc = new CrcBase(8,0x1D, 0xFF, 0xFF, false, false);

int hexValue = 0x054004;
byte[] hexValue_bytes = new byte[] { (byte)((hexValue >> 16) & 0xFF), (byte)((hexValue >> 8) & 0xFF), (byte)(hexValue & 0xFF) };

// reversing because this is somehow the way it has to be done! per documentation
byte[] bytes = new byte[] { (byte)(hexValue & 0xFF), (byte)((hexValue >> 8) & 0xFF), (byte)((hexValue >> 16) & 0xFF) };

byte[] resultBytes  = crc.ComputeHash(bytes);
Console.WriteLine("result:");
Console.WriteLine(BitConverter.ToString(resultBytes));


byte[] combinedBytes = resultBytes.Concat(hexValue_bytes).ToArray();
Console.WriteLine(BitConverter.ToString(combinedBytes));






