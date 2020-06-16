using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Natural.Language.TemplateEngine
{
    static class Extensions
    {
        public static void UpdateFiles(this string filepath, string oldvalue, string newValue)
        {
            var fileContent = new StringBuilder(File.ReadAllText(filepath));
            fileContent = fileContent.Replace(oldvalue, newValue);

            File.WriteAllText(filepath, fileContent.ToString());
        }

        public static void RemoveDataTag(this string filepath)
        {
            filepath.UpdateFiles(Constants.DataTagStart, string.Empty);
            filepath.UpdateFiles(Constants.DataTagEnd, string.Empty);
        }


        public static void UpdateKeyValues(this string filepath, Dictionary<string, string> replacementValues)
        {
            var fileContent = new StringBuilder(File.ReadAllText(filepath));
            foreach (var value in replacementValues)
            {
                fileContent = fileContent.Replace(value.Key, value.Value);
            }

            File.WriteAllText(filepath, fileContent.ToString());
        }

    }
}
