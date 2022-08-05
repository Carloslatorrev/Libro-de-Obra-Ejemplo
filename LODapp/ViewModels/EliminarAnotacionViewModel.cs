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
    [QueryProperty(nameof(IdAnotacion), nameof(IdAnotacion))]
    class EliminarAnotacionViewModel : ViewModelBase
    {
        int idAnotacion { get; set; }
        public int IdAnotacion { get => idAnotacion; set { idAnotacion = value; OnPropertyChanged(); 
           } }

        public AsyncCommand EliminarAnotacionCommand { get; }
        public AsyncCommand BackCommand { get; }

        public EliminarAnotacionViewModel()
        {
            EliminarAnotacionCommand = new AsyncCommand(Eliminar);
            BackCommand = new AsyncCommand(GoToBack);
        }

        async Task Eliminar()
        {
           LOD_Anotaciones anotacion = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
            int IdLibro = anotacion.IdLod;
           if(anotacion.EstadoFirma == false)
           {
                try
                {
                    LOD_LibroObras lod = new LOD_LibroObras();
                    ConnectivityHelper helperConnection = new ConnectivityHelper();
                    bool connection = helperConnection.VerifyConnection();
                    if (connection)
                    {
                        lod = await InternetAnotacionServices.DeleteAnotacion(IdAnotacion);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Denegado", "No se ha podido Eliminar la Anotación, se requiere conexión, intentelo más tarde", "OK");
                        string idL = IdLibro.ToString();
                        var route2 = $"{nameof(LoadLibroDataPage)}?IdLibro={idL}";
                        await Shell.Current.GoToAsync(route2);
                    }


                    await AnotacionesDBServices.RemoveAnotacion(anotacion.IdAnotacion);
                    string id = IdLibro.ToString();
                    var route = $"{nameof(LoadLibroDataPage)}?ObjectID={id}";
                    await Shell.Current.GoToAsync(route);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Application.Current.MainPage.DisplayAlert("Error", "No se ha podido Eliminar la Anotación, contacte a Soporte", "OK");
                }
               
            }     


        }

        async Task GoToBack()
        {
            try
            {
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
