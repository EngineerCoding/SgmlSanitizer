namespace SgmlSanitizer
{
	public interface ISanitationHandler
	{
		bool KeepElement(string name);

		bool KeepAttribute(string name, string value);
	}
}
