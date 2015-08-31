using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using CCWebUIAuto.Helpers;
using CommonUtilities;
using OpenQA.Selenium.Support.UI;

namespace CCWebUIAuto.Pages
{
    public static class PopUpWindow
    {
        /// <summary>
        /// Use this field as a means of switching back to the parent window
        /// </summary>
        public static string ParentWindowHandle { get; set; }
        public static string CurrentWindowHandle { get; set; }
        public static string ParentWindowTitle { get; set; }

        public static void SwitchTo(string windowTitle, bool partialMatch = false)
        {
            RetriableRunner.Run(() =>
            {
                var handles = Web.Driver.WindowHandles;

				try {
					var handle = partialMatch
					? handles.First(h => Web.Driver.SwitchTo().Window(h).Title.Contains(windowTitle))
					: handles.First(h => Web.Driver.SwitchTo().Window(h).Title == windowTitle);
					Web.Driver.SwitchTo().Window(handle);
				} catch (Exception) {
					var windowTitles = handles.Select(h => Web.Driver.SwitchTo().Window(h).Title);
					var titleList = String.Join(",", windowTitles);
					Trace.WriteLine("unable to find '" + windowTitle + "'");
					Trace.WriteLine("available windows: [" + titleList + "]");
					throw;
				}
				var wait = new WebDriverWait(Web.Driver, TimeSpan.FromSeconds(10));
                try
                {
                    wait.Until(ExpectedConditions.TitleContains(windowTitle));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex, false);
                }
            });
        }

        public static void SwitchToByHandle(string handle)
        {
			Web.Driver.SwitchTo().Window(handle);
        }

        public static bool WindowTitleExists(string windowTitle)
        {
			return (Web.Driver.Title == windowTitle);
        }

        public static void Close()
        {
            // close the current window
			Web.Driver.Close();
            // switch focus back to parentWindow
            if (ParentWindowHandle != null || ParentWindowHandle != "")
            {
				Web.Driver.SwitchTo().Window(ParentWindowHandle);
            }
        }

        public static Boolean IsOpen(String windowTitle, bool partialMatch = false)
        {
            var currentWindow = CCPage.CurrentWindowTitle;
            var count = 0;
            RetriableRunner.Run(() =>
            {
				var handles = Web.Driver.WindowHandles;
                count = partialMatch
					? handles.Count(h => Web.Driver.SwitchTo().Window(h).Title.Contains(windowTitle))
					: handles.Count(h => Web.Driver.SwitchTo().Window(h).Title == windowTitle);
            });
            SwitchTo(currentWindow);
            return count > 0;
        }
    }

	// currently unused
    public static class PopUpWindowManager
    {
        public static HashSet<string> BaseHandleSet = new HashSet<string>();
        private static HashSet<string> currentHandleSet = new HashSet<string>();
        public static Stack<string> WindowFocusStack = new Stack<string>();
        
        public static void AddToFocusStack(string handle)
        {
            WindowFocusStack.Push(handle);
        }

        public static void RemoveFromFocusStack(string handle)
        {
           // string handleRemoved = WindowFocusStack.Pop();
            Trace.WriteLine(String.Format("Window "));
        }

        public static void ClearBaseHandleSet()
        {
            BaseHandleSet.Clear();
        }

        public static void ClearWindowFocusStack()
        {
            WindowFocusStack.Clear();
        }

        public static void AddToCurrentHandles()
        {
            
        }

        public static void RemoveFromCurrentHandles()
        {
            
        }

        // This method should be used whenever init is called or an interaction with an element is required.
        // It will focus on the top most window.
        public static void FocusTopWindow()
        {
            // get currentWindowHandles, put it in a temporary hashset

			var currentHandles = Web.Driver.WindowHandles;
            try
            {
                foreach (string handle in currentHandles)
                {
                    currentHandleSet.Add(handle);
                }
                if (currentHandleSet.Count == BaseHandleSet.Count)
                {
                    // do nothing
                    return;
                }
                    // if (currentHandles > baseSet) (there is a new window or popup)
                    // diff the two sets for one handle
                    // if more than one handle is different, throw an exception?  Not sure which to focus window on
                    // add the handle to focus stack
                if (currentHandleSet.Count > BaseHandleSet.Count)
                {
                    // BUG -- do not manipulate SETS after diffing
                    currentHandleSet.ExceptWith(BaseHandleSet);
                    if (currentHandleSet.Count > 1)
                    {
                        // THROW EXCEPTION -- SHOULD NOT be more than one handle difference
                    }
                    string diffHandle = currentHandleSet.FirstOrDefault();
                    PopUpWindow.SwitchToByHandle(diffHandle);
                    WindowFocusStack.Push(diffHandle);
                    GetLatestWindowHandles();
                    return;
                }
                    // if (currentHandles < baseSet) (there are fewer windows or popup)
                    // diff the two sets for one handle
                    // if more than one handle is different, throw an exception?  Not sure which to focus window on 
                if (currentHandleSet.Count < BaseHandleSet.Count)
                {
                    // BUG -- do not manipulate SETS after diffing
                    BaseHandleSet.ExceptWith(currentHandleSet);
                    string handleToRemove = WindowFocusStack.Pop();
                    //BaseHandleSet.Remove(handleToRemove);
                    // verify that the window being removed, is the one that was different between two sets
                    //string diffHandle = BaseHandleSet.FirstOrDefault();
                    if (!BaseHandleSet.Contains(handleToRemove))
                    {
                        // THROW EXCEPTION
                    }
                    PopUpWindow.SwitchToByHandle(WindowFocusStack.Peek());
                    GetLatestWindowHandles();
                    //return;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
            

            // other notes:  clear the currentWindowHandles and focusStack before starting up

            
            // determine if the size of the two sets are equal
               // if equal, do nothing and set the focus on the one window handle
               // if (currentHandles > baseSet)
                    // diff the two sets for one handle
                    // if more than one handle is different, throw an exception?  Not sure which to focus window on
                    // add the handle to focus stack
               // if (currentHandles < baseSet)
                    // diff the two sets for one handle
                    // if more than one handle is different, throw an exception?  Not sure which to focus window on
            // Finally, set the basehandles
         }

        private static void GetLatestWindowHandles()
        {
            // clear, and replace the base handle set with the current handles
            BaseHandleSet.Clear();
			var currentHandles = Web.Driver.WindowHandles;
            foreach (string handle in currentHandles)
            {
                BaseHandleSet.Add(handle);
            }
        }      
    }
}
