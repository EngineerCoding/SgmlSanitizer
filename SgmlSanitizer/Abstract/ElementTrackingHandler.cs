namespace SgmlSanitizer.Abstract
{
	public abstract class ElementTrackingHandler : ISanitationHandler
	{
		protected string LastElement { get; set; }


		public virtual bool KeepElement(string name)
		{
			LastElement = name;
			return true;
		}

		public abstract bool KeepAttribute(string name, string value);
	}
}
