using System;
using System.Runtime.Serialization;

namespace SgmlSanitizer.UrlAttribute
{
	[Serializable]
	public class JavascriptAttributeUnsafeException : Exception
	{
		private const string DefaultMessage = "Javascript attributes, on* attributes, may contain non clear url's which are not caught by this handler.";

		public JavascriptAttributeUnsafeException()
			: this(DefaultMessage)
		{
		}

		public JavascriptAttributeUnsafeException(string message) : base(message)
		{
		}

		public JavascriptAttributeUnsafeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected JavascriptAttributeUnsafeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
