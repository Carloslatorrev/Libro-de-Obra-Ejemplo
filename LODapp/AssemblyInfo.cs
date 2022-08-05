using Android.App;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessNetworkState)]
[assembly: ExportFont("fa-solid-900.ttf", Alias ="FAS")]
[assembly: ExportFont("fa-regular-400.ttf", Alias = "FAR")]
[assembly: ExportFont("fa-brands-400.ttf", Alias = "FAB")]