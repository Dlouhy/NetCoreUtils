namespace NetCoreUtils.FileUtilities
{
    /// <summary>
    /// Interface defines the method that a file processor should implement. Is used in class
    /// FileReader in ReadFileAsync method.
    /// </summary>
    public interface IFileProcessor
    {
        /// <summary>
        /// Processes the data from the file.
        /// </summary>
        /// <param name="bytesRead">The number of bytes read.</param>
        /// <param name="data">The byte array containing the data.</param>
        /// <param name="totalBytes">The total number of bytes expected.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public Task ProcessAsync(int bytesRead, byte[] data, double totalBytes);
    }
}