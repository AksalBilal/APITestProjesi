using System;
using System.Configuration;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TechTestAPI.Helpers;
using static System.IO.Path;

namespace TechTestAPI.Base
{
    [AllureNUnit]
    [TestFixture]
    [Category("TechTestAPI")]
    [Author("Tester > Bilal Aksal", "bilalaksal@gmail.com")]
    [AllureDisplayIgnored]
    public abstract class BaseClass
    {
        protected string AllureCleanUpType;
        private readonly Logger _apiTestLog = Logger.GetInstance();

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var allureLogsPath = ConfigurationManager.AppSettings["AllureLogsPath"];//allure loglarının eklenecegi path i app.config bölümünden alır.
            Environment.CurrentDirectory = GetDirectoryName(allureLogsPath) ?? throw new InvalidOperationException();// allure path ini environment path e ekler
            
            AllureCleanUpType =
                "false"; // allureCleanUpType : false (false > gelirse loglari silmeden ustune yazmaya devam eder!)

            if (TestContext.Parameters["allureCleanUpType"] != null)
                AllureCleanUpType = TestContext.Parameters["allureCleanUpType"];

            if (AllureCleanUpType.IndexOf("true", StringComparison.OrdinalIgnoreCase) != -1)
                AllureLifecycle.Instance.CleanupResultDirectory(); // NUnitConsole3 Params * 
        }
        
        public void AfterMethod(TestContext currentResult)
        {
            //her test case tamamlandıktan sonra buraya düşer burda da eger case fail verdiyse loglama işlemi yapılır.
            var status = currentResult.Result.Outcome.Status; //passed -failed
            switch (status)
            {
                case TestStatus.Inconclusive:

                    break;
                case TestStatus.Skipped:

                    break;
                case TestStatus.Passed:

                    break;
                case TestStatus.Warning:
                    break;
                case TestStatus.Failed:
                    _apiTestLog.ExceptionLog(currentResult);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}