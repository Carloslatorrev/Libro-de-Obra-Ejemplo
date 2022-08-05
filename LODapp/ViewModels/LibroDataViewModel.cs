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
using System.Windows.Input;
using Xamarin.Forms;

namespace LODapp.ViewModels
{
    [QueryProperty(nameof(IdLibro), nameof(IdLibro))]
    public class LibroDataViewModel : ViewModelBase
    {
        int idLibro { get; set; }
        public int IdLibro { get => idLibro; 
            set { 
                idLibro = value; 
                OnPropertyChanged();
                GetDataCommand.ExecuteAsync();
            } }

        public bool aperturado;
        public bool Aperturado { get => aperturado; set { aperturado = value; OnPropertyChanged();}}

        public bool creado;
        public bool Creado { get => creado; set { creado = value; OnPropertyChanged(); } }

        public bool cerrado;
        public bool Cerrado { get => cerrado; set { cerrado = value; OnPropertyChanged(); } }

        public bool showListLod;
        public bool ShowListLod { get => showListLod; set { showListLod = value; OnPropertyChanged(); } }

        Color color { get; set; }

        public Color Color { get => color; set { color = value; OnPropertyChanged(); } }

        public ApplicationUser user;
        public ApplicationUser User { get => user; set { user = value; OnPropertyChanged(); } }

        LOD_LibroObras libro;

        public LOD_LibroObras Libro { get => libro; set { libro = value; OnPropertyChanged();}}

        public FiltradoAnotaciones filtradoAnotaciones = new FiltradoAnotaciones();

        string remitente { get; set; }
        public string Remitente { get => remitente; set { remitente = value; OnPropertyChanged(); } }
        int? destinatario { get; set; }
        public int? Destinatario { get => destinatario; set { destinatario = value; OnPropertyChanged(); } }
        string contenidoCuerpo { get; set; }
        public string ContenidoCuerpo { get => contenidoCuerpo; set { contenidoCuerpo = value; OnPropertyChanged(); } }
        DateTime? fechaPublicacion { get; set; }
        public DateTime? FechaPublicacion { get => fechaPublicacion; set { fechaPublicacion = value; OnPropertyChanged(); } }

        int bandeja { get; set; }
        public int Bandeja { get => bandeja; set { bandeja = value; OnPropertyChanged(); } }

        private ObservableRangeCollection<LOD_Anotaciones> _Anotaciones { get; set; }
        public ObservableRangeCollection<LOD_Anotaciones> Anotaciones {
            get { return _Anotaciones; }
            set
            {
                _Anotaciones = value;
                OnPropertyChanged("Anotaciones");
            }
        }
        public ObservableRangeCollection<LOD_Anotaciones> AnotacionesFiltradas { get; set; }

        public ObservableRangeCollection<LOD_UsuariosLod> Remitentes { get; set; }
        public ObservableRangeCollection<LOD_UsuariosLod> Destinatarios { get; set; }


        public AsyncCommand<string> BuscarCommand { get; }
        public AsyncCommand ListCommand { get; }
        //public AsyncCommand<int> LongPressedCommand { get; }
        public AsyncCommand DelayLoadMoreCommand { get; set; }
        public AsyncCommand<int> DetailsCommand { get; }
        public AsyncCommand AddAnotacionCommand { get; }
        public AsyncCommand GetDataCommand { get; }

        public AsyncCommand GetInetDataCommand { get; }
        public AsyncCommand GetDBDataCommand { get; }
        public AsyncCommand GetFiltradosCommand { get; }
        public AsyncCommand GetUsuariosLodCommand { get; }
        public AsyncCommand GetUserAnotacionCommand { get; }

        public AsyncCommand GetPrincipalCommand { get; }
        public AsyncCommand GetMisPublicacionesCommand { get; }
        public AsyncCommand GetMisBorradoresCommand { get; }
        public AsyncCommand GetMisDestacadasCommand { get; }
        public AsyncCommand GetNombradoEnCommand { get; }
        public AsyncCommand GetMisFirmasPendientesCommand { get; }
        public AsyncCommand GetMisRespPendientesCommand { get; }

