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
    [QueryProperty(nameof(IdRefAnot), nameof(IdRefAnot))]
    class EliminarReferenciaViewModel : ViewModelBase
    {
        int idRefAnot { get; set; }
        public int IdRefAnot
        {
            get => idRefAnot; set
            {
                idRefAnot = value; OnPropertyChanged();
            }
        }

        public AsyncCommand EliminarAnotacionCommand { get; }
        public AsyncCommand BackCommand { get; }

        public EliminarReferenciaViewModel()
        {
            EliminarAnotacionCommand = new AsyncCommand(Eliminar);
            BackCommand = new AsyncCommand(GoToBack);
        }

        async Task Eliminar()
        {
            LOD_ReferenciasAnot anotacion = await ReferenciasAnotDBServices.FindReferenciaAnot(IdRefAnot);
            int IdAnotacion = anotacion.IdAnotacion;

                try
                {
                bool lod = false;
                ConnectivityHelper helperConnection = new ConnectivityHelper();
                    bool connection = helperConnection.VerifyConnection();
                    if (connection)
                    {
                        lod = await InternetAnotacionServices.RemoveReferencia(anotacion.IdRefAnot);
                        await ReferenciasAnotDBServices.RemoveReferenciaAnot(anotacion.IdRefAnot);
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
                LOD_ReferenciasAnot anotacion = await ReferenciasAnotDBServices.FindReferenciaAnot(IdRefAnot);
                int IdAnotacion = anotacion.IdAnotacion;
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
