using Bonbonniere.AcceptanceTests.Tools.PageAccess;
using TechTalk.SpecFlow;

namespace Bonbonniere.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class AllTestsEventDefinition
    {
        [AfterScenario]//[After]
        public void AfterScenario()
        {
            WebDriverHelper.QuitDriver();
        }

        //[BeforeTestRun]
        //public static void BeforeTestRun()
        //{
        //    /**
        //     *  Automation logic that has to run before/after the entire test run
        //        Note: As most of the unit test runners do not provide a hook for executing logic once the tests have been executed, the [AfterTestRun] event is triggered by the test assembly unload event. The exact timing and thread of this execution may therefore differ for each test runner.
        //        The method it is applied to must be static.
        //     * **/
        //}
        //[AfterTestRun]
        //public static void AfterTestRun()
        //{

        //}

        //[BeforeFeature]
        //public static void BeforeFeature()
        //{
        //    /**
        //     *  Automation logic that has to run before/after executing each feature
        //        The method it is applied to must be static.
        //     * **/
        //}
        //[AfterFeature]
        //public static void AfterFeature()
        //{

        //}

        //[BeforeScenario]//[Before]
        //public void BeforeScenario()
        //{
        //    /**
        //     *  Automation logic that has to run before/after executing each scenario or scenario outline example
        //        Short attribute names are available from v1.8.
        //     * **/
        //    if (ScenarioContext.Current.ScenarioInfo.Tags.Contains("web"))
        //    {
        //        var browserDriverFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BrowserDrivers");
        //        _driver = new InternetExplorerDriver(browserDriverFolder);
        //    }
        //}
        //[AfterScenario]//[After]
        //public void AfterScenario()
        //{
        //    if (ScenarioContext.Current.ScenarioInfo.Tags.Contains("web"))
        //    {
        //        if (_driver != null)
        //        {
        //            _driver.Quit();
        //        }
        //    }
        //}

        //[BeforeScenarioBlock]
        //public void BeforeScenarioBlock()
        //{
        //    /**
        //     *  Automation logic that has to run before/after executing each scenario block (e.g. between the "givens" and the "whens")
        //     * **/
        //}
        //[AfterScenarioBlock]
        //public void AfterScenarioBlock()
        //{

        //}

        //[BeforeStep]
        //public void BeforeStep()
        //{
        //    /**
        //     *  Automation logic that has to run before/after executing each scenario step
        //     * **/
        //}
        //[AfterStep]
        //public void AfterStep()
        //{

        //}
    }
}
