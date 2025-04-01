using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using MonadicSharp.Iteration;

[Config(typeof(Config))]
[MemoryDiagnoser]
public class SomeBenchmark
{
	private class Config : ManualConfig
	{
		public Config() {
			AddJob(Job.MediumRun
				.WithLaunchCount(1)
				.WithToolchain(InProcessEmitToolchain.Instance)
				.WithId("InProcess"));
		}
	}

	readonly int[] arr = Enumerable.Range(1, 20).ToArray();
	int __;

	[Benchmark]
	public void Iter() {
		arr.ToIter()
			.Map(x => x * x)
			.Filter(x => x > 10)
			.ForEach(x => __ = x)
		;
	}

	[Benchmark]
	public void Linq() {
		foreach (var x in arr
			.Select(x => x * x)
			.Where(x => x > 10))
		{
			__ = x;
		}
	}

	[Benchmark]
	public void BigIter() {
		arr.ToIter()
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.ForEach(x => __ = x)
		;
	}

	[Benchmark]
	public void BigLinq() {
		foreach (var x in arr
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10))
		{
			__ = x;
		}
	}

	[Benchmark]
	public void HugeIter() {
		arr.ToIter()
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.Map(x => x * x)
			.Filter(x => x > 10)
			.ForEach(x => __ = x)
		;
	}

	[Benchmark]
	public void HugeLinq() {
		foreach (var x in arr
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10)
			.Select(x => x * x)
			.Where(x => x > 10))
		{
			__ = x;
		}
	}
}
