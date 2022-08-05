using LODapp.Data;
using LODapp.Helpers;
using LODapp.Models;
using LODapp.Services;
using LODapp.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LODapp.ViewModels
{
    [QueryProperty(nameof(IdAnotacion), nameof(IdAnotacion))]
    class LoadAnotacionDataViewModel : ViewModelBase
    {
        int idAnotacion { get; set; }
        public int IdAnotacion { get => idAnotacion; set {
                idAnotacion = value; 
                OnPropertyChanged();
                GetDataCommand.ExecuteAsync();
            } }

        public AsyncCommand GetDataCommand { get; }

        public LoadAnotacionDataViewModel()
        {
            GetDataCommand = new AsyncCommand(GetDataReferenciasInet);
            
        }

        public async Task GetDataReferenciasInet()
        {
            IsBusy = true;
            bool continueCon = true;
            ConnectivityHelper helperConnection = new ConnectivityHelper();
            bool connection = helperConnection.VerifyConnection();
            if (connection)
            {
                LOD_Anotaciones anotacion = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
                if (anotacion != null)
                {
                    var user = await ApplicationUserDBServices.FindUser(1);

                    var listReferenciasInet = await InternetAnotacionServices.GetReferencias(anotacion.IdAnotacion.ToString());
                    if (listReferenciasInet != null)
                    {
                        foreach (var item in listReferenciasInet)
                        {
                            LOD_ReferenciasAnot libroAux = await ReferenciasAnotDBServices.FindReferenciaAnot(item.IdRefAnot);
                            if (libroAux == null)
                            {
                                await ReferenciasAnotDBServices.AddReferenciaAnot(item);
                            }

                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un problema intentando conectar con el Servicio, continuaremos sin actualizar", "OK");
                        IsBusy = false;
                        await Task.Delay(1000);
                        if (IsBusy == false)
                        {
                            var route = $"{nameof(AnotacionDataPage)}?IdAnotacion={IdAnotacion}";
                            await Shell.Current.GoToAsync(route);
                        }
                    }
                }

                if (continueCon)
                {
                    var listUsuariosInet = await InternetAnotacionServices.GetReceptores(IdAnotacion.ToString());
                    if (listUsuariosInet != null)
                    {
                        foreach (var item in listUsuariosInet)
                        {
                            var user = await UserAnotacionDBServices.FindUserAnotacion(item.IdUsLod, item.IdAnotacion);
                            if (user == null)
                            {
                                await UserAnotacionDBServices.AddUserAnotacion(item);
                            }
                        }
                    }

                    var listDocumentosInet = await InternetAnotacionServices.GetDocumentos(IdAnotacion.ToString());
                    if (listDocumentosInet != null)
                    {
                        foreach (var item in listDocumentosInet)
                        {
                            var user = await DocAnotacionDBServices.FindDocAnotacion(item.IdDocAnotacion);
                            if (user == null)
                                await DocAnotacionDBServices.AddDocAnotacion(item);
                        }
                    }


                    var listLogsInet = await InternetAnotacionServices.GetLogs(IdAnotacion.ToString());
                    if (listLogsInet != null)
                    {
                        foreach (var item in listLogsInet)
                        {
                            var user = await LogDBServices.FindLog(item.IdLog);
                            if (user == null)
                                await LogDBServices.AddLog(item);
                        }
                    }

                    IsBusy = false;
                    await Task.Delay(1000); await Task.Delay(1000);
                    if (IsBusy == false)
                    {
                        var route = $"{nameof(AnotacionDataPage)}?IdAnotacion={IdAnotacion}";
                        await Shell.Current.GoToAsync(route);
                    }
                }

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo actualizar , contacte a Soporte", "OK");

                IsBusy = false;
                await Task.Delay(1000);
                if (IsBusy == false)
                {
                    var route = $"{nameof(AnotacionDataPage)}?IdAnotacion={IdAnotacion}";
                    await Shell.Current.GoToAsync(route);
                }
            }

            
        }

        
    }
}
