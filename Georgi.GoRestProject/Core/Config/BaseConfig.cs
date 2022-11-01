using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Georgi.GoRestProject.Core.Config
{
    public class BaseConfig
    {
        public BaseConfig()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("testConfig.json")
                .Build();
            HttpClientConfig = config.GetSection("HttpClient").Get<HttpClientConfig>();
        }
        public HttpClientConfig HttpClientConfig { get; set; }
    }
}
