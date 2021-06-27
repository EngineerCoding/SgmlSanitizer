using System.Security;
using System.Xml;

namespace SgmlSanitizer.Helper
{
	public static class XmlHelper
	{
		public static string GetXmlEscapedValue(this XmlReader xmlReader)
		{
			return SecurityElement.Escape(xmlReader.Value);
		}
	}
}
