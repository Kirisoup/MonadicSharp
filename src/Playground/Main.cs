using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Running;
using MonadicSharp.IterMonad;

BenchmarkRunner.Run<SomeBenchmark>();
// var bm = new SomeBenchmark();

// try {
// 	bm.HugeIter();
// 	bm.HugeIterBoxed();
// } catch (Exception ex) {
// 	ex.Dump();
// }

// string[] arr = Enumerable.Range(1, 20).Select(x => x.ToString()).ToArray();

// arr.ToIter()
// 	.Map(x => x + x)
// 	.Filter(x => x.Length > 2)
// 	.Map(x => x + x)
// 	.ForEach(x => x.Dump());
		// The type 'MapAdapter<
		//   MapAdapter<StandardIterators.ArrayIter<string>, 
		//   string, string>, string, string>' 
		// may not be a ref struct or a type parameter allowing ref structs in order to use it 
		// as parameter 'I' in the generic type or method 'Iterator.ForEach<T, I>(I, Action<T>)'
		// (CS9244)

// _ = default(MapAdapter<StandardIterators.ArrayIter<int>, int, int>);

// _ = default(MapAdapter<MapAdapter<StandardIterators.ArrayIter<int>, int, int>, int, int>);
// SomeManagedData[] someManagedArray = [new("Hello"), new(", "), new("World"), new("! ")];

// new Span<SomeManagedData>(someManagedArray)[1].Dump();

// record struct SomeManagedData(string Name);

static class Ext
{
	internal static T Dump<T>(this T obj) {
		Console.WriteLine(obj);
		return obj;
	}
}