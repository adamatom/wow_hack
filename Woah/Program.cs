using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Woah
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThreadAttribute]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WoahFish());
        }
    }
}