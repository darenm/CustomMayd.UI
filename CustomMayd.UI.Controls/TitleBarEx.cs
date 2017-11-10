using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CustomMayd.UI.Controls
{
    public sealed class TitleBarEx : Control
    {
        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register(
            "HeaderText", typeof(string), typeof(TitleBarEx), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty ExtendIntoTitleBarProperty = DependencyProperty.Register(
            "ExtendIntoTitleBar", typeof(bool), typeof(TitleBarEx),
            new PropertyMetadata(default(bool), (o, args) =>
            {
                if ((bool) args.NewValue)
                {
                    CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
                    var colorTitleBar = ApplicationView.GetForCurrentView().TitleBar;
                    colorTitleBar.BackgroundColor = Colors.Transparent;
                }
                else
                {
                    CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = false;
                    var colorTitleBar = ApplicationView.GetForCurrentView().TitleBar;
                    colorTitleBar.BackgroundColor = Colors.White;
                }
            }));

        public static readonly DependencyProperty TitleBarButtonForegroundColorProperty = DependencyProperty.Register(
            "TitleBarButtonForegroundColor", typeof(Color), typeof(TitleBarEx),
            new PropertyMetadata(default(Color), (o, args) =>
            {
                var colorTitleBar = ApplicationView.GetForCurrentView().TitleBar;
                colorTitleBar.ForegroundColor = (Color) args.NewValue;
                colorTitleBar.ButtonForegroundColor = (Color) args.NewValue;
                colorTitleBar.ButtonInactiveForegroundColor = (Color) args.NewValue;
            }));

        public static readonly DependencyProperty TitleBarButtonBackgroundColorProperty = DependencyProperty.Register(
            "TitleBarButtonBackgroundColor", typeof(Color), typeof(TitleBarEx), new PropertyMetadata(default(Color),
                (o, args) =>
                {
                    var colorTitleBar = ApplicationView.GetForCurrentView().TitleBar;
                    colorTitleBar.ButtonBackgroundColor = (Color) args.NewValue;
                    colorTitleBar.ButtonInactiveBackgroundColor = (Color) args.NewValue;
                }));

        private TextBlock _appTitle;

        public TitleBarEx()
        {
            DefaultStyleKey = typeof(TitleBarEx);
        }

        public Color TitleBarButtonBackgroundColor
        {
            get => (Color) GetValue(TitleBarButtonBackgroundColorProperty);
            set => SetValue(TitleBarButtonBackgroundColorProperty, value);
        }

        public Color TitleBarButtonForegroundColor
        {
            get => (Color) GetValue(TitleBarButtonForegroundColorProperty);
            set => SetValue(TitleBarButtonForegroundColorProperty, value);
        }

        public bool ExtendIntoTitleBar
        {
            get => (bool) GetValue(ExtendIntoTitleBarProperty);
            set => SetValue(ExtendIntoTitleBarProperty, value);
        }

        public string HeaderText
        {
            get => (string) GetValue(HeaderTextProperty);
            set => SetValue(HeaderTextProperty, value);
        }

        protected override void OnApplyTemplate()
        {
            _appTitle = (TextBlock) GetTemplateChild("AppTitle");
            //HeaderText = Package.Current.DisplayName;

            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = ExtendIntoTitleBar;

            if (ExtendIntoTitleBar)
            {
                var colorTitleBar = ApplicationView.GetForCurrentView().TitleBar;
                colorTitleBar.BackgroundColor = Colors.Transparent;
                colorTitleBar.ButtonBackgroundColor = Colors.Transparent;
                colorTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            }
            base.OnApplyTemplate();
        }
    }
}