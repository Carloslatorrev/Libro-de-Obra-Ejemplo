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
    [QueryProperty(nameof(IdContrato), nameof(IdContrato))]
    class LoadContratoDataViewModel : ViewModelBase
    {

        int idContrato { get; set; }
        public int IdContrato { get => idContrato; set { idContrato = value; OnPropertyChanged();
                GetLibrosInetCommand.ExecuteAsync();
            } }


        public bool activityState = false;
        public bool ActivityState
        {
            get => activityState;
            set
            {
                activityState = value;
                OnPropertyChanged();
            }
        }

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

        public AsyncCommand GetLibrosInetCommand { get; }



        public LoadContratoDataViewModel()
        {
            GetLibrosInetCommand = new AsyncCommand(GetDataLibrosInet);
        }

        public async Task GetDataLibrosInet()
        {
            IsBusy = true;
            bool continueCon = true;
            ConnectivityHelper helperConnection = new ConnectivityHelper();
            bool connection = helperConnection.VerifyConnection();
            if (connection)
            {
                contrato = await ContratosDBServices.FindContrato(IdContrato);
                if (contrato != null)
                {
                    var user = await ApplicationUserDBServices.FindUser(1);
                    LibroObrasUserView libUser = new LibroObrasUserView()
                    {
                        UserId = user.UserId,
                        IdContrato = contrato.IdContrato

                    };

                    var listLibrosInet = await InternetLibroObrasServices.GetLibroObrasByUser(libUser);
                    if (listLibrosInet != null)
                    {
                        foreach (var item in listLibrosInet)
                        {
                            LOD_LibroObras libroAux = await LibroObrasDBServices.FindLibro(item.IdLod);
                            if (libroAux == null)
                                await LibroObrasDBServices.AddLibroObras(item);

                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un problema intentando conectar con el Servicio, continuaremos sin actualizar", "OK");
                        IsBusy = false;
                        await Task.Delay(1000);
                        if (IsBusy == false)
                        {
                            continueCon = false;
                            var route = $"{nameof(ContratoDataPage)}?IdContrato={IdContrato}";
                            await Shell.Current.GoToAsync(route);
                        }
                    }
                }

                if (continueCon)
                {
                    var listUsuariosInet = await InternetUsuariosLodServices.GetUsuariosLodByContrato(IdContrato.ToString());
                    if (listUsuariosInet != null)
                    {
                        foreach (var item in listUsuariosInet)
                        {
                            var user = await UsuariosLodDBServices.FindUserLod(item.IdUsLod);
                            if (user == null)
                                await UsuariosLodDBServices.AddUserLod(item);
                        }
                    }

                    IsBusy = false;
                    await Task.Delay(1000);
                    if (IsBusy == false)
                    {
                        var route = $"{nameof(ContratoDataPage)}?IdContrato={IdContrato}";
                        await Shell.Current.GoToAsync(route);
                    }
                }
               
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo recuperar la información, Verifique su conexión", "OK");
                IsBusy = false;
                await Task.Delay(1000);
                if (IsBusy == false)
                {
                    var route = $"{nameof(ContratoDataPage)}?IdContrato={IdContrato}";
                    await Shell.Current.GoToAsync(route);
                }
            }

            
        }



    }
}
