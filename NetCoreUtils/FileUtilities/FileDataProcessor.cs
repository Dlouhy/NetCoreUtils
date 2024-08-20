namespace NetCoreUtils.FileUtilities
{
    /// <summary>
    /// Process the data read from the file. It is used in the FileReader class in the ReadFileAsync
    /// method.
    /// </summary>
    public class FileDataProcessor : IFileProcessor
    {
        /// <summary>
        /// Processes the data from the file.
        /// </summary>
        /// <param name="bytesRead">The number of bytes read.</param>
        /// <param name="data">The byte array containing the data.</param>
        /// <param name="totalBytes">The total number of bytes expected.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task ProcessAsync(int bytesRead, byte[] data, double totalBytes)
        {
            // Do something here with the data that was just read. For example
            //convert data to string nad save them to the file

            string text = System.Text.Encoding.UTF8.GetString(data, 0, bytesRead);

            await FileWriter.WriteTextToFileAsync("result.txt", text, System.Text.Encoding.UTF8).ConfigureAwait(false);
        }
    }
}