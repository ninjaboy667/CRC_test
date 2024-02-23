
#pragma warning disable CA1416
// adding #if WINDOWS and #endif to system managment and the fuction prevent it from working correctly... at least for now
using System.Management;

using System;
using System.Text.RegularExpressions;

public class AutoCOM
{
    public string GetJLinkCOMPort()
    {

        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption LIKE '%(COM%'");

        foreach (ManagementObject port in searcher.Get())
        {
            string? name = port["Name"] as string;

            if (!string.IsNullOrEmpty(name) && name.StartsWith("JLink CDC UART Port", StringComparison.OrdinalIgnoreCase))
            {
                // Extract COM port from the Name property using regular expressions
                return ExtractComPort(name);
            }
        }


        // Return null or an empty string if no JLink COM port is found
        return "";
    }

    private string ExtractComPort(string portName)
    {
        // Use a regular expression to extract the COM port part
        var match = Regex.Match(portName, @"\((COM\d+)\)");
        return match.Success ? match.Groups[1].Value : "";
    }
}
