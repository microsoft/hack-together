using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystemAbstraction
{
    public static class UploadDocsFunctions
    {
		public static string RemoveIllegalCharacters(string source, char[] listofillegalchars)
		{
			string regexSearch = new string(listofillegalchars);
			var r = new System.Text.RegularExpressions.Regex(string.Format("[{0}]", System.Text.RegularExpressions.Regex.Escape(regexSearch)));
			return r.Replace(source, "");
		}

	}
}
