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
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LODapp.ViewModels
{
//    [QueryProperty(nameof(Num), nameof(Num))]
    class LoginViewModel : ViewModelBase
    {
        public ICommand TapCommand => new AsyncCommand<string>((url) => Launcher.OpenAsync(url));
        bool rememberData { get; set; }
        public bool RememberData
        {
            get => rememberData;
            set
            {
                rememberData = value;
                OnPropertyChanged();
            }
        }

        string fileName { get; set; }
        public string FileName { get => fileName; set { fileName = value; OnPropertyChanged(); } }

        public string rut { get; set; }
        public string Rut {
            get => rut;
            set
            {
                rut = value;
                OnPropertyChanged();
            }
        }

        //public string num { get; set; }
        //public string Num
        //{
        //    get => num;
        //    set
        //    {
        //        num = value;
        //        if (String.IsNullOrEmpty(value))
        //        {
        //            VerifyRememberDataCommand.ExecuteAsync();
        //        }
        //        OnPropertyChanged();
        //    }
        //}


        public string password { get; set; }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public AsyncCommand CallLoginCommand { get; set; }
        public AsyncCommand VerifyRememberDataCommand { get; set; }
        public AsyncCommand<string> RecoverPassword { get; set; }

        public LoginViewModel(string Num)
        {
            RecoverPassword = new AsyncCommand<string>(GoToRecoverPassword);
            VerifyRememberDataCommand = new AsyncCommand(VerifyRememberData);
            CallLoginCommand = new AsyncCommand(CallLogin);
            if (String.IsNullOrEmpty(Num))
            {
                VerifyRememberDataCommand.ExecuteAsync();
            }
        }

        async Task VerifyRememberData()
        {
            var user = await ApplicationUserDBServices.FindUser(1);
            if(user != null)
            {
                if(user.RememberData)
                {
                    var route = $"{nameof(LoadDataPage)}";
                    await Shell.Current.GoToAsync(route);
                }
            }
        }

        async Task CallLogin()
        {
            ConnectivityHelper helperConnection = new ConnectivityHelper();
            bool connection = helperConnection.VerifyConnection();
            if (connection)
            {
                var user = await InternetLoginServices.Login(Rut, Password);
                if (user != null)
                {
                    if (user.Nombres.Equals("Error"))
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Usuario Inválido", "OK");
                    }
                    else
                    {
                        if (await ApplicationUserDBServices.FindUserString(user.UserId) == null)
                        {
                            ApplicationUser usuario = new ApplicationUser()
                            {
                                UserId = user.UserId,
                                Activo = user.Activo,
                                AnexoEmpresa = user.AnexoEmpresa,
                                Apellidos = user.Apellidos,
                                CargoContacto = user.CargoContacto,
                                DataLetters = user.DataLetters,
                                IdCertificado = user.IdCertificado,
                                Nombres = user.Nombres,
                                IdSucursal = user.IdSucursal,
                                Movil = user.Movil,
                                Run = user.Run,
                                RutaImagen = user.RutaImagen,
                                Telefono = user.Telefono,
                                RememberData = RememberData,
                                Email = user.Email,
                                Password = password

                            };

                            int id = await ApplicationUserDBServices.AddUser(usuario);
                        }
                        else
                        {
                            var userSave = await ApplicationUserDBServices.FindUserString(user.UserId);
                            userSave.Nombres = user.Nombres;
                            userSave.Apellidos = user.Apellidos;
                            userSave.Telefono = user.Telefono;
                            userSave.IdSucursal = user.IdSucursal;
                            userSave.RememberData = RememberData;
                            userSave.Password = password;
                            await ApplicationUserDBServices.UpdateUser(userSave);
                        }

                        var route = $"{nameof(LoadDataPage)}";
                        await Shell.Current.GoToAsync(route);
                    }


                }else if (await ApplicationUserDBServices.FindUser(1) != null)
                {
                    var route = $"{nameof(LoadDataPage)}";
                    await Shell.Current.GoToAsync(route);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Usuario Inválido o No hay Conexión con la red, Contacte a Soporte", "OK");
                }
            }
            else if (await ApplicationUserDBServices.FindUser(1) != null)
            {
                var route = $"{nameof(LoadDataPage)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Verifique su conexión", "OK");
            }

            
        }

        async Task GoToRecoverPassword(string ruta)
        {
            try
            {
                string uri = ruta;
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
