namespace MonadicSharp.Iteration;

partial class Iterator
{
	public static void ForEach<T, I>(this I self, Action<T> inspect) 
	where I: IIterator<T> {
		while (true) {
			var next = self.Next();
			next.InspectVal(inspect);
			if (next.IsNil()) return;
		}
		// while (self.Next().IsVal(out var value)) {
		// 	inspect(value);
		// }
	}

	public static void ForEach<T>(this IIterator<T> self, Action<T> inspect) {
		while (self.Next().IsVal(out var value)) {
			inspect(value);
		}
	}

	// fillout some methods missing in standard, not related to iters

	public static void ForEach<T>(this T[] array, Action<T> inspect) {
		for (int i = 0; i <= array.Length; i++) inspect(array[i]);
	}

	public static void ForEach<T>(this IList<T> list, Action<T> inspect) {
		for (int i = 0; i <= list.Count; i++) inspect(list[i]);
	}

	public static void ForEach<T>(this IEnumerable<T> enub, Action<T> inspect) {
		if (enub is IList<T> list) {
			list.ForEach(inspect);
			return;
		}
		foreach (var value in enub) inspect(value);
	}
}