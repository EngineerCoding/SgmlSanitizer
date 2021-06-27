using System.Collections;
using System.Collections.Generic;

namespace SgmlSanitizer.List
{
	public class CaseInsentiveSet : ICollection<string>
	{
		private readonly ICollection<string> set = new HashSet<string>();

		public int Count => set.Count;

		public bool IsReadOnly => set.IsReadOnly;

		public void Add(string item)
		{
			set.Add(item.ToLower());
		}

		public void Clear()
		{
			set.Clear();
		}

		public bool Contains(string item)
		{
			return set.Contains(item.ToLower());
		}

		public void CopyTo(string[] array, int arrayIndex)
		{
			set.CopyTo(array, arrayIndex);
		}

		public IEnumerator<string> GetEnumerator()
		{
			return set.GetEnumerator();
		}

		public bool Remove(string item)
		{
			return set.Remove(item.ToLower());
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
