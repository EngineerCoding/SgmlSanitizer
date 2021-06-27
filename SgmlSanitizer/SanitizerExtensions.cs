using System;
using System.IO;
using System.Xml;

namespace SgmlSanitizer
{
	public static class SanitizerExtensions
	{
		private static string GetString(Action<StringWriter> stringWriterAction)
		{
			StringWriter output = new StringWriter();
			stringWriterAction.Invoke(output);
			return output.ToString();
		}

		public static string Sanitize(this Sanitizer sanitizer, string sgml)
		{
			return GetString(output => sanitizer.Sanitize(sgml, output));
		}

		public static string Sanitize(this Sanitizer sanitizer, TextReader textReader)
		{
			return GetString(output => sanitizer.Sanitize(textReader, output));
		}

		public static string Sanitize(this Sanitizer sanitizer, XmlReader xmlReader)
		{
			return GetString(output => sanitizer.Sanitize(xmlReader, output));
		}
	}
}
