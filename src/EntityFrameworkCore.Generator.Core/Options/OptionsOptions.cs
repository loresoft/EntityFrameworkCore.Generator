using System;
using System.IO;

namespace EntityFrameworkCore.Generator.Options
{
    /// <summary>
    /// Options file meta data.
    /// </summary>
    public class OptionsOptions : OptionsBase
    {
        public OptionsOptions(VariableDictionary variables, string prefix)
            : base(variables, AppendPrefix(prefix, "Options"))
        {
        }

        /// <summary>
        /// Gets the full path to the current options file.
        /// This value is computed and read-only, and cannot be set.
        /// </summary>
        /// <value>
        /// The full path to the options file.
        /// </value>
        public string FullPath
        {
            get { return GetProperty(); }
            set { }
        }

        /// <summary>
        /// Gets just the directory containing the current options file.
        /// This value is computed and read-only, and cannot be set.
        /// </summary>
        /// <value>
        /// The directory containing the options file.
        /// </value>
        public string Directory
        {
            get { return GetProperty(); }
            set { }
        }

        /// <summary>
        /// Gets just the file name of the current options file.
        /// This value is computed and read-only, and cannot be set.
        /// </summary>
        /// <value>
        /// The file name of the options file.
        /// </value>
        public string FileName
        {
            get { return GetProperty(); }
            set { }
        }

        /// <summary>
        /// Gets just the file name without any extension of the current options file.
        /// This value is computed and read-only, and cannot be set.
        /// </summary>
        /// <value>
        /// The file name without any extension of the options file.
        /// </value>
        public string FileNameWithoutExtension
        {
            get { return GetProperty(); }
            set { }
        }

        /// <summary>
        /// Sets the full path to the current options file.
        /// This will also resolve the Directory and File Name.
        /// </summary>
        public void SetFullPath(string path)
        {
            SetProperty(path, nameof(FullPath));
            SetProperty(Path.GetDirectoryName(path), nameof(Directory));
            SetProperty(Path.GetFileName(path), nameof(FileName));

            // Special handling for *.efg.* sub-extension
            var filename = Path.GetFileNameWithoutExtension(path);
            if (filename.EndsWith(".efg", StringComparison.OrdinalIgnoreCase))
            {
                filename = Path.GetFileNameWithoutExtension(filename);
            }
            SetProperty(filename, nameof(FileNameWithoutExtension));
        }
    }
}