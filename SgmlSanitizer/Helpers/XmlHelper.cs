using System.Security;
using System.Xml;

namespace SgmlSanitizer.Helpers
{
	public static class XmlHelper
	{
		public static string GetXmlEscapedValue(this XmlReader xmlReader)
		{
			return SecurityElement.Escape(xmlReader.Value);
		}
	}
}
