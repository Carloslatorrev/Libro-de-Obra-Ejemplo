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
    [QueryProperty(nameof(IdUserAnotacion), nameof(IdUserAnotacion))]
    class EditarReceptorViewModel : ViewModelBase
    {
        int idUserAnot { get; set; }
        public int IdUserAnotacion { get => idUserAnot; set { idUserAnot = value; OnPropertyChanged(); GetDataCommand.ExecuteAsync(); } }
        int idLibro { get; set; }
        public int IdLibro { get => idLibro; set { idLibro = value; OnPropertyChanged(); } }

        int receptor { get; set; }
        public int Receptor { get => receptor; set { receptor = value; OnPropertyChanged(); } }

        bool principal = false;
        public bool Principal { get => principal; set { Principal = value; OnPropertyChanged(); } }

        bool solicitaRespuesta = false;
        public bool SolicitaRespuesta { get => solicitaRespuesta; set { solicitaRespuesta = value; OnPropertyChanged(); } }

        string nombreUser { get; set; }
        public string NombreUser { get => nombreUser; set { nombreUser = value; OnPropertyChanged(); } }

        public AsyncCommand EditReceptorCommand { get; }
        public AsyncCommand GetDataCommand { get; }
        public AsyncCommand BackCommand { get; }

        public EditarReceptorViewModel()
        {
            EditReceptorCommand = new AsyncCommand(EditNewReceptor);
            GetDataCommand = new AsyncCommand(GetData);
            BackCommand = new AsyncCommand(GoToBack);
        }

        public async Task GetData()
        {
            LOD_UserAnotacion userAnot = await UserAnotacionDBServices.FindUserAnotacion(IdUserAnotacion);
            LOD_UsuariosLod userLod = await UsuariosLodDBServices.FindUserLod(userAnot.IdUsLod);
            NombreUser = userLod.Usuario;
        }

        public async Task EditNewReceptor()
        {
            IsBusy = true;
            LOD_UsuariosLod userLod = await UsuariosLodDBServices.FindUserLod(receptor);
            LOD_Anotaciones anot = await AnotacionesDBServices.FindAnotacion(IdUserAnotacion);
            LOD_UserAnotacion userAnot = new LOD_UserAnotacion();
            ReceptoresView receptoresView = new ReceptoresView();
            receptoresView.IdAnotacion = userAnot.IdAnotacion;
            receptoresView.EsRespRespuesta = SolicitaRespuesta;
            receptoresView.IdUsLod = userLod.IdUsLod;
            receptoresView.EsPrincipal = Principal;
            try
            {
                ConnectivityHelper helperConnection = new ConnectivityHelper();
                bool connection = helperConnection.VerifyConnection();
                if (connection)
                {
                    userAnot = await InternetAnotacionServices.UpdateReceptor(receptoresView);
                }
                else
                {
                    userAnot = null;
                }


                if (userAnot != null)
                {
                    userAnot.Actualizado = 3;
                    await UserAnotacionDBServices.AddUserAnotacion(userAnot);
                }
                else
                {
                    userAnot = new LOD_UserAnotacion();
                    userAnot.IdUsLod = Receptor;
                    userAnot.IdAnotacion = IdUserAnotacion;
                    userAnot.FechaVB = null;
                    userAnot.EsPrincipal = Principal;
                    userAnot.EsRespRespuesta = SolicitaRespuesta;
                    userAnot.RespVB = false;
                    userAnot.Leido = false;
                    userAnot.Destacado = false;
                    userAnot.UsuarioLod = userLod.Usuario;
                    userAnot.Anotacion = anot.Correlativo + " - " + anot.Titulo;
                    userAnot.Actualizado = 4;
                    await UserAnotacionDBServices.AddUserAnotacion(userAnot);
                }

                IsBusy = false;
                if (IsBusy == false)
                {
                    var route = $"{nameof(LoadAnotacionDataPage)}?IdAnotacion={userAnot.IdAnotacion}";
                    await Shell.Current.GoToAsync(route);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", "No se ha podido Editar el Receptor, contacte a Soporte", "OK");
            }
            
        }

        async Task GoToBack()
        {
            try
            {
                LOD_UserAnotacion userAnot = await UserAnotacionDBServices.FindUserAnotacion(IdUserAnotacion);
                int IdAnotacion = userAnot.IdAnotacion;
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
