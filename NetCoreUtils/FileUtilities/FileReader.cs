using System.Text;

namespace NetCoreUtils.FileUtilities
{
    /// <summary>
    /// Utilities for reading file.
    /// </summary>
    public static class FileReader
    {
        /// <summary>
        /// Asynchronously reads all lines from a text file using the specified encoding.
        /// https://stackoverflow.com/a/13168006
        /// </summary>
        /// <param name="filePath">The path to the text file.</param>
        /// <param name="encoding">The encoding to use for reading the file.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. The result is an array of strings
        /// containing the lines from the file.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the filePath is null or empty.
        /// </exception>
        public static async Task<string[]> ReadAllLinesAsync(string filePath, Encoding encoding, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            const int defaultBuffer = 4096;
            const FileOptions defaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

            var lines = new List<string>();

            await using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, defaultBuffer, defaultOptions))
            {
                using (var reader = new StreamReader(fileStream, encoding))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }

            return lines.ToArray();
        }

        /// <summary>
        /// Asynchronously reads the contents of a file, process readed data and reports progress.
        /// https://stackoverflow.com/a/22617832
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <param name="dataProcessor">
        /// Implementation of IFileProcessor, that process readed file data.
        /// </param>
        /// <param name="progressReporter">
        /// An object that implements the IProgress interface to report progress during the read
        /// operation.
        /// </param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. No return value is provided.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the filePath is null or empty.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the dataProcessor is null.
        /// </exception>
        public static async Task ReadFileAsync(string filePath, IFileProcessor dataProcessor, IProgress<double> progressReporter, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            ArgumentNullException.ThrowIfNull(dataProcessor);

            double totalBytes = new FileInfo(filePath).Length;
            var bufferSize = 1024 * 4;
            var buffer = new byte[bufferSize];
            var bytesRead = 0;
            var totalBytesRead = 0L;

            const int defaultBuffer = 4096;
            const FileOptions defaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

            await using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read,
                                                 FileShare.None, defaultBuffer, defaultOptions))
            {
                do
                {
                    bytesRead = await fileStream.ReadAsync(buffer.AsMemory(0, bufferSize), cancellationToken).ConfigureAwait(false);

                    await dataProcessor.ProcessAsync(bytesRead, buffer, totalBytes).ConfigureAwait(false);

                    totalBytesRead += bytesRead;
                    var fractionDone = totalBytesRead / totalBytes;

                    if (progressReporter != null)
                        progressReporter.Report(fractionDone * 100);
                } while (bytesRead > 0);
            }
        }
    }
}