using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using NugetAuditor.Data;
using NugetAuditor.Processors;

namespace NugetAuditor
{
    class MainClass
	{

		public static void Main(string[] args)
		{
            NugetAuditProcessor.ProcessAsync().Wait();
		}
	}


}
