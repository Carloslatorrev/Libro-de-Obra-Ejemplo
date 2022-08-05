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
    [QueryProperty(nameof(IdAnotacion), nameof(IdAnotacion))]
    class AddReferenciaViewModel : ViewModelBase
    {
        int idAnotacion { get; set; }
        public int IdAnotacion { get => idAnotacion; set { idAnotacion = value; OnPropertyChanged();
                GetLibrosCommand.ExecuteAsync();
                GetAnotacionesCommand.ExecuteAsync();
            } }

        int idLibro { get; set; }
        public int IdLibro { get => idLibro; set { idLibro = value; OnPropertyChanged(); } }

        int idAnotacionRef { get; set; }
        public int IdAnotacionRef { get => idAnotacionRef; set { idAnotacionRef = value; OnPropertyChanged(); } }

        string observacion { get; set; }
        public string Observacion { get => observacion; set { observacion = value; OnPropertyChanged(); } }

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

        LOD_LibroObras libroSeleccionado;

        public LOD_LibroObras selectedLibro
        {
            get => libroSeleccionado;
            set
            {
                libroSeleccionado = value;
                idLibro = value.IdLod;
                GetAnotacionesCommand.ExecuteAsync();
                OnPropertyChanged();
            }
        }

        LOD_Anotaciones anotacionRefSeleccionado;

        public LOD_Anotaciones selectedAnotacionRef
        {
            get => anotacionRefSeleccionado;
            set
            {
                anotacionRefSeleccionado = value;
                IdAnotacionRef = value.IdAnotacion;
                OnPropertyChanged();
            }
        }

        private ObservableRangeCollection<LOD_LibroObras> _ListLibroObras; 
        public ObservableRangeCollection<LOD_LibroObras> ListLibroObras
        {
            get { return _ListLibroObras; }
            set
            {
                _ListLibroObras = value;
                OnPropertyChanged("ListLibroObras");
            }
        }
        //public ObservableRangeCollection<MAE_SubtipoComunicacion> ListSubTipoComunicacion { get; set; }

        private ObservableRangeCollection<LOD_Anotaciones> _ListAnotaciones;
        public ObservableRangeCollection<LOD_Anotaciones> ListAnotaciones
        {
            get { return _ListAnotaciones; }
            set
            {
                _ListAnotaciones = value;
                OnPropertyChanged("ListAnotaciones");
            }
        }

        public AsyncCommand GetLibrosCommand { get; }

        public AsyncCommand GetAnotacionesCommand { get; }
        public AsyncCommand CreateReferenciaCommand { get; }
        public AsyncCommand BackCommand { get; }

        public AddReferenciaViewModel()
        {
            GetLibrosCommand = new AsyncCommand(GetLibros);
            GetAnotacionesCommand = new AsyncCommand(GetAnotaciones);
            CreateReferenciaCommand = new AsyncCommand(CreateReferencia);
            BackCommand = new AsyncCommand(GoToBack);
            ListAnotaciones = new ObservableRangeCollection<LOD_Anotaciones>();
            ListLibroObras = new ObservableRangeCollection<LOD_LibroObras>();
            
        }

        public async Task CreateReferencia()
        {

            IsBusy = true;
            CreateReferencia refCreate = new CreateReferencia();
            refCreate.IdAnontacionRef = IdAnotacionRef;
            refCreate.IdAnotacion = IdAnotacion;
            refCreate.Observacion = Observacion;
            refCreate.EsRepuesta = false;
            LOD_ReferenciasAnot refInet = new LOD_ReferenciasAnot();

            ConnectivityHelper helperConnection = new ConnectivityHelper();
            bool connection = helperConnection.VerifyConnection();
            if (connection)
            {
                refInet = await InternetAnotacionServices.CreateReferencia(refCreate);
            }
            else
            {
                refInet = null;
            }

            if (refInet != null)
            {
                refInet.Actualizado = 1;
                await ReferenciasAnotDBServices.AddReferenciaAnot(refInet);
            }
            else
            {
                LOD_ReferenciasAnot refAux = new LOD_ReferenciasAnot();
                List<LOD_ReferenciasAnot> listadoRef = await ReferenciasAnotDBServices.GetReferenciaAnot();
                int lastId = listadoRef.OrderByDescending(x => x.IdRefAnot).FirstOrDefault().IdRefAnot + 1;
                refAux.IdRefAnot = lastId;
                refAux.IdAnontacionRef = refCreate.IdAnontacionRef;
                refAux.IdAnotacion = refCreate.IdAnotacion;
                refAux.Observacion = refCreate.Observacion;
                LOD_Anotaciones anotOrigen = await AnotacionesDBServices.FindAnotacion(refCreate.IdAnotacion);
                refAux.AnotacionOrigen = anotOrigen.Correlativo + " - " + anotOrigen.Titulo;
                LOD_Anotaciones anotReferenciada = await AnotacionesDBServices.FindAnotacion(refCreate.IdAnontacionRef);
                refAux.AnotacionReferencia = anotReferenciada.Correlativo + " - " + anotReferenciada.Titulo;
                refAux.Actualizado = 2;
                await ReferenciasAnotDBServices.AddReferenciaAnot(refAux);
            }
                
            IsBusy = false;
            if (IsBusy == false)
            {
                var route = $"{nameof(LoadAnotacionDataPage)}?IdAnotacion={IdAnotacion}";
                await Shell.Current.GoToAsync(route);
            }
        }

        public async Task GetLibros()
        {
            IsBusy = true;
            LOD_Anotaciones anotDB = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
            LOD_LibroObras libro = await LibroObrasDBServices.FindLibro(anotDB.IdLod);
            ListLibroObras = new ObservableRangeCollection<LOD_LibroObras>();
            var listado = await LibroObrasDBServices.GetLibroObrasByContrato(libro.IdContrato);
            ListLibroObras.AddRange(listado);
            IsBusy = false;
        }

        public async Task GetAnotaciones()
        {
            IsBusy = true;
            if (IdLibro == null || IdLibro == 0)
            {
                ListAnotaciones = null;
                ListAnotaciones = new ObservableRangeCollection<LOD_Anotaciones>();
                List<LOD_Anotaciones> listado = await AnotacionesDBServices.GetAnotacion();
                ListAnotaciones.AddRange(listado);
            }
            else
            {
                ListAnotaciones = new ObservableRangeCollection<LOD_Anotaciones>();
                ListAnotaciones.AddRange(await AnotacionesDBServices.FindAnotacionByLod(IdLibro));
                
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
