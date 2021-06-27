using System.Collections.Generic;

namespace SgmlSanitizer.Helpers
{
	public static class CollectionHelper
	{
		public static void AddToDictionaryCollection<TKey, TValue>(this IDictionary<TKey, ICollection<TValue>> container, TKey key, TValue item)
		{
			bool addToDictionary = false;
			if (!container.TryGetValue(key, out ICollection<TValue> data))
			{
				data = new HashSet<TValue>();
				addToDictionary = true;
			}

			data.Add(item);
			if (addToDictionary)
				container.Add(key, data);
		}
	}
}
