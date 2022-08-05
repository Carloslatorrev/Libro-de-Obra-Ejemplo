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
    [QueryProperty(nameof(IdDocAnotacion), nameof(IdDocAnotacion))]
    class EliminarDocViewModel : ViewModelBase
    {
        int idDocAnotacion { get; set; }
        public int IdDocAnotacion
        {
            get => idDocAnotacion; set
            {
                idDocAnotacion = value; OnPropertyChanged();
            }
        }
        public AsyncCommand EliminarAnotacionCommand { get; }
        public AsyncCommand BackCommand { get; }

        public EliminarDocViewModel()
        {
            EliminarAnotacionCommand = new AsyncCommand(Eliminar);
            BackCommand = new AsyncCommand(GoToBack);
        }

        async Task Eliminar()
        {
            LOD_docAnotacion docAnot = await DocAnotacionDBServices.FindDocAnotacion(IdDocAnotacion);
            int IdAnotacion = docAnot.IdAnotacion;
            try
            {
                bool lod = false;
                ConnectivityHelper helperConnection = new ConnectivityHelper();
                bool connection = helperConnection.VerifyConnection();
                if (connection)
                {
                    lod = await InternetAnotacionServices.RemoveDoc(docAnot.IdDocAnotacion);
                    await DocAnotacionDBServices.RemoveDocAnotacion(docAnot.IdDocAnotacion);
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
                //await DocAnotacionDBServices.RemoveDocAnotacion(docAnot.IdDocAnotacion);
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", "No se ha podido Eliminar el documento, contacte a Soporte", "OK");
            }

        }

        async Task GoToBack()
        {
            try
            {
                LOD_docAnotacion docAnot = await DocAnotacionDBServices.FindDocAnotacion(IdDocAnotacion);
                int IdAnotacion = docAnot.IdAnotacion;
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
