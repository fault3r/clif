using System;
using Clif.Application;

namespace Clif
{
    class Program
    {
        public static void Main(string[] args)
        {
            var app = new ClifCli();
            app.Run(args);
        }
    }
}