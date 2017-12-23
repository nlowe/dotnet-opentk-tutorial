using System;
using dotnet_opentk_tutorial.Components;

namespace dotnet_opentk_tutorial
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            new MainWindow().Run(60); // Unlocked FPS, 60 ticks per second
        }
    }
}