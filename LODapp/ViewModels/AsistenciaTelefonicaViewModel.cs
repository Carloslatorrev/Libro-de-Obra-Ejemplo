using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LODapp.ViewModels
{
    class AsistenciaTelefonicaViewModel : ViewModelBase
    {
        public AsyncCommand CallSoporteCommand { get; }
        public AsistenciaTelefonicaViewModel()
        {
            CallSoporteCommand = new AsyncCommand(CallSoporte);
        }

        async Task CallSoporte()
        {
            try
            {
                string number = "+5691111111";
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
                await Application.Current.MainPage.DisplayAlert("Error", "El Número de teléfono al que intenta contactarno posee el formato correcto para la llamada", "OK");
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
                await Application.Current.MainPage.DisplayAlert("Error", "El Dispositivo con el cual intenta llamar no está habilitado para realizar esta función", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Se ha producido un error inesperado, Contacte a Soporte", "OK");
                // Other error has occurred.
            }
        }
    }
}
