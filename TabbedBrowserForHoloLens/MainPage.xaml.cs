using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace TabbedBrowserForHoloLens
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly string defaultPageUri = "http://google.com";

        public MainPage()
        {
            this.InitializeComponent();
        }

        public void OpenNewPage(string uriString)
        {
            Uri uri;
            try
            {
                uri = new Uri(uriString);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return;
            }

            var webView = new WebView
            {
                Source = uri
            };
            webView.NavigationCompleted += WebView_NavigationCompleted;

            var newTab = new TabViewItem
            {
                Content = webView,
                Header = uriString
            };

            tabView.TabItems.Add(newTab);
            tabView.SelectedItem = newTab;
        }

        private void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            var tabViewItem = sender.Parent as TabViewItem;
            if (tabViewItem == null)
            {
                return;
            }

            var title = sender.DocumentTitle;
            tabViewItem.Header = title;
        }

        private void AddTabButtonClick(TabView sender, object e)
        {
            OpenNewPage(defaultPageUri);
        }

        private void TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
        }
    }
}
