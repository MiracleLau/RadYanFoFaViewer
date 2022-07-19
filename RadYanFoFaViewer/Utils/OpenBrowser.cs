using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RadYanFoFaViewer.Utils;

public static class OpenBrowser
{
    public static void Open(string url)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            Process.Start("xdg-open", url);
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            Process.Start("open", url);
        else
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
    }
}