using LODapp.Data;
using LODapp.Helpers;
using LODapp.Models;
using LODapp.Services;
using LODapp.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LODapp.ViewModels
{
    [QueryProperty(nameof(IdAnotacion), nameof(IdAnotacion))]
    class SolicitarFirmaViewModel : ViewModelBase
    {
        int idAnotacion { get; set; }
        public int IdAnotacion { get => idAnotacion; set { idAnotacion = value; OnPropertyChanged();
                SetDataCommand.ExecuteAsync();
            } }

        int firmante { get; set; }
        public int Firmante { get => firmante; set { firmante = value; OnPropertyChanged(); } }

        LOD_UsuariosLod firmanteSeleccionado;

        public LOD_UsuariosLod selectedFirmante
        {
            get => firmanteSeleccionado;
            set
            {
                firmanteSeleccionado = value;
                firmante = value.IdUsLod;
                OnPropertyChanged();
            }
        }

        private ObservableRangeCollection<LOD_UsuariosLod> _ListFirmantes;
        public ObservableRangeCollection<LOD_UsuariosLod> ListFirmantes
        {
            get { return _ListFirmantes; }
            set
            {
                _ListFirmantes = value;
                OnPropertyChanged("ListFirmantes");
            }
        }

        public AsyncCommand BackCommand { get; }
        public AsyncCommand SolicitarFirmaCommand { get; }
        public AsyncCommand SetDataCommand { get; }


        public SolicitarFirmaViewModel()
        {
            BackCommand = new AsyncCommand(GoToBack);
            SolicitarFirmaCommand = new AsyncCommand(SolicitarFirma);
            SetDataCommand = new AsyncCommand(SetData);
            ListFirmantes = new ObservableRangeCollection<LOD_UsuariosLod>();
        }

        async Task SetData()
        {
            IsBusy = true;
            LOD_Anotaciones anot = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
            int IdLibro = anot.IdLod;
            ApplicationUser user = await ApplicationUserDBServices.FindUser(1);
            var listado = await UsuariosLodDBServices.GetReceptores(IdLibro, user.UserId);
            ListFirmantes.AddRange(listado);
            IsBusy = false;

        }

        async Task SolicitarFirma()
        {
            LOD_Anotaciones anotacion = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
            LOD_UsuariosLod userLod = await UsuariosLodDBServices.FindUserLod(Firmante);
            if (anotacion.EstadoFirma == false)
            {
                LOD_Anotaciones anotacionInet = new LOD_Anotaciones();
                ConnectivityHelper helperConnection = new ConnectivityHelper();
                bool connection = helperConnection.VerifyConnection();
                if (connection)
                {
                    SolicitudFirma solicitarFirma = new SolicitudFirma();
                    solicitarFirma.IdAnotacion = IdAnotacion;
                    solicitarFirma.IdFirmante = userLod.IdUsLod;
                    anotacionInet = await InternetAnotacionServices.SolicitarFirma(solicitarFirma);
                    anotacionInet.Actualizado = 3;
                    await AnotacionesDBServices.UpdateAnotacion(anotacionInet);
                    
                }
                else
                {
                    anotacion.Estado = 1;
                    anotacion.UserId = userLod.UserId;
                    anotacion.Actualizado = 4;
                    await AnotacionesDBServices.UpdateAnotacion(anotacion);
                }

                string id = IdAnotacion.ToString();
                var route = $"{nameof(LoadAnotacionDataPage)}?IdAnotacion={id}";
                await Shell.Current.GoToAsync(route);
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
