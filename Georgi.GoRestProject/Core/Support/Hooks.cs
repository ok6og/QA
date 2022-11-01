using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Georgi.GoRestProject.Core.Config;
using Georgi.GoRestProject.Core.ContextContainers;

namespace Georgi.GoRestProject.Core.Support
{
    [Binding]
    public class Hooks
    {
        private TestContextContainer _testContext;
        private BaseConfig _baseConfig;
        public Hooks(TestContextContainer testContext, BaseConfig baseConfig)
        {
            _testContext = testContext;
            _baseConfig = baseConfig;
        }

        [BeforeScenario]
        public void HttpInitializer()
        {
            _testContext.HttpClient = new HttpClient();
        }

        [BeforeScenario("Authenticate")]
        public void Authenticate()
        {
            _testContext.HttpClient.DefaultRequestHeaders.Add("Authorization", _baseConfig.HttpClientConfig.Token);
        }
        [AfterScenario]
        public void TearDown()
        {
            _testContext.HttpClient.DeleteAsync($"{_baseConfig.HttpClientConfig.BaseUrl}{_baseConfig.HttpClientConfig.UsersUrl}{_testContext.IdToDelete}");
        }
    }
}
