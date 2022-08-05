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
    [QueryProperty(nameof(IdLibro), nameof(IdLibro))]
    class LoadLibroDataViewModel : ViewModelBase
    {
        int idLibro { get; set; }
        public int IdLibro { get => idLibro; set { idLibro = value; OnPropertyChanged();
                GetDataCommand.ExecuteAsync();
            } }

        public AsyncCommand GetDataCommand { get; }

        string rutaImg { get; set; }
        public string RutaImg
        {
            get => rutaImg; set
            {
                rutaImg = value; OnPropertyChanged();
            }
        }


        public LoadLibroDataViewModel()
        {
            GetDataCommand = new AsyncCommand(GetDataAnotacionesInet);
            RutaImg = "/Content/Images/Logo_APR.png";
        }

        public async Task GetDataAnotacionesInet()
        {
            IsBusy = true;
            
            ApplicationUser User = await ApplicationUserDBServices.FindUser(1);
            LOD_LibroObras libro = await LibroObrasDBServices.FindLibro(IdLibro);

            ConnectivityHelper helperConnection = new ConnectivityHelper();
            bool connection = helperConnection.VerifyConnection();
            if (connection)
            {
                if (libro != null)
                {
                    int idLibro = libro.IdLod;

                    AnotacionUserView anotacionUser = new AnotacionUserView()
                    {
                        IdLod = libro.IdLod,
                        UserID = User.UserId
                    };

                    var listAnotacionesInet = await InternetAnotacionServices.GetAnotacionesByUser(anotacionUser);
                    if (listAnotacionesInet != null)
                    {
                        foreach (var item in listAnotacionesInet)
                        {
                            LOD_Anotaciones anotAux = await AnotacionesDBServices.FindAnotacion(item.IdAnotacion);
                            if (anotAux == null)
                                await AnotacionesDBServices.AddAnotacion(item);

                        }
                    }

                }

                if (libro != null)
                {
                    var listUserLodInet = await InternetUsuariosLodServices.GetUsuariosLodByUser(User.UserId);
                    if (listUserLodInet != null)
                    {
                        foreach (var item in listUserLodInet)
                        {
                            LOD_UsuariosLod anotAux = await UsuariosLodDBServices.FindUserLod(item.IdUsLod);
                            if (anotAux == null)
                                await UsuariosLodDBServices.AddUserLod(item);

                        }
                    }

                }

                if (libro != null)
                {
                    var listUserAnotacionInet = await InternetUserAnotacionServices.GetUserAnotacionByUser(User.UserId);
                    if (listUserAnotacionInet != null)
                    {
                        foreach (var item in listUserAnotacionInet)
                        {
                            LOD_UserAnotacion anotAux = await UserAnotacionDBServices.FindUserAnotacionByUserLod(item.IdUsLod);
                            if (anotAux == null)
                            {
                                await UserAnotacionDBServices.AddUserAnotacion(item);
                            }
                        }
                    }

                }

                IsBusy = false;
                await Task.Delay(1000);
                if (IsBusy == false)
                {
                    var route = $"{nameof(LibroDataPage)}?IdLibro={libro.IdLod}";
                    await Shell.Current.GoToAsync(route);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Verifique su conexión", "OK");
                IsBusy = false;
                await Task.Delay(1000);
                if (IsBusy == false)
                {
                    var route = $"{nameof(LibroDataPage)}?IdLibro={libro.IdLod}";
                    await Shell.Current.GoToAsync(route);
                }
            }

            

        }

        
    }
}
