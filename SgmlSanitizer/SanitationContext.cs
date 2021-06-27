using System.IO;

namespace SgmlSanitizer
{
	public class SanitationContext
	{
		private const char ElementStart = '<';
		private const char ElementEnd = '>';
		private const char ClosingIndicator = '/';

		private const char Space = ' ';
		private const char Quote = '"';
		private const char AttributeSeparator = '=';

		private readonly ISanitationHandler sanitationHandler;
		private readonly TextWriter textWriter;

		private string ignoreElement;
		private int ignoreCounter;

		public SanitationContext(ISanitationHandler sanitationHandler, TextWriter textWriter)
		{
			this.sanitationHandler = sanitationHandler;
			this.textWriter = textWriter;
		}

		public void OnElement(string element, bool hasAttributes)
		{
			if (ignoreElement == null)
			{
				if (sanitationHandler.KeepElement(element))
				{
					textWriter.Write(ElementStart);
					textWriter.Write(element);
					if (!hasAttributes)
						textWriter.Write(ElementEnd);
				}
				else
				{
					ignoreElement = element;
					ignoreCounter = 1;
				}
			}
			else
			{
				if (ignoreElement == element)
					ignoreCounter += 1;
			}
		}

		public void OnCloseElement(string element)
		{
			if (element == ignoreElement)
			{
				ignoreCounter -= 1;
				if (ignoreCounter == 0)
				{
					ignoreElement = null;
				}
			}
			else if (ignoreElement == null)
			{
				textWriter.Write(ElementStart);
				textWriter.Write(ClosingIndicator);
				textWriter.Write(element);
				textWriter.Write(ElementEnd);
			}
		}

		public void OnAttribute(string name, string value, bool lastAttribute)
		{
			if (ignoreElement != null)
				return;

			if (sanitationHandler.KeepAttribute(name, value))
			{
				textWriter.Write(Space);
				textWriter.Write(name);
				textWriter.Write(AttributeSeparator);
				textWriter.Write(Quote);
				textWriter.Write(value);
				textWriter.Write(Quote);
			}

			if (lastAttribute)
			{
				textWriter.Write(ElementEnd);
			}
		}

		public void OnText(string text)
		{
			if (ignoreElement == null)
			{
				textWriter.Write(text);
			}
		}
	}
}
