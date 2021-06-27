using SgmlSanitizer.Abstract;
using SgmlSanitizer.Helpers;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SgmlSanitizer.UrlAttribute
{
	/// <summary>
	/// Validates whether attributes contian url like strings. This should be avoided at all cost. Note that this handler
	/// is not complete by any means at all, since with javascript attributes code can be run which means that strings don't have to available
	/// as a single string. Therefore with on* attributes, danger must be acknowledged by specifically.
	/// This is a very naive implementation, if a slash or dot is detected we assume this is an url.
	/// </summary>
	public class UrlAttributeHandler : ElementTrackingHandler, ISanitationHandler
	{
		private const string JavascriptMarker = "on";
		private const string UrlRegexMarker = "[/\\.]";

		private static bool AcknowledgedJsDanger = false;

		private readonly IDictionary<string, ICollection<string>> exemptElementAttributes = new Dictionary<string, ICollection<string>>();
		private readonly ICollection<string> exemptAttributes = new HashSet<string>();

		public void AddExemptAttribute(string attribute)
		{
			exemptAttributes.Add(attribute.ToLower());
		}

		public void AddExemptAttribute(string element, string attribute)
		{
			exemptElementAttributes.AddToDictionaryCollection(element.ToLower(), attribute.ToLower());
		}

		public override bool KeepAttribute(string name, string value)
		{
			name = name.ToLower();
			if (exemptAttributes.Contains(name) || (exemptElementAttributes.TryGetValue(LastElement, out ICollection<string> elExemptAttributes) && elExemptAttributes.Contains(name)))
				return true;

			if (!AcknowledgedJsDanger && name.StartsWith(JavascriptMarker))
			{
				throw new JavascriptAttributeUnsafeException();
			}

			MatchCollection matches = Regex.Matches(value, UrlRegexMarker);
			return matches.Count == 0;
		}

		/// <summary>
		/// Read class documentation
		/// </summary>
		public static void AcknowledgeJavascriptAttributeDanger()
		{
			AcknowledgedJsDanger = true;
		}
	}
}
