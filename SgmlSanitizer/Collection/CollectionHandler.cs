using System;
using System.Collections.Generic;
using System.Linq;

namespace SgmlSanitizer.Collection
{
	public class CollectionHandler : ISanitationHandler
	{
		public readonly ICollection<ISanitationHandler> Handlers = new HashSet<ISanitationHandler>();
		public CollectionStrategy ElementStrategy { get; set; }
		public CollectionStrategy AttributeStrategy { get; set; }

		public bool KeepElement(string name)
		{
			return KeepItem(ElementStrategy, (handler) => handler.KeepElement(name));
		}

		private bool KeepItem(CollectionStrategy strategy, Func<ISanitationHandler, bool> keepItemFunc)
		{
			switch (strategy)
			{
				case CollectionStrategy.All:
					return !Handlers.All(handler => !keepItemFunc.Invoke(handler));
				case CollectionStrategy.Any:
					return !Handlers.Any(handler => !keepItemFunc.Invoke(handler));
				default:
					throw new NotImplementedException(strategy.ToString());
			}
		}

		private IEnumerable<bool> GetBools(Func<ISanitationHandler, bool> keepItemFunc)
		{
			return Handlers.Select(keepItemFunc).ToArray();
		}

		public bool KeepAttribute(string name, string value)
		{
			return KeepItem(AttributeStrategy, (handler) => handler.KeepAttribute(name, value));
		}
	}
}
