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
    class VBConfirmedViewModel : ViewModelBase
    {
        string password { get; set; }
        public string Password { get => password; set { password = value; OnPropertyChanged(); } }

        int idAnotacion { get; set; }
        public int IdAnotacion { get => idAnotacion; set { idAnotacion = value; OnPropertyChanged(); } }

        public AsyncCommand VBConfirmarCommand { get; }

        public VBConfirmedViewModel()
        {
            VBConfirmarCommand = new AsyncCommand(VBConfirmar);
        }

        async Task VBConfirmar()
        {
            IsBusy = true;
            ApplicationUser user = await ApplicationUserDBServices.FindUser(1);
            if (user.Password.Equals(password))
            {
                LOD_Anotaciones anot = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
                LOD_UsuariosLod userLod = await UsuariosLodDBServices.FindIdByUser(anot.IdLod, user.UserId);
                LOD_UserAnotacion userAnot = new LOD_UserAnotacion();
                VBConfirmedView vbConfirmed = new VBConfirmedView();
                vbConfirmed.IdAnotacion = IdAnotacion;
                vbConfirmed.IdUsLod = userLod.IdUsLod;

                ConnectivityHelper helperConnection = new ConnectivityHelper();
                bool connection = helperConnection.VerifyConnection();
                if (connection)
                {
                    userAnot = await InternetAnotacionServices.VBConfirmed(vbConfirmed);
                }
                else
                {
                    userAnot = null;
                }


                
                if(userAnot != null)
                {
                    await UserAnotacionDBServices.UpdateUserAnotacion(userAnot);
                }
                else
                {
                    userAnot = await UserAnotacionDBServices.FindUserAnotacion(userLod.IdUsLod, anot.IdAnotacion);
                    userAnot.VistoBueno = true;
                    userAnot.FechaVB = DateTime.Now;
                    userAnot.Leido = true;
                    userAnot.Actualizado = 2;
                    await UserAnotacionDBServices.UpdateUserAnotacion(userAnot);
                }

                IsBusy = false;
                var route = $"{nameof(LoadAnotacionDataPage)}?IdAnotacion={anot.IdAnotacion}";
                await Shell.Current.GoToAsync(route);

            }
        }
    }
}
