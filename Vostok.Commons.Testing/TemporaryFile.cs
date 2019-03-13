using System;
using System.IO;
using JetBrains.Annotations;

namespace Vostok.Commons.Testing
{
    [PublicAPI]
    internal class TemporaryFile : IDisposable
    {
        public TemporaryFile(string content = null)
        {
            FileName = Path.GetTempFileName();
            if (content != null)
                File.WriteAllText(FileName, content);
        }

        public string FileName { get; }

        public void Dispose()
        {
            if (File.Exists(FileName))
                File.Delete(FileName);
        }
    }
}