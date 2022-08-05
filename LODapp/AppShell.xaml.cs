using LODapp.Views;
using System;
using Xamarin.Forms;

namespace LODapp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ListContratosPage), typeof(ListContratosPage));
            Routing.RegisterRoute(nameof(ContratoDataPage), typeof(ContratoDataPage));
            Routing.RegisterRoute(nameof(LibroDataPage), typeof(LibroDataPage));
            Routing.RegisterRoute(nameof(AnotacionDataPage), typeof(AnotacionDataPage));
            Routing.RegisterRoute(nameof(LoadDataPage), typeof(LoadDataPage));
            Routing.RegisterRoute(nameof(LoadContratoDataPage), typeof(LoadContratoDataPage));
            Routing.RegisterRoute(nameof(LoadLibroDataPage), typeof(LoadLibroDataPage));
            Routing.RegisterRoute(nameof(LoadAnotacionDataPage), typeof(LoadAnotacionDataPage));
            Routing.RegisterRoute(nameof(AddAnotacionPage), typeof(AddAnotacionPage));
            Routing.RegisterRoute(nameof(AddReceptorPage), typeof(AddReceptorPage));
            Routing.RegisterRoute(nameof(AddReferenciaPage), typeof(AddReferenciaPage));
            Routing.RegisterRoute(nameof(EliminarAnotacionPage), typeof(EliminarAnotacionPage));
            Routing.RegisterRoute(nameof(FirmarAnotacionPage), typeof(FirmarAnotacionPage));
            Routing.RegisterRoute(nameof(SolicitarFirmaAnotacionPage), typeof(SolicitarFirmaAnotacionPage));
            Routing.RegisterRoute(nameof(LoadReferenciaDataPage), typeof(LoadReferenciaDataPage));
            Routing.RegisterRoute(nameof(AsistenciaTelefonica), typeof(AsistenciaTelefonica));
            Routing.RegisterRoute(nameof(EnviarCorreoSoporte), typeof(EnviarCorreoSoporte));
            Routing.RegisterRoute(nameof(PreguntasFrecuentes), typeof(PreguntasFrecuentes));
            Routing.RegisterRoute(nameof(EditarAnotacionPage), typeof(EditarAnotacionPage));
            Routing.RegisterRoute(nameof(EliminarDocPage), typeof(EliminarDocPage));
            Routing.RegisterRoute(nameof(EliminarReceptorPage), typeof(EliminarReceptorPage));
            Routing.RegisterRoute(nameof(EliminarReferenciaPage), typeof(EliminarReferenciaPage));
            Routing.RegisterRoute(nameof(AddDocumentoPage), typeof(AddDocumentoPage));
            Routing.RegisterRoute(nameof(AddOtroDocumentoPage), typeof(AddOtroDocumentoPage));
            Routing.RegisterRoute(nameof(EditReceptorPage), typeof(EditReceptorPage));
            Routing.RegisterRoute(nameof(VBConfirmedPage), typeof(VBConfirmedPage));
            Routing.RegisterRoute(nameof(LogoutData), typeof(LogoutData));
        }

        //private async void OnMenuItemClicked(object sender, EventArgs e)
        //{
        //    await Shell.Current.GoToAsync($"//{nameof(LogoutData)}");
        //}
    }
}