        public AsyncCommand BackCommand { get; }

        public ICommand SearchCommand => new AsyncCommand<string>(async (string query) => await Buscar(query));

        public MvvmHelpers.Commands.Command DelayLoadMoreUserCommand { get; set; }


        LOD_Anotaciones anotacionPrevia;
        LOD_Anotaciones anotacionSeleccionada;

        public LOD_Anotaciones selectedAnotacion
        {
            get => anotacionSeleccionada;
            set
            {
                if (value != null)
                {
                    //Application.Current.MainPage.DisplayAlert("Seleccionado", value.NombreContrato, "OK");
                    value = null;
                }

                anotacionSeleccionada = value;
                OnPropertyChanged();
            }
        }

        //LOD_Anotaciones anotacionFiltradaPrevia;
        //LOD_Anotaciones anotacionFiltradaSeleccionada;

        //public LOD_Anotaciones selectedAnotacionFiltrada
        //{
        //    get => anotacionFiltradaSeleccionada;
        //    set
        //    {
        //        if (value != null)
        //        {
        //            //Application.Current.MainPage.DisplayAlert("Seleccionado", value.NombreContrato, "OK");
        //            value = null;
        //        }

        //        anotacionFiltradaSeleccionada = value;
        //        OnPropertyChanged();
        //    }
        //}

        public LibroDataViewModel()
        {
            //IdContrato = id;
            Libro = new LOD_LibroObras();
            Anotaciones = new ObservableRangeCollection<LOD_Anotaciones>();
            AnotacionesFiltradas = new ObservableRangeCollection<LOD_Anotaciones>();
            GetDataCommand = new AsyncCommand(GetDataLibro);
            GetFiltradosCommand = new AsyncCommand(GetFiltrado);
            GetPrincipalCommand = new AsyncCommand(GetBandejaPrincipal);
            GetMisBorradoresCommand = new AsyncCommand(GetMisBorradores);
            GetMisDestacadasCommand = new AsyncCommand(GetMisDestacadas);
            DelayLoadMoreCommand = new AsyncCommand(DelayLoadMoreAsync);
            GetNombradoEnCommand = new AsyncCommand(GetNombradoEn);
            GetMisPublicacionesCommand = new AsyncCommand(GetMisPublicaciones);
            GetMisFirmasPendientesCommand = new AsyncCommand(GetMisFirmasPendientes);
            GetMisRespPendientesCommand = new AsyncCommand(GetMisRespPendientes);
            DetailsCommand = new AsyncCommand<int>(GoToDetails);
            AddAnotacionCommand = new AsyncCommand(AddAnotacion);
            showListLod = false;
            BackCommand = new AsyncCommand(GoToBack);
            //GetData(Convert.ToInt32(Convert.ToInt32(ObjectID)));
            //BuscarCommand = new AsyncCommand<string>(Buscar);
            //FavoriteCommand = new AsyncCommand<Contrato>(Favorite);
            //ListCommand = new AsyncCommand(Listar);
            //DelayLoadMoreCommand = new Command(DelayLoadMore);
            //LongPressedCommand = new AsyncCommand<int>(LongPressed);
        }



