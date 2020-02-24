using System;
using System.Collections.Generic;
using System.Text;

namespace SampleWebJob.DI.WebJobConfiguration
{
    public class WebJobConfiguration : IWebJobConfiguration
    {
        public string Message { get; set; }
    }
}
