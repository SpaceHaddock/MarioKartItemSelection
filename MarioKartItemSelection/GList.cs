using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioKartItemSelection
{
	class GList
	{
		public static List<T> MakeList<T>(int size, T initial_value = default(T))
		{
			List<T> result = new List<T>();
			for (int i = 0; i < size; i++)
				result.Add(initial_value);
			return result;
		}
	}
}
