# TabbedBrowserForHoloLens

TabbedBrowserForHoloLens (HoloBrowser) is a tabbed brower for HoloLens to launch from 3D applications.

## Install from Microsoft Store

https://www.microsoft.com/p/holobrowser/9nz4bs6lfhmt

## Build from source
Open `TabbedBrowserForHoloLens.sln`.

Build `TabbedBrowserForHoloLens` solution.

## Launch browser from 3D applications
```cs
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if WINDOWS_UWP
using Windows.System;
#endif

public static class BrowserLauncher
{
    public static void Launch(string url, bool useHoloBrowser = false)
    {
#if WINDOWS_UWP
        UnityEngine.WSA.Application.InvokeOnUIThread(async () =>
        {
            try
            {
                var uri = new Uri(url);
                if (useHoloBrowser)
                {
                    var holoBrowserUri = new Uri($"holo-browser:{url}");
                    var option = new LauncherOptions()
                    {
                        FallbackUri = uri
                    };
                    await Launcher.LaunchUriAsync(holoBrowserUri, option);
                }
                else
                {
                    await Launcher.LaunchUriAsync(uri);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }, false);
#else
        Application.OpenURL(url);
#endif
    }
}
```

## Author
Furuta, Yusuke ([@tarukosu](https://twitter.com/tarukosu))

## License
MIT
