using System;
using Xamarin.Forms;

namespace Quitaye.Helpers
{
    public class BoolResources
    {
        public static readonly bool ShouldShowBoxView = Device.OnPlatform(true, false, true);
    }
}
