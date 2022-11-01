using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Georgi.GoRestProject.Core.Support;

namespace Georgi.GoRestProject.Core.ContextContainers
{
    public class TestContextContainer
    {
        public HttpClient HttpClient { get; set; }
        public int IdToDelete { get; set; }
        public GoRestRequestUser GoRestUser { get; set; }
    }
}
