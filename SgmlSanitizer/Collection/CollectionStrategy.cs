namespace SgmlSanitizer.Collection
{
	public enum CollectionStrategy
	{
		/// <summary>
		/// Filter if any of the handlers are filtering the element or attribute
		/// </summary>
		Any,
		/// <summary>
		/// Filter only if all handlers are filtering the element or attribute
		/// </summary>
		All,
	}
}
