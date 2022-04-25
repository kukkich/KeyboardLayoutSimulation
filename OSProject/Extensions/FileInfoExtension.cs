using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSProject.Extensions
{
    public static class FileInfoExtension
    {
        public static string NameWithoutExtension(this FileInfo file)
        {
            return file.Name.Remove(
                    file.Name.Length - file.Extension.Length
                );
        }
    }
}
