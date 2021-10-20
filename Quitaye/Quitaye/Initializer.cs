using Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(InitialService))]

namespace Quitaye
{
    public class Initializer 
    {
        public IInitialService Init { get; }
        public Initializer()
        {
            Init = DependencyService.Get<IInitialService>();
        }

    }
}
