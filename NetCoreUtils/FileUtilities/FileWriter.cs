using System.Text;

namespace NetCoreUtils.FileUtilities
{
    /// <summary>
    /// Utilities for writing to file.
    /// </summary>
    public static class FileWriter
    {
        /// <summary>
        /// Writes text asynchronously to the specified file. https://stackoverflow.com/a/21990015
        /// </summary>
        /// <param name="filePath">The path to the file to write to.</param>
        /// <param name="text">The text to write to the file.</param>
        /// <param name="encoding">The encoding to use for writing to the file.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. No return value is provided.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the filePath is null or empty.
        /// </exception>
        public static async Task WriteTextToFileAsync(string filePath, string text, Encoding encoding, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            const int defaultBuffer = 4096;
            const FileOptions defaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

            await using (var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, defaultBuffer, defaultOptions))
            {
                await using (var sw = new StreamWriter(fileStream, encoding))
                {
                    await sw.WriteAsync(text.AsMemory(), cancellationToken).ConfigureAwait(false);
                    await sw.FlushAsync().ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Writes the contents of a stream to a file.
        /// </summary>
        /// <param name="filePath">The path to the file to which the stream will be written.</param>
        /// <param name="sourceStream">
        /// The stream that contains the data to be written to the file.
        /// </param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if filePath or sourceStream is null or empty.
        /// </exception>
        public static async Task WriteStreamToFileAsync(string filePath, Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            ArgumentNullException.ThrowIfNull(sourceStream);

            const int defaultBuffer = 4096;
            const FileOptions defaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

            await using (var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write,
                                                          FileShare.None, defaultBuffer, defaultOptions))
            {
                if (sourceStream.CanSeek)
                {
                    sourceStream.Position = 0;
                }
                await sourceStream.CopyToAsync(fileStream, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Writes byte array asynchronously to the specified file.
        /// https://stackoverflow.com/a/21990015
        /// </summary>
        /// <param name="filePath">The path to the file to write to.</param>
        /// <param name="data">The text to write to the file.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>
        /// A Task that represents the asynchronous operation. No return value is provided.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the filePath is null or empty.
        /// </exception>
        public static async Task WriteByteArrayToFileAsync(string filePath, byte[] data, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            const int defaultBuffer = 4096;
            const FileOptions defaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

            await using (var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, defaultBuffer, defaultOptions))
            {
                await fileStream.WriteAsync(data, cancellationToken).ConfigureAwait(false);
                await fileStream.FlushAsync(cancellationToken).ConfigureAwait(false);
            }
        }
    }
}