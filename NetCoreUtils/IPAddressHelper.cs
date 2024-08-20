using System.Net;

namespace NetCoreUtils
{
    /// <summary>
    /// The IP address extensions https://stackoverflow.com/a/13350494
    /// </summary>
    public static class IPAddressHelper
    {
        /// <summary>
        /// Converts from uint to IP address
        /// </summary>
        /// <param name="candidateIP">The candidate IP address to convert</param>
        /// <returns>The IP address as string</returns>
        public static string ConvertFromIntegerToIPAddress(this uint candidateIP)
        {
            byte[] bytes = BitConverter.GetBytes(candidateIP);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return new IPAddress(bytes).ToString();
        }

        /// <summary>
        /// Converts from IP address string to integer
        /// </summary>
        /// <param name="ipAddress">The IP address as string</param>
        /// <returns>The IP address as uint</returns>
        public static uint ConvertFromIPAddressToInteger(this string ipAddress)
        {
            var address = IPAddress.Parse(ipAddress);
            byte[] bytes = address.GetAddressBytes();

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt32(bytes, 0);
        }
    }
}