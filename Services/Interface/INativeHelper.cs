using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface INativeHelper
    {
        /// <summary>
        /// On iOS, gets the <c>CFBundleVersion</c> number and on Android, gets the <c>PackageInfo</c>'s <c>VersionName</c>, both of which are specified in their respective project properties.
        /// </summary>
        /// <returns><c>string</c>, containing the build number.</returns>
        string GetAppVersion();

        /// <summary>
        /// On iOS, gets the <c>UIDevice.CurrentDevice.SystemVersion</c> number and on Android, gets the <c>Build.VERSION.Release</c>.
        /// </summary>
        /// <returns><c>string</c>, containing the OS version number.</returns>
        string GetOsVersion();
    }
}
