using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent2020.Helpers
{
    public static class FileHelper
    {
        public static List<T> GetFileContents<T>(string filePath)
        {
            var type = typeof(T);
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = type.GenericTypeArguments[0];
            }

            // get the file contents
            List<string> list = new List<string>();
            string contents;

            try
            {
                using (var sr = new StreamReader(filePath))
                {
                    contents = sr.ReadToEnd();
                }
            }
            catch (FileNotFoundException ex)
            {
                contents = ex.Message;
            }

            list = contents.Split("\n").ToList();

            return list.Select(v => v != null ? (T)Convert.ChangeType(v, type) : default(T)).ToList();
        }
    }
}