        public async Task GetDataLibro()
        {
            IsBusy = true;

            int id = IdLibro;
            User = await ApplicationUserDBServices.FindUser(1);
            libro = await LibroObrasDBServices.FindLibro(Convert.ToInt32(id));
            Libro = await LibroObrasDBServices.FindLibro(Convert.ToInt32(id));
            Title = Libro.CodigoLObras + " - " + Libro.NombreLibroObra;
            idLibro = libro.IdLod;
            if (Libro.IdTipoLod == 1)
            {
                Color = Color.FromHex("#F8AC59");
            }
            else if (Libro.IdTipoLod == 2)
            {
                Color = Color.FromHex("#3498DB");
            }
            else
            {
                Color = Color.FromHex("#62CB31");
            }

            if(Libro.Estado == 0)
            {
                Creado = true;
                Aperturado = false;
                Cerrado = false;
            }else if(Libro.Estado == 1)
            {
                Creado = false;
                Aperturado = true;
                Cerrado = false;
            }
            else if(Libro.Estado == 2)
            {
                Creado = false;
                Aperturado = false;
                Cerrado = true;
            }

            if (Libro != null)
            {
                var listAux = await AnotacionesDBServices.FindAnotacionPrincipal(Libro.IdLod);
                if (listAux.Count > 0) {
                    if(listAux.Count < 5)
                    {
                        for (int i = 0; i < listAux.Count; i++)
                        {
                            Anotaciones.Add(listAux[i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Anotaciones.Add(listAux[i]);
                        }
                        
                    }
                    
                }
            }

            await SetIcons();

            IsBusy = false;

        }

        async Task DelayLoadMoreAsync()
        {
            IsBusy = true;
            var listAux = new List<LOD_Anotaciones>();
                
            if(Anotaciones.Count < 5)
            {
                return;
            }

            switch (Bandeja)
            {
                case 1:
                    listAux = await AnotacionesDBServices.FindAnotacionPrincipal(Libro.IdLod);
                    break;
                case 2:
                    listAux = await filtradoAnotaciones.GetMisPublicaciones(User.UserId, Libro.IdLod);
                    break;
                case 3:
                    listAux = await filtradoAnotaciones.GetMisBorradores(User.UserId, Libro.IdLod);
                    break;
                case 4:
                    LOD_UsuariosLod UserLod = await UsuariosLodDBServices.FindIdByUser(Libro.IdLod, User.UserId);
                    listAux = await filtradoAnotaciones.GetMisDestacadas(Libro.IdLod, UserLod.IdUsLod);
                    break;
                case 5:
                    listAux = await filtradoAnotaciones.GetNombradoEn(Libro.IdLod, User.UserId);
                    break;
                case 6:
                    listAux = await filtradoAnotaciones.GetNombradoEn(Libro.IdLod, User.UserId);
                    break;
                case 7:
                    listAux = await filtradoAnotaciones.GetMisRespPendientes(User.UserId, Libro.IdLod);
                    break;
                default:
                    listAux = await AnotacionesDBServices.FindAnotacionPrincipal(Libro.IdLod);
                    break;
            }


            if (listAux.Count > 0)
            {
                foreach (var item in listAux)
                {
                    if (item.EstadoFirma == true)
                    {
                        item.Borrador = false;
                        item.Publicada = true;
                        item.PendienteFirma = false;
                    }
                    else if (item.EstadoFirma == false && item.UserId != null)
                    {
                        item.Borrador = false;
                        item.Publicada = false;
                        item.PendienteFirma = true;
                    }
                    else
                    {
                        item.Borrador = true;
                        item.Publicada = false;
                        item.PendienteFirma = false;
                    }

                    ApplicationUser user = await ApplicationUserDBServices.FindUser(1);
                    LOD_UsuariosLod userLod = await UsuariosLodDBServices.FindIdByUser(item.IdLod, user.UserId);
                    LOD_UserAnotacion userAnotacion = await UserAnotacionDBServices.FindUserAnotacion(userLod.IdUsLod, item.IdAnotacion);
                    if (userAnotacion != null)
                    {
                        if (userAnotacion.Destacado)
                        {
                            item.DestacadaOn = true;
                            item.DestacadaOff = false;
                        }
                        else
                        {
                            item.DestacadaOn = false;
                            item.DestacadaOff = true;
                        }
                    }
                    else
                    {
                        item.DestacadaOn = false;
                        item.DestacadaOff = true;
                    }

                }


                if (listAux.Count < 5)
                {
                    return;
                }
                else
                {
                    int start = Anotaciones.Count() - 1;
                    int total = Anotaciones.Count() + 5;
                    if (total < listAux.Count())
                    {
                        for (int i = start; i < total; i++)
                        {
                            Anotaciones.Add(listAux[i]);
                        }
                    }
                }

            }

            IsBusy = false;
            
        }


        public async Task GetFiltrado()
        {
            AnotacionesFiltradasView filtro = new AnotacionesFiltradasView();
            filtro.ContenidoCuerpo = ContenidoCuerpo;
            filtro.Destinatario = Destinatario;
            filtro.FechaPublicacion = FechaPublicacion;
            filtro.Remitente = Remitente;

            List<int> listadoIds = new List<int>();
            foreach (var item in Anotaciones)
            {
                listadoIds.Add(item.IdAnotacion);
            }

            Anotaciones = new ObservableRangeCollection<LOD_Anotaciones>();
            var listado = await filtradoAnotaciones.GetFiltrado(filtro, listadoIds);
            Anotaciones.AddRange(listado);

        }

        public async Task SetIcons()
        {
            foreach (var item in Anotaciones)
            {
                if (item.EstadoFirma == true)
                {
                    item.Borrador = false;
                    item.Publicada = true;
                    item.PendienteFirma = false;
                }
                else if (item.EstadoFirma == false && item.UserId != null)
                {
                    item.Borrador = false;
                    item.Publicada = false;
                    item.PendienteFirma = true;
                }
                else
                {
                    item.Borrador = true;
                    item.Publicada = false;
                    item.PendienteFirma = false;
                }

                ApplicationUser user = await ApplicationUserDBServices.FindUser(1);
                LOD_UsuariosLod userLod = await UsuariosLodDBServices.FindIdByUser(item.IdLod, user.UserId);
                LOD_UserAnotacion userAnotacion = await UserAnotacionDBServices.FindUserAnotacion(userLod.IdUsLod, item.IdAnotacion);
                if (userAnotacion != null)
                {
                    if (userAnotacion.Destacado)
                    {
                        item.DestacadaOn = true;
                        item.DestacadaOff = false;
                    }
                    else
                    {
                        item.DestacadaOn = false;
                        item.DestacadaOff = true;
                    }
                }
                else
                {
                    item.DestacadaOn = false;
                    item.DestacadaOff = true;
                }
                
            }

            await Task.Delay(1000);
        }

        //1
        public async Task GetBandejaPrincipal()
        {
            IsBusy = true;
            Anotaciones = new ObservableRangeCollection<LOD_Anotaciones>();
            var listado = await filtradoAnotaciones.GetBandejaPrincipal(Libro.IdLod);
            if (listado.Count > 0)
            {
                if (listado.Count < 5)
                {
                    for (int i = 0; i < listado.Count; i++)
                    {
                        Anotaciones.Add(listado[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Anotaciones.Add(listado[i]);
                    }
                }

            }

            IsBusy = false;
            if (!IsBusy)
            {
                await SetIcons();
            }
           

            Bandeja = 1;
        }

        //2
        public async Task GetMisPublicaciones()
        {
            IsBusy = true;
            Anotaciones = new ObservableRangeCollection<LOD_Anotaciones>();
            var listado = await filtradoAnotaciones.GetMisPublicaciones(User.UserId, Libro.IdLod);
            //var listadoAux = listado.OrderByDescending(x => x.FechaPub).ToList();
            if (listado.Count > 0)
            {
                if (listado.Count < 5)
                {
                    for (int i = 0; i < listado.Count; i++)
                    {
                        Anotaciones.Add(listado[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Anotaciones.Add(listado[i]);
                    }
                }

            }

            IsBusy = false;
            if (!IsBusy)
            {
                await SetIcons();
            }

            Bandeja = 2;
        }

        //3
        public async Task GetMisBorradores()
        {
            IsBusy = true;
            Anotaciones = new ObservableRangeCollection<LOD_Anotaciones>();
            var listado = await filtradoAnotaciones.GetMisBorradores(User.UserId, Libro.IdLod);
            if (listado.Count > 0)
            {
                if(listado.Count < 5)
                {
                    for (int i = 0; i < listado.Count; i++)
                    {
                        Anotaciones.Add(listado[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Anotaciones.Add(listado[i]);
                    }
                }
                
            }

            IsBusy = false;
            if (!IsBusy)
            {
                await SetIcons();
            }

            Bandeja = 3;
        }

        //4
        public async Task GetMisDestacadas()
        {
            IsBusy = true;
            Anotaciones = new ObservableRangeCollection<LOD_Anotaciones>();
            LOD_UsuariosLod UserLod = await UsuariosLodDBServices.FindIdByUser(Libro.IdLod, User.UserId);
            if (UserLod != null)
            {
                var listado = await filtradoAnotaciones.GetMisDestacadas(Libro.IdLod, UserLod.IdUsLod);
                
                if (listado.Count > 0)
                {
                    if (listado.Count < 5)
                    {
                        for (int i = 0; i < listado.Count; i++)
                        {
                            Anotaciones.Add(listado[i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Anotaciones.Add(listado[i]);
                        }
                    }
                }
            }

            IsBusy = false;
            if (!IsBusy)
            {
                await SetIcons();
            }

            Bandeja = 4;
        }


        //5
        public async Task GetNombradoEn()
        {
            IsBusy = true;
            Anotaciones = new ObservableRangeCollection<LOD_Anotaciones>();
            var listado = await filtradoAnotaciones.GetNombradoEn(Libro.IdLod, User.UserId);
            //var listadoAux = listado.OrderByDescending(x => x.FechaIngreso).ToList();
            if (listado.Count > 0)
            {
                if (listado.Count < 5)
                {
                    for (int i = 0; i < listado.Count; i++)
                    {
                        Anotaciones.Add(listado[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Anotaciones.Add(listado[i]);
                    }
                }
            }

            IsBusy = false;
            if (!IsBusy)
            {
                await SetIcons();
            }

            Bandeja = 5;
        }

        //6
        public async Task GetMisFirmasPendientes()
        {
            IsBusy = true;
            Anotaciones = new ObservableRangeCollection<LOD_Anotaciones>();
            var listado = await filtradoAnotaciones.GetNombradoEn(Libro.IdLod, User.UserId);
            //var listadoAux = listado.OrderByDescending(x => x.FechaIngreso).ToList();
            if (listado.Count > 0)
            {
                if (listado.Count < 5)
                {
                    for (int i = 0; i < listado.Count; i++)
                    {
                        Anotaciones.Add(listado[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Anotaciones.Add(listado[i]);
                    }
                }
            }

            IsBusy = false;
            if (!IsBusy)
            {
                await SetIcons();
            }

            Bandeja = 6;
        }

        //7
        public async Task GetMisRespPendientes()
        {
            IsBusy = true;
            Anotaciones = new ObservableRangeCollection<LOD_Anotaciones>();
            var listado = await filtradoAnotaciones.GetMisRespPendientes(User.UserId, Libro.IdLod);
            //var listadoAux = listado.OrderByDescending(x => x.FechaIngreso).ToList();
            if (listado.Count > 0)
            {
                if (listado.Count < 5)
                {
                    for (int i = 0; i < listado.Count; i++)
                    {
                        Anotaciones.Add(listado[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Anotaciones.Add(listado[i]);
                    }
                }
            }

            IsBusy = false;
            if (!IsBusy)
            {
                await SetIcons();
            }

            Bandeja = 7;
        }


        async Task Buscar(string query)
        {
            if (query.Length > 3)
            {
                var librosSearch = await AnotacionesDBServices.SearchAnotacion(query);
                Anotaciones = new ObservableRangeCollection<LOD_Anotaciones>();
                Anotaciones.AddRange(librosSearch);

                await SetIcons();
            }
        }

        async Task GoToBack()
        {
            try
            {
                string aux = Libro.IdContrato.ToString();
                var route = $"{nameof(LoadContratoDataPage)}?IdContrato={aux}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task GoToDetails(int args)
        {
            try
            {
                string aux = args.ToString();
                var route = $"{nameof(LoadAnotacionDataPage)}?IdAnotacion={aux}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task AddAnotacion()
        {
            try
            {
                if (Libro.Estado == 1)
                {
                    var route = $"{nameof(AddAnotacionPage)}?IdLibro={IdLibro}";
                    await Shell.Current.GoToAsync(route);
                }
                else
                {

                    await Application.Current.MainPage.DisplayAlert("Error","Libro no se encuentra en estado de Apertura", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}
