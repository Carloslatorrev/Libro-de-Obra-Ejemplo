using LODapp.Data;
using LODapp.Views;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LODapp.ViewModels 
{

    public class LogoutDataViewModel : ViewModelBase
    {
        public AsyncCommand LoginCommand { get; set; }


        public LogoutDataViewModel(){
            LoginCommand = new AsyncCommand(GoToLogin);
            LoginCommand.ExecuteAsync();
        }

        async Task GoToLogin()
        {
            IsBusy = true;
            await Application.Current.MainPage.DisplayAlert("Advertencia", "Sólo se borrarán los datos de conexión del Usuario, NO se borrarán datos relacionados a la Configuración de la Aplicación", "OK");
            await ApplicationUserDBServices.RemoveUser(1);
            var User = await ApplicationUserDBServices.FindUser(1);
            User.RememberData = false;
            await ApplicationUserDBServices.UpdateUser(User);
            string aux = "2";
            var route = $"{nameof(LoginPage)}?ObjectID={aux}";
            await Shell.Current.GoToAsync(route);            
            IsBusy = false;
        }
    }
}   

