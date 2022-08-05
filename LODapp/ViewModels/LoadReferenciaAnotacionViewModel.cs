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
    class LoadReferenciaAnotacionViewModel : ViewModelBase
    {
        int idLibro { get; set; }
        public int IdLibro { get => idLibro; set { idLibro = value; OnPropertyChanged();
            } }

        int idAnotacion { get; set; }
        public int IdAnotacion { get => idAnotacion; set { idAnotacion = value; OnPropertyChanged();
                GetDataCommand.ExecuteAsync();
            } }

        CON_Contratos contrato;

        public CON_Contratos Contrato
        {
            get => contrato;
            set
            {
                contrato = value;
                OnPropertyChanged();
            }
        }

        public AsyncCommand GetDataCommand { get; }

        public LoadReferenciaAnotacionViewModel()
        {
            //idAnotacion = Convert.ToInt32(IdAnot);
            GetDataCommand = new AsyncCommand(GetAnotaciones);
        }


        public async Task GetAnotaciones()
        {
            IsBusy = true;
            LOD_Anotaciones anotDB = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
            LOD_LibroObras libro = await LibroObrasDBServices.FindLibro(anotDB.IdLod);
            contrato = await ContratosDBServices.FindContrato(libro.IdContrato);
            ConnectivityHelper helperConnection = new ConnectivityHelper();
            bool connection = helperConnection.VerifyConnection();
            if (connection)
            {
                var librosInet = await InternetLibroObrasServices.GetSelectLibroObras(Contrato.IdContrato);
                if(librosInet != null)
                {
                    foreach (var item in librosInet)
                    {

                        if (await LibroObrasDBServices.FindLibro(item.IdLod) == null)
                        {
                            await LibroObrasDBServices.AddLibroObras(item);
                        }

                    }

                    List<LOD_LibroObras> libros = await LibroObrasDBServices.FindByContratoLibro(Contrato.IdContrato);
                    foreach (var item in libros)
                    {
                        List<LOD_Anotaciones> anotInet = await InternetAnotacionServices.GetAnotacionByLod(item.IdLod);
                        foreach (var anot in anotInet)
                        {
                            if (await AnotacionesDBServices.FindAnotacion(anot.IdAnotacion) == null)
                            {
                                await AnotacionesDBServices.AddAnotacion(anot);
                            }
                        }
                    }

                    IsBusy = false;
                    if (IsBusy == false)
                    {
                        var route = $"{nameof(AddReferenciaPage)}?IdAnotacion={anotDB.IdAnotacion}";
                        await Shell.Current.GoToAsync(route);
                    }

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo actualizar , contacte a Soporte", "OK");
                    IsBusy = false;
                    await Task.Delay(1000);
                    if (IsBusy == false)
                    {
                        var route = $"{nameof(AddReferenciaPage)}?IdAnotacion={anotDB.IdAnotacion}";
                        await Shell.Current.GoToAsync(route);
                    }
                }
                
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Verifique su conexión", "OK");

                IsBusy = false;
                await Task.Delay(1000);
                if (IsBusy == false)
                {
                    var route = $"{nameof(AddReferenciaPage)}?IdAnotacion={anotDB.IdAnotacion}";
                    await Shell.Current.GoToAsync(route);
                }
            }

           
        }
    }
}
