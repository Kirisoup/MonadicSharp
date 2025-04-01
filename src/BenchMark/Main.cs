using MonadicSharp.Iteration;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

// BenchmarkRunner.Run<Benchmarks>();
BenchmarkRunner.Run<Benchmarks>();

[MemoryDiagnoser]
public class Benchmarks
{
	static readonly int[] arr = Enumerable.Range(0, 2000).ToArray();

    // [Benchmark]
    // public void ChainMapEach()
    // {
	// 	var map = arr.GetIterator()
	// 		.MapEach(static (int x) => x * x)
	// 		.MapEach(static (int x) => x * x)
	// 		.MapEach(static (int x) => x * x)
	// 		.MapEach(static (int x) => x * x)
	// 		.MapEach(static (int x) => x * x)
	// 		.MapEach(static (int x) => x * x)
	// 		.MapEach(static (int x) => x * x)
	// 		.MapEach(static (int x) => x * x)
	// 		.MapEach(static (int x) => x * x)
	// 		.MapEach(static (int x) => x * x)
	// 		.MapEach(static (int x) => x * x)
	// 		.MapEach(static (int x) => x * x)
	// 		.MapEach(static (int x) => x * x)
	// 		.MapEach(static (int x) => x * x)
	// 		.MapEach(static (int x) => x * x)
	// 		.MapEach(static (int x) => x * x);
    // }

    // [Benchmark]
	// public void ChainSelect()
	// {
	// 	var map = arr
	// 		.Select(static (int x) => x * x)
	// 		.Select(static (int x) => x * x)
	// 		.Select(static (int x) => x * x)
	// 		.Select(static (int x) => x * x)
	// 		.Select(static (int x) => x * x)
	// 		.Select(static (int x) => x * x)
	// 		.Select(static (int x) => x * x)
	// 		.Select(static (int x) => x * x)
	// 		.Select(static (int x) => x * x)
	// 		.Select(static (int x) => x * x)
	// 		.Select(static (int x) => x * x)
	// 		.Select(static (int x) => x * x)
	// 		.Select(static (int x) => x * x)
	// 		.Select(static (int x) => x * x)
	// 		.Select(static (int x) => x * x)
	// 		.Select(static (int x) => x * x);
	// }

    [Benchmark]
    public void ExecChainMap()
	{
		var map = arr.GetIterator()
			.Map(static (int x) => x * x)
			.MapEach(static (int x) => x * x)
			.MapEach(static (int x) => x * x)
			.MapEach(static (int x) => x * x)
			.MapEach(static (int x) => x * x)
			.MapEach(static (int x) => x * x)
			.MapEach(static (int x) => x * x)
			.MapEach(static (int x) => x * x)
			.MapEach(static (int x) => x * x)
			.MapEach(static (int x) => x * x)
			.MapEach(static (int x) => x * x)
			.MapEach(static (int x) => x * x)
			.MapEach(static (int x) => x * x)
			.MapEach(static (int x) => x * x)
			.MapEach(static (int x) => x * x)
			.MapEach(static (int x) => x * x);

		map.ForEach(static x => {});
	}

	[Benchmark]
	public void ExecChainSelect()
	{
		var map = arr
			.Select(static (int x) => x * x)
			.Select(static (int x) => x * x)
			.Select(static (int x) => x * x)
			.Select(static (int x) => x * x)
			.Select(static (int x) => x * x)
			.Select(static (int x) => x * x)
			.Select(static (int x) => x * x)
			.Select(static (int x) => x * x)
			.Select(static (int x) => x * x)
			.Select(static (int x) => x * x)
			.Select(static (int x) => x * x)
			.Select(static (int x) => x * x)
			.Select(static (int x) => x * x)
			.Select(static (int x) => x * x)
			.Select(static (int x) => x * x)
			.Select(static (int x) => x * x);

		map.ForEach(static (int x) => {});
	}

	// [Benchmark]
	// public void ExecForLoop() 
	// {
	// 	for (int i = 0; i < arr.Length; i++) {
	// 		int x = arr[i];
	// 		x *= x;
	// 		x *= x;
	// 		x *= x;
	// 		x *= x;
	// 		x *= x;
	// 		x *= x;
	// 		x *= x;
	// 		x *= x;
	// 		x *= x;
	// 		x *= x;
	// 		x *= x;
	// 		x *= x;
	// 		x *= x;
	// 		x *= x;
	// 		x *= x;
	// 		x *= x;
	// 	}
	// }
}