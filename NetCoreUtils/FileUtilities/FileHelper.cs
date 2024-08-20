namespace NetCoreUtils.FileUtilities
{
    /// <summary>
    /// Utilities for file manipulation.
    /// </summary>
    public static class FileHelper
    {
        private static readonly char[] sourceArray = new char[] { '?', '{', '}', '&', ':', '|', '[', ']', '<', '>', '~', '#', '%' };

        /// <summary>
        /// Removes the invalid file name characters http://jon-martin.com/?p=323
        /// </summary>
        /// <param name="fileName">The file name to check for invalid characters</param>
        /// <returns>The file name without invalid characters</returns>
        public static string RemoveInvalidFilenameCharacters(this string fileName)
        {
            int i;
            // Invalid file name chars
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa368590(v=vs.85).aspx
            var invalidFilenameChars = sourceArray;

            if (!string.IsNullOrEmpty(fileName))
            {
                List<char> invalidChars = new List<char>();

                invalidChars.AddRange(invalidFilenameChars);
                invalidChars.AddRange(Path.GetInvalidPathChars());
                invalidChars.AddRange(Path.GetInvalidFileNameChars());
                invalidChars.AddRange(new char[] { Path.PathSeparator, Path.AltDirectorySeparatorChar });
                for (i = 0; i < invalidChars.Count; ++i)
                {
                    fileName = fileName.Replace(invalidChars[i].ToString(), string.Empty);
                }
            }
            return fileName;
        }

        /// <summary>
        /// Determines whether file name has invalid chars
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>Returns true if file name contains invalid chars, otherwise false</returns>
        public static bool HasFileNameInvalidChars(this string fileName)
        {
            return !string.IsNullOrEmpty(fileName) && fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0;
        }

        /// <summary>
        /// Determines whether file path has invalid chars
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <returns>Returns true if file path contains invalid chars, otherwise false</returns>
        public static bool HasFilePathInvalidChars(this string filePath)
        {
            return !string.IsNullOrEmpty(filePath) && filePath.IndexOfAny(Path.GetInvalidPathChars()) >= 0;
        }

        /// <summary>
        /// Gets the file size in bytes
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <returns>Returns file size</returns>
        public static long GetFileSizeInBytes(this string filePath)
        {
            if (File.Exists(filePath))
            {
                return new FileInfo(filePath).Length;
            }
            return 0;
        }

        /// <summary>
        /// Determines size of the file and add bytes abbreviation ("B", "KB", "MB", "GB", "TB")
        /// https://stackoverflow.com/a/281679
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <returns>Returns file size with abbreviation.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the filePath is null or empty.
        /// </exception>
        public static string FileSizeWithBytesAbbreviation(this string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            long fileLength = filePath.GetFileSizeInBytes();
            int order = 0;
            while (fileLength >= 1024 && order < sizes.Length - 1)
            {
                order++;
                fileLength = fileLength / 1024;
            }

            // "{0:0.#}{1}" would show a single decimal place, and no space.
            string result = string.Format("{0:0.##} {1}", fileLength, sizes[order]);

            return result;
        }

        /// <summary>
        /// Zatim nedavat na Github
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string[] SplitFileNameAndExtension(this string fileName)
        {
            if (!fileName.HasFileNameValidExtension())
            {
                throw new ArgumentException("File name has not valid extension.", nameof(fileName));
            }

            string[] result = new string[2];
            result[0] = fileName.Substring(0, fileName.LastIndexOf('.'));
            result[1] = Path.GetExtension(fileName);

            return result;
        }

        /// <summary>
        /// Zatim nedavat na Github
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool HasFileNameValidExtension(this string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException($"'{nameof(fileName)}' cannot be null or empty.", nameof(fileName));
            }

            var extension = Path.GetExtension(fileName);

            string[] allowedExtensions = { ".txt", ".jpg" };

            var result = allowedExtensions.Contains(extension);

            return result;
        }

        public static string GetApplicationPath(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
                return AppDomain.CurrentDomain.BaseDirectory;
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\" + folderName);
            return AppDomain.CurrentDomain.BaseDirectory + folderName + "\\";
        }
    }
}