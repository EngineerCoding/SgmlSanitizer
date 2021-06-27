using SgmlSanitizer.Abstract;
using SgmlSanitizer.Helpers;
using System.Collections.Generic;

namespace SgmlSanitizer.List
{
	public class ListSanitationHandler : ElementTrackingHandler, ISanitationHandler
	{
		private readonly IDictionary<string, ICollection<string>> ElementAttributes = new Dictionary<string, ICollection<string>>();

		public readonly ICollection<string> Elements = new CaseInsentiveSet();
		public readonly ICollection<string> Attributes = new CaseInsentiveSet();

		public ListType ElementListType { get; set; }
		public ListType AttributeListType { get; set; }

		public void AddElementAttribute(string element, string attribute)
		{
			ElementAttributes.AddToDictionaryCollection(element.ToLower(), attribute.ToLower());
		}

		public override bool KeepElement(string name)
		{
			base.KeepElement(name);

			bool contains = Elements.Contains(name);
			return ModifyContains(ElementListType, contains);
		}

		private static bool ModifyContains(ListType listType, bool contains)
		{
			if (listType == ListType.Blacklist)
			{
				return !contains;
			}
			return contains;
		}

		public override bool KeepAttribute(string name, string value)
		{
			bool contains = Attributes.Contains(name);
			if (!contains && ElementAttributes.TryGetValue(LastElement, out ICollection<string> attributes))
			{
				contains = attributes.Contains(name.ToLower());
			}

			return ModifyContains(AttributeListType, contains);
		}
	}
}
