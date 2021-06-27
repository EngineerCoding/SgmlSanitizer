using Sgml;
using SgmlSanitizer.Collection;
using SgmlSanitizer.Helper;
using System.IO;
using System.Xml;

namespace SgmlSanitizer
{
	public class Sanitizer : CollectionHandler
	{
		public void Sanitize(string sgml, TextWriter outputWriter)
		{
			using (TextReader textReader = new StringReader(sgml))
			{
				Sanitize(textReader, outputWriter);
			}
		}

		public void Sanitize(TextReader textReader, TextWriter outputWriter)
		{
			SgmlReader sgmlReader = new SgmlReader()
			{
				DocType = "HTML",
				WhitespaceHandling = WhitespaceHandling.All,
				CaseFolding = CaseFolding.ToLower,
				InputStream = textReader,
			};

			using (sgmlReader)
			{
				Sanitize(sgmlReader, outputWriter);
			}
		}

		public void Sanitize(XmlReader xmlReader, TextWriter outputWriter)
		{
			SanitationContext context = new SanitationContext(this, outputWriter);
			while (xmlReader.Read())
			{
				switch (xmlReader.NodeType)
				{
					case XmlNodeType.Element:
						context.OnElement(xmlReader.Name, xmlReader.HasAttributes);
						if (xmlReader.HasAttributes)
						{
							for (int i = 0; i < xmlReader.AttributeCount; i++)
							{
								xmlReader.MoveToAttribute(i);
								context.OnAttribute(xmlReader.Name, xmlReader.GetXmlEscapedValue(), i + 1 == xmlReader.AttributeCount);
							}
							xmlReader.MoveToElement();
						}
						break;
					case XmlNodeType.EndElement:
						context.OnCloseElement(xmlReader.Name);
						break;
					case XmlNodeType.Text:
						context.OnText(xmlReader.GetXmlEscapedValue());
						break;
				}
			}
		}
	}
}
