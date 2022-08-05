using LODapp.Data;
using LODapp.Helpers;
using LODapp.Models;
using LODapp.Services;
using LODapp.Views;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LODapp.ViewModels
{
    [QueryProperty(nameof(IdUserAnot), nameof(IdUserAnot))]
    class EliminarReceptorViewModel : ViewModelBase
    {
        int idUserAnot { get; set; }
        public int IdUserAnot
        {
            get => idUserAnot; set
            {
                idUserAnot = value; OnPropertyChanged();
            }
        }

        public AsyncCommand EliminarAnotacionCommand { get; }
        public AsyncCommand BackCommand { get; }

        public EliminarReceptorViewModel()
        {
            EliminarAnotacionCommand = new AsyncCommand(Eliminar);
            BackCommand = new AsyncCommand(GoToBack);
        }

        async Task Eliminar()
        {
            LOD_UserAnotacion userAnot = await UserAnotacionDBServices.FindUserAnotacion(IdUserAnot);
            int IdAnotacion = userAnot.IdAnotacion;
            int IdUserLod = userAnot.IdUsLod;
            try
            {
                bool lod = false;
                ConnectivityHelper helperConnection = new ConnectivityHelper();
                bool connection = helperConnection.VerifyConnection();
                if (connection)
                {
                    lod = await InternetAnotacionServices.RemoveReceptor(IdAnotacion, IdUserLod);
                    await ReferenciasAnotDBServices.RemoveReferenciaAnot(userAnot.IdUserAnot);
                    var route = $"{nameof(LoadAnotacionDataPage)}?IdAnotacion={IdAnotacion}";
                    await Shell.Current.GoToAsync(route);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Denegado", "No se ha podido Eliminar la referencia, se requiere conexión, intentelo más tarde", "OK");
                    var route2 = $"{nameof(LoadAnotacionDataPage)}?IdAnotacion={IdAnotacion}";
                    await Shell.Current.GoToAsync(route2);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", "No se ha podido Eliminar la referencia, contacte a Soporte", "OK");
            }

        }

        async Task GoToBack()
        {
            try
            {
                LOD_UserAnotacion userAnot = await UserAnotacionDBServices.FindUserAnotacion(IdUserAnot);
                int IdAnotacion = userAnot.IdAnotacion;
                string aux = IdAnotacion.ToString();
                var route = $"{nameof(LoadAnotacionDataPage)}?IdAnotacion={aux}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
