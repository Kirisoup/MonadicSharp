using MonadicSharp.Iteration;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmarks>();

[MemoryDiagnoser]
public class Benchmarks
{
	static int[] arr = Enumerable.Range(0, 2000).ToArray();

    [Benchmark]
    public void ChainMapEach()
    {
		var map = arr.GetIterator()
			.MapEach(static x => x * x)
			.MapEach(static x => (int)Math.Sqrt(x));

		for (int i = 0; i < 2000; i++) {
			map	= map
			.MapEach(static x => x * x)
			.MapEach(static x => (int)Math.Sqrt(x));
		}
    }

    [Benchmark]
	public void ChainSelect()
	{
		var map = arr
			.Select(static x => x * x)
			.Select(static x => (int)Math.Sqrt(x));
		for (int i = 0; i < 2000; i++) {
			map	= map
			.Select(static x => x * x)
			.Select(static x => (int)Math.Sqrt(x));
		}
	}
}