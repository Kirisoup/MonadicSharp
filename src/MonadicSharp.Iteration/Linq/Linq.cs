namespace MonadicSharp.Iteration;

partial struct Iterator<Impl, T> 
{
	public Option<T> Next() => _iter.Move(out var item) 
		? Option.Val(item) 
		: Option.Nil<T>();

	public Option<T> NextBack() => _iter.MoveBack(out var item) 
		? Option.Val(item) 
		: Option.Nil<T>();

	public void ForEach(Action<T> action) {
		while (_iter.Move(out var item)) action(item);
		Dispose();
	}

	public int Count() {
		int n;
		if ((n = _iter.Size()) >= 0) return n;
		n = 0;
		while (_iter.Move(out _)) n++;
		return n;
	}

	public Iterator<Adapter.Map<Impl, T, U>, U> Map<U>(Func<T, U> map) => new(new(ref _iter, map));

	public Iterator<Adapter.Filter<Impl, T>, T> Filter(Predicate<T> filter) => 
		new(new(ref _iter, filter));

	public Iterator<Adapter.FilterMap<Impl, T, U>, U> FilterMap<U>(Func<T, Option<U>> filterMap) =>
		new(new(ref _iter, filterMap));

	public Iterator<Adapter.Index<Impl, T>, (int Index, T Item)> Index() =>
		new(new(ref _iter, 0));
	
	public Iterator<Adapter.Inspect<Impl, T>, T> Inspect(Action<T> inspect) => 
		new(new(ref _iter, inspect));
}