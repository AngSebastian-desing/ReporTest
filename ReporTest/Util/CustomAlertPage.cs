using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ReporTest.Util
{
    public class CustomAlertPage : ContentPage
    {
        public CustomAlertPage(string message)
        {
            var activityIndicator = new ActivityIndicator
            {
                IsRunning = true,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            var label = new Label
            {
                Text = message,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            var stackLayout = new StackLayout
            {
                Children = { activityIndicator, label },
                BackgroundColor = Color.White,
                Padding = new Thickness(20),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Spacing = 10
            };

            Content = stackLayout;
        }
    }
}
