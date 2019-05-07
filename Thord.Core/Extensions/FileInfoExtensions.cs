using System;
using System.IO;
using System.Threading.Tasks;

namespace Thord.Core.Extensions
{
    public static class FileInfoExtensions
    {
        #region Public Methods

        public static void CopyTo(this FileInfo file, string destination, Action<double> progressCallback)
        {
            var buffer = new byte[1024 * 1024];

            using (var source = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
            {
                var fileLength = source.Length;
                using (var dest = new FileStream(destination, FileMode.Create, FileAccess.Write))
                {
                    long totalBytes = 0;
                    var currentBlockSize = 0;

                    while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        totalBytes += currentBlockSize;
                        var persentage = (double)totalBytes * 100.0 / fileLength;

                        dest.Write(buffer, 0, currentBlockSize);
                        
                        progressCallback(persentage);
                        
                    }
                }
            }
        }

        #endregion
    }
}