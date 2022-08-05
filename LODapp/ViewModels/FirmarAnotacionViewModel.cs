using LODapp.Data;
using LODapp.Helpers;
using LODapp.Models;
using LODapp.Services;
using LODapp.Views;
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
    class FirmarAnotacionViewModel : ViewModelBase
    {
        string password { get; set; }
        public string Password { get => password; set { password = value; OnPropertyChanged(); } }

        int idAnotacion { get; set; }
        public int IdAnotacion { get => idAnotacion; set { idAnotacion = value; OnPropertyChanged(); } }

        public AsyncCommand FirmarCommand { get; }
        public AsyncCommand BackCommand { get; }

        public FirmarAnotacionViewModel()
        {
            BackCommand = new AsyncCommand(GoToBack);
            FirmarCommand = new AsyncCommand(Firmar);
        }

        async Task Firmar()
        {
            IsBusy = true;
            ApplicationUser user = await ApplicationUserDBServices.FindUser(1);
            LOD_Anotaciones anot = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
            if (user.Password.Equals(password))
            {
                try
                {
                    FirmarAnotacion firmarAnotacion = new FirmarAnotacion();
                    firmarAnotacion.IdAnotacion = IdAnotacion;
                    firmarAnotacion.password = Password;
                    firmarAnotacion.UserId = user.UserId;
                    LOD_Anotaciones anotacion = new LOD_Anotaciones();

                    bool permiteFirma = true;

                    List<LOD_UserAnotacion> receptores = await UserAnotacionDBServices.FindUserAnotacionByAnot(IdAnotacion);
                    if(receptores != null)
                    {
                        if (receptores.Count > 0)
                        {
                            var controles = await CodSubComDBServices.FindCodSubComBySubtipo(anot.IdTipoSub);
                            if (controles.Count > 0)
                            {
                                List<int> listadoIdDocReq = new List<int>();
                                foreach (var item in controles)
                                {
                                    listadoIdDocReq.Add(item.IdTipo);
                                }

                                List<MAE_TipoDocumento> listTipoDocRequeridos = new List<MAE_TipoDocumento>();
                                foreach (var item in listadoIdDocReq)
                                {
                                    listTipoDocRequeridos.Add(await TipoDocumentoDBServices.FindTipoDoc(item));
                                }

                                var listDocumentos = await DocAnotacionDBServices.FindDocAnotacionByAnotacion(IdAnotacion);
                                if (listDocumentos != null && listDocumentos.Count > 0)
                                {
                                    List<int> IdCargados = listDocumentos.Select(x => x.IdTipoDoc).ToList();
                                    foreach (var item in listTipoDocRequeridos)
                                    {
                                        if (!IdCargados.Contains(item.IdTipo))
                                        {
                                            permiteFirma = false;
                                        }
                                    }
                                }
                                else
                                {
                                    permiteFirma = false;
                                }
                            }

                        }
                    }
                    

                    if (permiteFirma)
                    {
                        ConnectivityHelper helperConnection = new ConnectivityHelper();
                        bool connection = helperConnection.VerifyConnection();
                        if (connection)
                        {
                            anotacion = await InternetAnotacionServices.FirmarAnotacion(firmarAnotacion);
                        }
                        else
                        {
                            anotacion = null;
                        }

                        if (anotacion != null)
                        {
                            anotacion.Actualizado = 3;
                            await AnotacionesDBServices.UpdateAnotacion(anotacion);
                        }
                        else
                        {
                            anotacion = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
                            anotacion.Estado = 4;
                            anotacion.EstadoFirma = true;
                            anotacion.FechaFirma = DateTime.Now;
                            anotacion.FechaPub = DateTime.Now;
                            anotacion.UserId = user.UserId;
                            anotacion.Actualizado = 4;

                            await AnotacionesDBServices.UpdateAnotacion(anotacion);
                        }

                        var listDocumentos = await DocAnotacionDBServices.FindDocAnotacionByAnotacion(IdAnotacion);

                        foreach (var item in listDocumentos)
                        {
                            item.EstadoDoc = 2;
                            item.Actualizado = 4;
                            item.FechaEvento = DateTime.Now;
                            await DocAnotacionDBServices.UpdateDocAnotacion(item);
                        }

                        IsBusy = false;
                        var route = $"{nameof(LoadAnotacionDataPage)}?IdAnotacion={anotacion.IdAnotacion}";
                        await Shell.Current.GoToAsync(route);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Denegado", "Existen documentos pendientes por cargar", "OK");
                    }

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo Firmar la Anotación, Contacte a Soporte", "OK");
                }

            }
            else
            {
                
                await Application.Current.MainPage.DisplayAlert("Denegado", "Verifique sus credenciales", "OK");
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
