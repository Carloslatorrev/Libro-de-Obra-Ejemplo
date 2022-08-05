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
    class EditarAnotacionViewModel : ViewModelBase
    {
        string tituloAnotacion { get; set; }
        public string TituloAnotacion { get => tituloAnotacion; set { tituloAnotacion = value; OnPropertyChanged(); } }

        string cuerpoAnotacion { get; set; }
        public string CuerpoAnotacion { get => cuerpoAnotacion; set { cuerpoAnotacion = value; OnPropertyChanged(); } }

        int idAnotacion { get; set; }
        public int IdAnotacion { get => idAnotacion; set { idAnotacion = value; OnPropertyChanged();
                GetAnotacionCommand.ExecuteAsync();
            } }

        bool solicitaRespuesta { get; set; }
        public bool SolicitaRespuesta { get => solicitaRespuesta; set { solicitaRespuesta = value; OnPropertyChanged(); } }

        DateTime? fechaRespuesta { get; set; }
        public DateTime? FechaRespuesta { get => fechaRespuesta; set { fechaRespuesta = value; OnPropertyChanged(); } }


        LOD_Anotaciones anotacion;


        public LOD_Anotaciones Anotacion
        {
            get => anotacion;
            set
            {
                anotacion = value;
                OnPropertyChanged();
            }
        }

        LOD_LibroObras libro;

        public LOD_LibroObras Libro
        {
            get => libro;
            set
            {
                Libro = value;
                OnPropertyChanged();
            }
        }

        MAE_TipoComunicacion tipoComSeleccionado;

        LOD_UsuariosLod ReceptorSeleccionado;

        public AsyncCommand EditAnotacionCommand { get; }
        public AsyncCommand GetAnotacionCommand { get; }
        public AsyncCommand BackCommand { get; }

        public EditarAnotacionViewModel()
        {
            EditAnotacionCommand = new AsyncCommand(EditAnotacion);
            GetAnotacionCommand = new AsyncCommand(GetAnotacion);
            BackCommand = new AsyncCommand(GoToBack);
        }

        public async Task GetAnotacion()
        {
            IsBusy = true;
            anotacion = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
            Anotacion = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
            TituloAnotacion = Anotacion.Titulo;
            CuerpoAnotacion = Anotacion.Cuerpo;
            SolicitaRespuesta = Anotacion.SolicitudRest;
            FechaRespuesta = Anotacion.FechaTopeRespuesta;
            IsBusy = false;
           
        }

        public async Task EditAnotacion()
        {
            IsBusy = true;
            Anotacion.Titulo = TituloAnotacion;
            Anotacion.Cuerpo = CuerpoAnotacion;
            Anotacion.SolicitudRest = SolicitaRespuesta;
            FechaRespuesta = FechaRespuesta;
            try
            {
                UpdateAnotacion updateAnotacion = new UpdateAnotacion();
                updateAnotacion.titulo = Anotacion.Titulo;
                updateAnotacion.cuerpo = Anotacion.Cuerpo;
                updateAnotacion.IdAnotacion = Anotacion.IdAnotacion;
                updateAnotacion.IdLod = Anotacion.IdLod;
                updateAnotacion.SolicitudResp = Anotacion.SolicitudRest;
                updateAnotacion.FechaSolicitud = Anotacion.FechaTopeRespuesta;
                LOD_Anotaciones AnotacionInet = new LOD_Anotaciones();
                ConnectivityHelper helperConnection = new ConnectivityHelper();
                bool connection = helperConnection.VerifyConnection();
                if (connection)
                {
                    AnotacionInet = await InternetAnotacionServices.UpdateAnotacion(updateAnotacion);
                }
                else
                {
                    AnotacionInet = null;
                }

               
                if (AnotacionInet != null)
                {
                    AnotacionInet.Actualizado = 3;
                    await AnotacionesDBServices.UpdateAnotacion(AnotacionInet);
                    var route = $"{nameof(LoadAnotacionDataPage)}?IdAnotacion={AnotacionInet.IdAnotacion}";
                    await Shell.Current.GoToAsync(route);
                }
                else
                {
                    await AnotacionesDBServices.UpdateAnotacion(Anotacion);
                    IsBusy = false;
                    var route = $"{nameof(LoadAnotacionDataPage)}?IdAnotacion={Anotacion.IdAnotacion}";
                    await Shell.Current.GoToAsync(route);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", "No se ha podido Editar la Anotación, contacte a Soporte", "OK");
            }


            IsBusy = false;

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
