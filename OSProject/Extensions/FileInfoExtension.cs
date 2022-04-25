using System.IO;

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
