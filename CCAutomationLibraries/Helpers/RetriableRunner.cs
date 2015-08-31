using System;
using System.Diagnostics;
using System.Threading;

namespace CCWebUIAuto.Helpers
{
	public static class RetriableRunner
	{
		public const int MaxRetries = 10;
		public const int WaitMultiplier = 250;

		public static T Run<T>(Func<T> func, int retries = MaxRetries)
		{
			for (var i = 0; i < MaxRetries; i++) {
				try {
					return func();
				} catch (Exception e) {
					Trace.WriteLine("retriable exception (" + i + "): " + e.Message);
					if (i >= (retries - 1)) throw;
				}
				Thread.Sleep(i * WaitMultiplier);
			}
			throw new InvalidOperationException();
		}

		public static void Run(Action func, int retries = MaxRetries)
		{
			for (var i = 0; i < MaxRetries; i++) {
				try {
					func();
					return;
				} catch (Exception e) {
					Trace.WriteLine("retriable exception (" + i + "): " + e.Message);
					if (i >= (retries - 1)) throw;
				}
				Thread.Sleep(i * WaitMultiplier);
			}
			throw new InvalidOperationException();
		}
	}
}
