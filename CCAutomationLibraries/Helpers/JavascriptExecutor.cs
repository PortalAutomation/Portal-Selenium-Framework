using OpenQA.Selenium;

namespace CCWebUIAuto.Helpers
{
	public static class JavascriptExecutor
	{
		public static T Execute<T>(string script, params object[] args)
		{
			return RetriableRunner.Run(() => (T) ((IJavaScriptExecutor) Web.Driver).ExecuteScript(script, args));
		}

		public static void Execute(string script, params object[] args)
		{
			RetriableRunner.Run(() => ((IJavaScriptExecutor) Web.Driver).ExecuteScript(script, args));
		}
	}
}
