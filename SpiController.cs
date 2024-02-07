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

            address |= (VBATUndervoltageDetection == 1) ? 0b000000000000000000000001 << 0 : 0b0;
            address |= (VBATOvervoltageDetection == 1) ? 0b000000000000000000000001 << 1 : 0b0;
            address |= (VIOUndervoltageDetection == 1) ? 0b000000000000000000000001 << 2 : 0b0;
            address |= (VIOOvervoltageDetenction == 1) ? 0b000000000000000000000001 << 3 : 0b0;
            address |= (VDDUndervoltageDetection == 1) ? 0b000000000000000000000001 << 4 : 0b0;
            address |= (VDDOverboltageDetection == 1) ? 0b000000000000000000000001 << 5 : 0b0;
            address |= (ClockFaultDetection == 1) ? 0b000000000000000000000001 << 6 : 0b0;
            address |= (CentralOvertemperatureError == 1) ? 0b000000000000000000000001 << 7 : 0b0;
            address |= (CentralOvertemperatureWarning == 1) ? 0b000000000000000000000001 << 8 : 0b0;
            address |= (ResetOccuredRESNPinLow == 1) ? 0b000000000000000000000001 << 9 : 0b0;
            address |= (EventOccured == 1) ? 0b000000000000000000000001 << 10 : 0b0;
            address |= (SPIWatchdogFaultDetection == 1) ? 0b000000000000000000000001 << 14 : 0b0;

            string finalstring = AddSpiCrc(address);

            return finalstring;
        }


        public string GlobalDiag1(
            int readWrite,
            int InternalBiasCurrentTooLowDetection,
            int InternalBiasCurrentTooHighDetection,
            int Internal2V5SupplyUndervoltageDetection,
            int Internal2V5SupplyOvervoltageDetection,
            int InternalReferenceUndervoltageDetection,
            int InternalReferenceOvervoltageDetection,
            int InternalPreRegulatorOverVoltageDetection,
            int InternalMoniteringADCErrorDetection

        )
        {
            int address = 0x0004 << 17;

            address |= (readWrite == 1) ? 0b000000000000000000000001 << 16 : 0b0;

            address |= (InternalBiasCurrentTooLowDetection == 1) ? 0b000000000000000000000001 << 0 : 0b0;
            address |= (InternalBiasCurrentTooHighDetection == 1) ? 0b000000000000000000000001 << 1 : 0b0;
            address |= (Internal2V5SupplyUndervoltageDetection == 1) ? 0b000000000000000000000001 << 2 : 0b0;
            address |= (Internal2V5SupplyOvervoltageDetection == 1) ? 0b000000000000000000000001 << 3 : 0b0;
            address |= (InternalReferenceUndervoltageDetection == 1) ? 0b000000000000000000000001 << 4 : 0b0;
            address |= (InternalReferenceOvervoltageDetection == 1) ? 0b000000000000000000000001 << 5 : 0b0;
            address |= (InternalPreRegulatorOverVoltageDetection == 1) ? 0b000000000000000000000001 << 6 : 0b0;
            address |= (InternalMoniteringADCErrorDetection == 1) ? 0b000000000000000000000001 << 15 : 0b0;

            string finalstring = AddSpiCrc(address);

            return finalstring;
        }

        public string GlobalDiag2(
            int readWrite,
            int RegisterECCError,
            int OtpEccError,
            int OtpMemoryConfigurationComplete
        )
        {
            int address = 0x0005 << 17;

            address |= (readWrite == 1) ? 0b000000000000000000000001 << 16 : 0b0;

            address |= (RegisterECCError == 1) ? 0b000000000000000000000001 << 1 : 0b0;
            address |= (OtpEccError == 1) ? 0b000000000000000000000001 << 3 : 0b0;
            address |= (OtpMemoryConfigurationComplete == 1) ? 0b000000000000000000000001 << 4 : 0b0;

            string finalstring = AddSpiCrc(address);

            return finalstring;
        }

        public string DiagnosisErrorRegister0(
            int readWrite,
            int OpenLoadOrShortToGroundDetectionCh0,
            int OpenLoadDetectionCh0,
            int OvercurrentDetectionCh0,
            int ShortToGroundDetectionCh0,
            int OvertemperatureErrorDetectionCh0,
            int OpenLoadOrShortToGroundDetectionCh1,
            int OpenLoadDetectionCh1,
            int OvercurrentDetectionCh1,
            int ShortToGroundDetectionCh1,
            int OvertemperatureErrorDetectionCh1
        )
        {
            int address = 0x000A << 17;

            address |= (readWrite == 1) ? 0b000000000000000000000001 << 16 : 0b0;

            address |= (OpenLoadOrShortToGroundDetectionCh0 == 1) ? 0b000000000000000000000001 << 0 : 0b0;
            address |= (OpenLoadDetectionCh0 == 1) ? 0b000000000000000000000001 << 1 : 0b0;
            address |= (OvercurrentDetectionCh0 == 1) ? 0b000000000000000000000001 << 2 : 0b0;
            address |= (ShortToGroundDetectionCh0 == 1) ? 0b000000000000000000000001 << 3 : 0b0;
            address |= (OvertemperatureErrorDetectionCh0 == 1) ? 0b000000000000000000000001 << 4 : 0b0;
            address |= (OpenLoadOrShortToGroundDetectionCh1 == 1) ? 0b000000000000000000000001 << 8 : 0b0;
            address |= (OpenLoadDetectionCh1 == 1) ? 0b000000000000000000000001 << 9 : 0b0;
            address |= (OvercurrentDetectionCh1 == 1) ? 0b000000000000000000000001 << 10 : 0b0;
            address |= (ShortToGroundDetectionCh1 == 1) ? 0b000000000000000000000001 << 11 : 0b0;
            address |= (OvertemperatureErrorDetectionCh1 == 1) ? 0b000000000000000000000001 << 12 : 0b0;

            string finalstring = AddSpiCrc(address);

            return finalstring;
        }

        public string DiagnosisErrorRegister1(
            int readWrite,
            int OpenLoadOrShortToGroundDetectionCh2,
            int OpenLoadDetectionCh2,
            int OvercurrentDetectionCh2,
            int ShortToGroundDetectionCh2,
            int OvertemperatureErrorDetectionCh2,
            int OpenLoadOrShortToGroundDetectionCh3,
            int OpenLoadDetectionCh3,
            int OvercurrentDetectionCh3,
            int ShortToGroundDetectionCh3,
            int OvertemperatureErrorDetectionCh3
        )
        {
            int address = 0x000B << 17;

            address |= (readWrite == 1) ? 0b000000000000000000000001 << 16 : 0b0;

            address |= (OpenLoadOrShortToGroundDetectionCh2 == 1) ? 0b000000000000000000000001 << 0 : 0b0;
            address |= (OpenLoadDetectionCh2 == 1) ? 0b000000000000000000000001 << 1 : 0b0;
            address |= (OvercurrentDetectionCh2 == 1) ? 0b000000000000000000000001 << 2 : 0b0;
            address |= (ShortToGroundDetectionCh2 == 1) ? 0b000000000000000000000001 << 3 : 0b0;
            address |= (OvertemperatureErrorDetectionCh2 == 1) ? 0b000000000000000000000001 << 4 : 0b0;
            address |= (OpenLoadOrShortToGroundDetectionCh3 == 1) ? 0b000000000000000000000001 << 8 : 0b0;
            address |= (OpenLoadDetectionCh3 == 1) ? 0b000000000000000000000001 << 9 : 0b0;
            address |= (OvercurrentDetectionCh3 == 1) ? 0b000000000000000000000001 << 10 : 0b0;
            address |= (ShortToGroundDetectionCh3 == 1) ? 0b000000000000000000000001 << 11 : 0b0;
            address |= (OvertemperatureErrorDetectionCh3 == 1) ? 0b000000000000000000000001 << 12 : 0b0;

            string finalstring = AddSpiCrc(address);

            return finalstring;
        }
        public string DiagnosisWarningRegister0(
            int readWrite,
            int IccPwmRegulationWarningDetectionCh0,
            int IccCurrentRegulationWarningCh0,
            int OvertemperatureWarningDetectionCh0,
            int OpenLoadOrShortToGroundWarningDetectionCh0,
            int OpenLoadOrShortToGroundWarningDetectionPerformedCh0,
            int IccPwmRegulationWarningDetectionCh1,
            int IccCurrentRegulationWarningCh1,
            int OvertemperatureWarningDetectionCh1,
            int OpenLoadOrShortToGroundWarningDetectionCh1,
            int OpenLoadOrShortToGroundWarningDetectionPerformedCh1
        )
        {
            int address = 0x0010 << 17;

            address |= (readWrite == 1) ? 0b000000000000000000000001 << 16 : 0b0;

            address |= (IccPwmRegulationWarningDetectionCh0 == 1) ? 0b000000000000000000000001 << 0 : 0b0;
            address |= (IccCurrentRegulationWarningCh0 == 1) ? 0b000000000000000000000001 << 1 : 0b0;
            address |= (OvertemperatureWarningDetectionCh0 == 1) ? 0b000000000000000000000001 << 2 : 0b0;
            address |= (OpenLoadOrShortToGroundWarningDetectionCh0 == 1) ? 0b000000000000000000000001 << 3 : 0b0;
            address |= (OpenLoadOrShortToGroundWarningDetectionPerformedCh0 == 1) ? 0b000000000000000000000001 << 4 : 0b0;
            address |= (IccPwmRegulationWarningDetectionCh1 == 1) ? 0b000000000000000000000001 << 8 : 0b0;
            address |= (IccCurrentRegulationWarningCh1 == 1) ? 0b000000000000000000000001 << 9 : 0b0;
            address |= (OvertemperatureWarningDetectionCh1 == 1) ? 0b000000000000000000000001 << 10 : 0b0;
            address |= (OpenLoadOrShortToGroundWarningDetectionCh1 == 1) ? 0b000000000000000000000001 << 11 : 0b0;
            address |= (OpenLoadOrShortToGroundWarningDetectionPerformedCh1 == 1) ? 0b000000000000000000000001 << 12 : 0b0;

            string finalstring = AddSpiCrc(address);

            return finalstring;
        }

        public string DiagnosisWarningRegister1(
            int readWrite,
            int IccPwmRegulationWarningDetectionCh2,
            int IccCurrentRegulationWarningCh2,
            int OvertemperatureWarningDetectionCh2,
            int OpenLoadOrShortToGroundWarningDetectionCh2,
            int OpenLoadOrShortToGroundWarningDetectionPerformedCh2,
            int IccPwmRegulationWarningDetectionCh3,
            int IccCurrentRegulationWarningCh3,
            int OvertemperatureWarningDetectionCh3,
            int OpenLoadOrShortToGroundWarningDetectionCh3,
            int OpenLoadOrShortToGroundWarningDetectionPerformedCh3
        )
        {
            int address = 0x0010 << 17;

            address |= (readWrite == 1) ? 0b000000000000000000000001 << 16 : 0b0;

            address |= (IccPwmRegulationWarningDetectionCh2 == 1) ? 0b000000000000000000000001 << 0 : 0b0;
            address |= (IccCurrentRegulationWarningCh2 == 1) ? 0b000000000000000000000001 << 1 : 0b0;
            address |= (OvertemperatureWarningDetectionCh2 == 1) ? 0b000000000000000000000001 << 2 : 0b0;
            address |= (OpenLoadOrShortToGroundWarningDetectionCh2 == 1) ? 0b000000000000000000000001 << 3 : 0b0;
            address |= (OpenLoadOrShortToGroundWarningDetectionPerformedCh2 == 1) ? 0b000000000000000000000001 << 4 : 0b0;
            address |= (IccPwmRegulationWarningDetectionCh3 == 1) ? 0b000000000000000000000001 << 8 : 0b0;
            address |= (IccCurrentRegulationWarningCh3 == 1) ? 0b000000000000000000000001 << 9 : 0b0;
            address |= (OvertemperatureWarningDetectionCh3 == 1) ? 0b000000000000000000000001 << 10 : 0b0;
            address |= (OpenLoadOrShortToGroundWarningDetectionCh3 == 1) ? 0b000000000000000000000001 << 11 : 0b0;
            address |= (OpenLoadOrShortToGroundWarningDetectionPerformedCh3 == 1) ? 0b000000000000000000000001 << 12 : 0b0;

            string finalstring = AddSpiCrc(address);

            return finalstring;
        }

        public string SetPoint(

        )
        {
            return "incomplete";
        }

        public string Mode(

        )
        {
            return "incomplete";
        }

        public string ChannelConfiguration(

        )
        {
            return "incomplete";
        }

        public string ChannelControl(

        )
        {
            return "incomplete";
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
