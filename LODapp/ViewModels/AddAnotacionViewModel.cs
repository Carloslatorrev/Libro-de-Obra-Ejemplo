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
    [QueryProperty(nameof(IdLibro), nameof(IdLibro))]
    class AddAnotacionViewModel : ViewModelBase
    {
        string tituloAnotacion { get; set; }
        public string TituloAnotacion { get => tituloAnotacion; set { tituloAnotacion = value; OnPropertyChanged(); } }

        string cuerpoAnotacion { get; set; }
        public string CuerpoAnotacion { get => cuerpoAnotacion; set { cuerpoAnotacion = value; OnPropertyChanged(); } }

        int idLibro { get; set; }
        public int IdLibro { get => idLibro; 
            set {
                idLibro = value;
                OnPropertyChanged();
                GetLibroCommand.ExecuteAsync();
                GetTipoComunicacionCommand.ExecuteAsync();
                GetSubtipoComunicacionCommand.ExecuteAsync();
                GetReceptoresCommand.ExecuteAsync();
            }
        }

        int idTipoCom { get; set; }
        public int IdTipoCom { get => idTipoCom; set { idTipoCom = value; OnPropertyChanged(); } }

        int idSubtipoCom { get; set; }
        public int IdSubTipoCom { get => idSubtipoCom; set { idSubtipoCom = value; OnPropertyChanged(); } }

        bool solicitaRespuesta { get; set; }
        public bool SolicitaRespuesta { get => solicitaRespuesta; set { solicitaRespuesta = value; OnPropertyChanged(); } }

        DateTime? fechaRespuesta { get; set; }
        public DateTime? FechaRespuesta { get => fechaRespuesta; set { fechaRespuesta = value; OnPropertyChanged(); } }

        int receptor { get; set; }
        public int Receptor { get => receptor; set { receptor = value; OnPropertyChanged(); } }

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
                libro = value;
                OnPropertyChanged();
            }
        }

        MAE_TipoComunicacion tipoComSeleccionado;

        public MAE_TipoComunicacion selectedTipoComunicacion
        {
            get => tipoComSeleccionado;
            set
            {
                tipoComSeleccionado = value;
                idTipoCom = value.IdTipoCom;
                GetSubtipoComunicacionCommand.ExecuteAsync();
                OnPropertyChanged();
            }
        }

        MAE_SubtipoComunicacion SubtipoComSeleccionado;

        public MAE_SubtipoComunicacion selectedSubTipoComunicacion
        {
            get => SubtipoComSeleccionado;
            set
            {
                SubtipoComSeleccionado = value;
                idSubtipoCom = value.IdTipoSub;
                OnPropertyChanged();
            }
        }

        LOD_UsuariosLod ReceptorSeleccionado;

        public LOD_UsuariosLod selectedReceptor
        {
            get => ReceptorSeleccionado;
            set
            {
                if (value != null)
                {
                    //Application.Current.MainPage.DisplayAlert("Seleccionado", value.NombreContrato, "OK");
                    value = null;
                }

                ReceptorSeleccionado = value;
                receptor = value.IdUsLod;
                OnPropertyChanged();
            }
        }


        public ObservableRangeCollection<MAE_TipoComunicacion> ListTipoComunicacion { get; set; }
        //public ObservableRangeCollection<MAE_SubtipoComunicacion> ListSubTipoComunicacion { get; set; }

        private ObservableRangeCollection<MAE_SubtipoComunicacion> _ListSubTipoComunicacion;
        public ObservableRangeCollection<MAE_SubtipoComunicacion> ListSubTipoComunicacion
        {
            get { return _ListSubTipoComunicacion; }
            set
            {
                _ListSubTipoComunicacion = value;
                OnPropertyChanged("ListSubTipoComunicacion");
            }
        }

        public ObservableRangeCollection<LOD_UsuariosLod> ListReceptores { get; set; }

        public AsyncCommand CreateAnotacionCommand { get; }
        public AsyncCommand CreateReceptorDBCommand { get; }
        public AsyncCommand GetTipoComunicacionCommand { get; }
        public AsyncCommand GetSubtipoComunicacionCommand { get; }
        public AsyncCommand GetReceptoresCommand { get; }
        public AsyncCommand GetLibroCommand { get; }
        public AsyncCommand BackCommand { get; }

        public AddAnotacionViewModel()
        {
            libro = new LOD_LibroObras();
            Libro = new LOD_LibroObras();
            ListTipoComunicacion = new ObservableRangeCollection<MAE_TipoComunicacion>();
            ListSubTipoComunicacion = new ObservableRangeCollection<MAE_SubtipoComunicacion>();
            ListReceptores = new ObservableRangeCollection<LOD_UsuariosLod>();
            CreateAnotacionCommand = new AsyncCommand(CreateAnotacion);
            GetTipoComunicacionCommand = new AsyncCommand(GetTipoComunicacion);
            GetSubtipoComunicacionCommand = new AsyncCommand(GetSubtipoComunicacion);
            GetReceptoresCommand = new AsyncCommand(GetReceptores);
            GetLibroCommand = new AsyncCommand(GetLibro);
            BackCommand = new AsyncCommand(GoToBack);
            

        }

        public async Task GetLibro()
        {
            IsBusy = true;
            Libro = await LibroObrasDBServices.FindLibro(IdLibro);
            IsBusy = false;

        }

        public async Task CreateAnotacion()
        {
            IsBusy = true;
            ApplicationUser user = await ApplicationUserDBServices.FindUser(1);
            AnotacionesCreate anotacionCreate = new AnotacionesCreate();
            anotacionCreate.IdLod = IdLibro;
            anotacionCreate.IdSubTipo = IdSubTipoCom;
            anotacionCreate.SolicitudResp = solicitaRespuesta;
            anotacionCreate.titulo = tituloAnotacion;
            anotacionCreate.cuerpo = CuerpoAnotacion;
            anotacionCreate.FechaSolicitud = FechaRespuesta;
            anotacionCreate.UserId = user.UserId;
            
            if(Receptor == null || Receptor == 0)
            {
                anotacionCreate.SolicitudVB = false;
            }
            else
            {
                anotacionCreate.SolicitudVB = true;
            }

            try
            {
                ConnectivityHelper helperConnection = new ConnectivityHelper();
                bool connection = helperConnection.VerifyConnection();
                if(connection)
                {
                    Anotacion = await InternetAnotacionServices.CreateAnotacion(anotacionCreate);
                }
                else
                {
                    Anotacion = null;
                }
                
                if(Anotacion != null)
                {
                    Anotacion.Actualizado = 1;
                    await AnotacionesDBServices.AddAnotacion(Anotacion);
                    var route = $"{nameof(LoadAnotacionDataPage)}?IdAnotacion={Anotacion.IdAnotacion}";
                    await Shell.Current.GoToAsync(route);
                }
                else
                {
                    Anotacion = new LOD_Anotaciones();
                    int idAnot = await AnotacionesDBServices.LastAnotacion() + 1;
                    Anotacion.IdAnotacion = idAnot;
                    Anotacion.IdTipoSub = anotacionCreate.IdSubTipo;
                    Anotacion.IdLod = anotacionCreate.IdLod;
                    Anotacion.UserIdBorrador = anotacionCreate.UserId;
                    Anotacion.Cuerpo = anotacionCreate.cuerpo;
                    Anotacion.Titulo = anotacionCreate.titulo;
                    Anotacion.Correlativo = 0;
                    Anotacion.EsEstructurada = false; //Depende del subtipo de anotación
                    Anotacion.Estado = 0;//borrador
                    Anotacion.FechaIngreso = DateTime.Now;
                    Anotacion.SolicitudRest = anotacionCreate.SolicitudResp;
                    Anotacion.FechaTopeRespuesta = anotacionCreate.FechaSolicitud;
                    Anotacion.SolicitudVB = anotacionCreate.SolicitudVB;
                    Anotacion.TipoFirma = 1; //depende del tipo de Usuario y su perfil, estado userFirma avanzada
                    Anotacion.EstadoFirma = false;
                    Anotacion.FechaPub = null;
                    Anotacion.FechaResp = anotacionCreate.FechaSolicitud;
                    Anotacion.Actualizado = 2;
                    await AnotacionesDBServices.AddAnotacion(Anotacion);
                    
                    
                    LOD_UsuariosLod userLod = await UsuariosLodDBServices.FindIdByUser(Anotacion.IdLod, user.UserId);
                    LOD_UserAnotacion newUserAnot = new LOD_UserAnotacion();
                    newUserAnot.IdUsLod = userLod.IdUsLod;
                    newUserAnot.IdAnotacion = Anotacion.IdAnotacion;
                    newUserAnot.RespVB = false;
                    newUserAnot.Leido = true;
                    newUserAnot.TipoVB = 0;
                    newUserAnot.EsPrincipal = false;
                    newUserAnot.EsRespRespuesta = false;
                    newUserAnot.Actualizado = 2;
                    await UserAnotacionDBServices.AddUserAnotacion(newUserAnot);

                    if (Receptor != 0 && Receptor != null)
                    {
                        await CreateReceptor();
                    }

                    IsBusy = false;
                    var route = $"{nameof(LoadAnotacionDataPage)}?ObjectID={Anotacion.IdAnotacion}";
                    await Shell.Current.GoToAsync(route);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", "No se ha podido crear la Anotación, contacte a Soporte", "OK");
            }


            IsBusy = false;

        }


        public async Task CreateReceptor()
        {
            IsBusy = true;
            if (Receptor != 0 && Receptor != null)
            {
                ReceptoresView recepCreate = new ReceptoresView();
                recepCreate.EsPrincipal = true;
                recepCreate.EsRespRespuesta = false;
                recepCreate.IdAnotacion = Anotacion.IdAnotacion;
                recepCreate.IdUsLod = Receptor;

                LOD_UserAnotacion userCreate = await InternetAnotacionServices.CreateReceptor(recepCreate);
                if(userCreate != null)
                {
                    await UserAnotacionDBServices.AddUserAnotacion(userCreate);
                }

            }
            IsBusy = false;

        }


        public async Task GetTipoComunicacion()
        {
            IsBusy = true;
            if (Libro.IdTipoLod == 1 || Libro.IdTipoLod == 2)
            {
                var listado = await TipoComunicacionDBServices.GetTipoCom();
                ListTipoComunicacion.AddRange(listado);
            }
            else
            {
                var listado = await TipoComunicacionDBServices.GetTipoComByLod(Libro.IdTipoLod);
                ListTipoComunicacion.AddRange(listado);
            }
            
            IsBusy = false;

        }

        public async Task GetReceptores()
        {
            IsBusy = true;
            ApplicationUser user = await ApplicationUserDBServices.FindUser(1);
            var listado = await UsuariosLodDBServices.GetReceptores(IdLibro, user.UserId);
            foreach (var item in listado)
            {
                if(!ListReceptores.Select(x => x.UserId).Contains(item.UserId))
                {
                    ListReceptores.Add(item);
                }
            }

            IsBusy = false;

        }

        public async Task GetSubtipoComunicacion()
        {
            IsBusy = true;
            if (IdTipoCom == null || IdTipoCom == 0)
            {
                ListSubTipoComunicacion = new ObservableRangeCollection<MAE_SubtipoComunicacion>();
                if (Libro.IdTipoLod == 1 || Libro.IdTipoLod == 2)
                {
                    var listado = await SubtipoComunicacionDBServices.GetSubtipos();
                    ListSubTipoComunicacion.AddRange(listado);
                }
                else
                {
                    var listado = await SubtipoComunicacionDBServices.GetSubtiposByLod(Libro.IdTipoLod);
                    ListSubTipoComunicacion.AddRange(listado);
                }
            }
            else
            {
                ListSubTipoComunicacion = null;
                ListSubTipoComunicacion = new ObservableRangeCollection<MAE_SubtipoComunicacion>();
                List<MAE_SubtipoComunicacion> listado = await SubtipoComunicacionDBServices.GetSubtiposByTipo(IdTipoCom);
                ListSubTipoComunicacion.AddRange(listado);
            }
            IsBusy = false;

        }

        async Task GoToBack()
        {
            try
            {
                string aux = IdLibro.ToString();
                var route = $"{nameof(LoadLibroDataPage)}?IdLibro={aux}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
