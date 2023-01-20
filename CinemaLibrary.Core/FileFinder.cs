using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CinemaLibrary.Core
{
    public class FileFinder
    {
        private static string? File_name;
        private static bool File_has_name;
        public FileFinder(string file_name)
        {
            File_name = file_name;
            File_has_name = FileHasName();
        }
        private bool FileHasName()
        {
            if (File_name == null || File_name == "")
                return false;
            else
                return true;
        }
        public string FindFilePath()
        {
            string part = string.Join(@"\", Directory.GetCurrentDirectory().Split("\\").Take(2).ToList());
            string path = $@"{part}\Zadachki zakachki\SeedingFiles";
            var files = Directory.GetFiles($"{path}").ToList();
            files.RemoveAll(x => !x.Contains(File_name));
            return files.First();
        }
        public static int? GetFileId()
        {
            if (File_has_name)
                return File_name.ToList().RemoveAll(x => !Enumerable.Range(0, 9).Contains(x));
            else
                return null;
        }

        public List<string> GetFileContents(string file_path)
        {
            string line;
            var contents = new List<string>();

            using (StreamReader sr = new StreamReader(file_path))
                while ((line = sr.ReadLine()) != null)
                    contents.Add(line);

            return contents;
        }
    }
}
