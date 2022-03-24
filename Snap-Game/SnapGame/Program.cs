using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnapGame
{
    /// <summary>
    /// The code struture explained below
    ///---- Model  project is where i defined all the entities 
    ///---- Business Logic project logic is where I defined my contracts(interfaces) and services(Implementations)
    ///---- SnapGame Project is the main project. i.e Where Business logic was referenced
    ///---- SnapGameTest project is the test project where the project requirements was tested
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            AsyncContext.Run(() => MainProgram.MainAsync(args));
        }
    }
}