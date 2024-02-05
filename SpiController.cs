using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Numerics;
using Crc; // will need to install this nugget

namespace SpiCommunication
{
    public class SpiController
    {
        // Your SPI-related methods can go here


        /// <summary>
        /// Configures the global settings based on the provided parameters.
        /// </summary>
        /// <param name="readWrite">1 for write, 0 for read.</param>
        /// <param name="voltageSelection">Voltage selection, default is 1.</param>
        /// <param name="testUnderOverVoltage">Test under/over voltage, default is 0.</param>
        /// <param name="testOverTemperature">Test over temperature, default is 0.</param>
        /// <param name="testInternalSupplyOverVoltage">Test internal supply over voltage, default is 0.</param>
        /// <param name="testInternalSupplyUnderVoltage">Test internal supply under voltage, default is 0.</param>
        /// <param name="spiCrcCheck">SPI CRC check, default is 1.</param>
        /// <param name="spiWatchdog">SPI watchdog, default is 0.</param>
        /// <param name="clockWatchdog">Clock watchdog, default is 0.</param>
        /// <returns>A string representing the final configuration.</returns>
        public string GlobalConfig(
            int readWrite,                  // 1 for write, 0 for read
            int voltageSelection,       // default 1
            int testUnderOverVoltage,   // default 0
            int testOverTemperature,    // default 0
            int testInternalSupplyOverVoltage, // default 0
            int testInternalSupplyUnderVoltage, // default 0
            int spiCrcCheck,            // default 1
            int spiWatchdog,            // default 0
            int clockWatchdog         // default 0
        )
        {

            int address = 0b000001000000000000000000;
            //int address = 0x0002 << 17; ?

            // binary shifting see datasheet page 56
            address |= (readWrite == 1) ? 0b000000010000000000000000 : 0b0;
            address |= (voltageSelection == 1) ? 0b000000000100000000000000 : 0b0;
            address |= (testUnderOverVoltage == 1) ? 0b000000000001000000000000 : 0b0;
            address |= (testOverTemperature == 1) ? 0b000000000000100000000000 : 0b0;
            address |= (testInternalSupplyOverVoltage == 1) ? 0b000000000000000000100000 : 0b0;
            address |= (testInternalSupplyUnderVoltage == 1) ? 0b000000000000000000010000 : 0b0;
            address |= (spiCrcCheck == 1) ? 0b000000000000000000000100 : 0b0;
            address |= (spiWatchdog == 1) ? 0b000000000000000000000010 : 0b0;
            address |= (clockWatchdog == 1) ? 0b000000000000000000000001 : 0b0;

            string finalstring = AddSpiCrc(address);

            return finalstring;

        }

        /// <summary>
        /// Provides global diagnostics information.
        /// </summary>
        /// <param name="readWrite">1 for write, 0 for read</param>
        /// <param name="VBATUndervoltageDetection">VBAT Undervoltage Detection</param>
        /// <param name="VBATOvervoltageDetection">VBAT Overvoltage Detection</param>
        /// <param name="VIOUndervoltageDetection">VIO Undervoltage Detection</param>
        /// <param name="VIOOvervoltageDetenction">VIO Overvoltage Detection</param>
        /// <param name="VDDUndervoltageDetection">VDD Undervoltage Detection</param>
        /// <param name="VDDOverboltageDetection">VDD Overvoltage Detection</param>
        /// <param name="ClockFaultDetection">Clock Fault Detection</param>
        /// <param name="CentralOvertemperatureError">Central Overtemperature Error</param>
        /// <param name="CentralOvertemperatureWarning">Central Overtemperature Warning</param>
        /// <param name="ResetOccuredRESNPinLow">Reset occurred due to RESN-pin</param>
        /// <param name="EventOccured">Event occurred</param>
        /// <param name="SPIWatchdogFaultDetection">SPI Watchdog Fault Detection</param>
        /// <returns>Returns the final string after processing.</returns>
        public string GlobalDiag0(
            int readWrite,                  // 1 for write, 0 for read
            int VBATUndervoltageDetection,
            int VBATOvervoltageDetection,
            int VIOUndervoltageDetection,
            int VIOOvervoltageDetenction,
            int VDDUndervoltageDetection,
            int VDDOverboltageDetection,
            int ClockFaultDetection,
            int CentralOvertemperatureError,
            int CentralOvertemperatureWarning,
            int ResetOccuredRESNPinLow,
            int EventOccured,
            int SPIWatchdogFaultDetection
        )
        {
            int address = 0x0003 << 17;

            address |= (readWrite == 1) ? 0b000000000000000000000001 << 16 : 0b0;

            address |= (VBATUndervoltageDetection == 1) ? 0b000000000000000000000001 << 14 : 0b0;
            address |= (VBATOvervoltageDetection == 1) ? 0b000000000000000000000001 << 10 : 0b0;
            address |= (VIOUndervoltageDetection == 1) ? 0b000000000000000000000001 << 9 : 0b0;
            address |= (VIOOvervoltageDetenction == 1) ? 0b000000000000000000000001 << 8 : 0b0;
            address |= (VDDUndervoltageDetection == 1) ? 0b000000000000000000000001 << 7 : 0b0;
            address |= (VDDOverboltageDetection == 1) ? 0b000000000000000000000001 << 6 : 0b0;
            address |= (ClockFaultDetection == 1) ? 0b000000000000000000000001 << 5 : 0b0;
            address |= (CentralOvertemperatureError == 1) ? 0b000000000000000000000001 << 4 : 0b0;
            address |= (CentralOvertemperatureWarning == 1) ? 0b000000000000000000000001 << 3 : 0b0;
            address |= (ResetOccuredRESNPinLow == 1) ? 0b000000000000000000000001 << 2 : 0b0;
            address |= (EventOccured == 1) ? 0b000000000000000000000001 << 1 : 0b0;
            address |= (SPIWatchdogFaultDetection == 1) ? 0b000000000000000000000001 << 0 : 0b0;

            string finalstring = AddSpiCrc(address);

            return finalstring;
        }





        public string AddSpiCrc (int hexValue) {
            // extract 3 bytes from the hexvalue 
            byte[] hexValue_bytes = new byte[] { (byte)((hexValue >> 16) & 0xFF), (byte)((hexValue >> 8) & 0xFF), (byte)(hexValue & 0xFF) };

            // reversing bytes: documentation requires this order for CRC_Check digit
            Array.Reverse(hexValue_bytes);

            // CRC check method per TLE92464ED Spec Sheet 
            CrcBase crc = new CrcBase(8,0x1D, 0xFF, 0xFF, false, false);

            // Compute CRC hash on the reversed bytes
            byte[] resultBytes  = crc.ComputeHash(hexValue_bytes);

            // Reverse back the original byte array before adding on result bytes
            Array.Reverse(hexValue_bytes);
            byte[] resultByte = AddByteToArray(resultBytes, hexValue_bytes);

            // convert the bytes result back into hexstring for SPI terminal
            string hexString = BitConverter.ToString(resultByte).Replace("-", "");

            return hexString;
        }


        public byte[] AddByteToArray(byte[] array1, byte[] array2)
        {
            byte[] result = new byte[array1.Length + array2.Length];
            Buffer.BlockCopy(array1, 0, result, 0, array1.Length);
            Buffer.BlockCopy(array2, 0, result, array1.Length, array2.Length);
            return result;
        }
    }
}
