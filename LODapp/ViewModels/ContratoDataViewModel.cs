using LODapp.Data;
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
using Xamarin.Essentials;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace LODapp.ViewModels
{
    [QueryProperty(nameof(IdContrato), nameof(IdContrato))]
    public class ContratoDataViewModel : ViewModelBase
    {
       string idContrato;
       public string IdContrato { get => idContrato; set { 
                idContrato = value;
                OnPropertyChanged();
                GetDataCommand.ExecuteAsync();
            } 
        }

          string nombreContrato { get; set; }
          public string NombreContrato { get => nombreContrato; set { nombreContrato = value; OnPropertyChanged(); } }

          string fechaFinalizacion { get; set; }
          public string FechaFinalizacion { get => fechaFinalizacion; set { fechaFinalizacion = value; OnPropertyChanged(); } }

        //int idContrato { get; set; }
        //public int IdContrato { get => idContrato; set { idContrato = value; OnPropertyChanged(); } }

        public bool activo { get; set; }
        public bool Activo
        {
            get => activo;
            set
            {
                activo = value;
                OnPropertyChanged();
            }
        }

        public string Title { get; set; }
        public bool showListLod = false;
        public bool ShowListLod
        {
            get => showListLod;
            set
            {
                showListLod = value;
                OnPropertyChanged();
            }
        }

        public bool activityState = false;
        public bool ActivityState
        {
            get => activityState;
            set
            {
                activityState = value;
                OnPropertyChanged();
            }
        }
        //public bool boolResetBtn = false;
        //public bool BoolResetBtn
        //{
        //    get => boolResetBtn;
        //    set
        //    {
        //        boolResetBtn = value;
        //        OnPropertyChanged();
        //    }
        //}

        CON_Contratos contrato;

        public CON_Contratos Contrato { 
            get => contrato; 
            set
            {
                contrato = value;
                OnPropertyChanged();
            } 
        }

        public ObservableRangeCollection<LOD_LibroObras> LibroObras { get; set; }
        public ObservableRangeCollection<LOD_UsuariosLod> UsuariosContrato { get; set; }
        public ObservableRangeCollection<Grouping<string, CON_Contratos>> ContratoGroups { get; set; }

        public AsyncCommand BuscarCommand { get; }
        public AsyncCommand ListCommand { get; }
        //public AsyncCommand<int> LongPressedCommand { get; }
        public AsyncCommand<int> DetailsCommand { get; }

        public AsyncCommand BackCommand { get; }
        public AsyncCommand/*<string>*/ GetDataCommand { get; }
        public AsyncCommand GetLibrosInetCommand { get; }
        public AsyncCommand GetLibrosDBCommand { get; }
        public AsyncCommand GetUserCommand { get; }
        public AsyncCommand<string> CallUserCommand{ get; }

        public ICommand SearchCommand => new AsyncCommand<string>(async (string query) => await Buscar(query));

        public Command DelayLoadMoreUserCommand { get; set; }

        public ContratoDataViewModel(/*string id*/)
        {
            //IdContrato = id;
            activityState = true;
            Contrato = new CON_Contratos();
            LibroObras = new ObservableRangeCollection<LOD_LibroObras>();
            UsuariosContrato = new ObservableRangeCollection<LOD_UsuariosLod>();
            ContratoGroups = new ObservableRangeCollection<Grouping<string, CON_Contratos>>();
            GetDataCommand = new AsyncCommand/*<string>*/(GetData);
            GetLibrosDBCommand = new AsyncCommand(GetDataLibrosDB);
            GetUserCommand = new AsyncCommand(GetUsers);
            DelayLoadMoreUserCommand = new Command(DelayLoadMoreUsuarios);
            BackCommand = new AsyncCommand(GoToBack);
            DetailsCommand = new AsyncCommand<int>(GoToDetails);
            CallUserCommand = new AsyncCommand<string>(CallUser);
            //IdContrato = "55";
            //GetDataCommand.ExecuteAsync(/*id*/);
            //GetLibrosDBCommand.ExecuteAsync();
            //GetData(Convert.ToInt32(Convert.ToInt32(ObjectID)));
            //BuscarCommand = new AsyncCommand(Buscar);
            //FavoriteCommand = new AsyncCommand<Contrato>(Favorite);
            //ListCommand = new AsyncCommand(Listar);
            //DelayLoadMoreCommand = new Command(DelayLoadMore);
            //LongPressedCommand = new AsyncCommand<int>(LongPressed);
           
            
        }

        LOD_LibroObras libroPrevio;
        LOD_LibroObras libroSeleccionado;

        public LOD_LibroObras selectedLibro
        {
            get => libroSeleccionado;
            set
            {
                if (value != null)
                {
                    //Application.Current.MainPage.DisplayAlert("Seleccionado", value.NombreContrato, "OK");
                    value = null;
                }

                libroSeleccionado = value;
                OnPropertyChanged();
            }
        }

        LOD_UsuariosLod userPrevio;
        LOD_UsuariosLod userSeleccionado;

        public LOD_UsuariosLod selectedUser
        {
            get => userSeleccionado;
            set
            {
                if (value != null)
                {
                    //Application.Current.MainPage.DisplayAlert("Seleccionado", value.NombreContrato, "OK");
                    value = null;
                }

                userSeleccionado = value;
                OnPropertyChanged();
            }
        }


        public async Task GetData(/*string id*/)
        {
            IsBusy = true;
            Contrato = await ContratosDBServices.FindContrato(Convert.ToInt32(/*id*/IdContrato));
            Title = Contrato.CodigoContrato + " - " + Contrato.NombreContrato;
            FechaFinalizacion = Contrato.FechaInicioContrato.Value.AddDays(Contrato.PlazoInicialContrato.Value).ToString("dd-MM-yyyy");
            if(Contrato.EstadoContrato == 1)
            {
                Activo = true;
            }
            else{
                Activo = false;
            }
            //if (contrato != null)
            //{
            //    idContrato = contrato.IdContrato.ToString();
            //}

            if (contrato != null)
            {
                var listAux = await LibroObrasDBServices.FindByContratoLibro(contrato.IdContrato);
                if (listAux.Count > 0) { LibroObras.AddRange(listAux); }

                foreach (var item in LibroObras)
                {
                    if (item.IdTipoLod == 1)
                    {
                        item.Color = Color.FromHex("#F8AC59");
                    }
                    else if (item.IdTipoLod == 2)
                    {
                        item.Color = Color.FromHex("#3498DB");
                    }
                    else
                    {
                        item.Color = Color.FromHex("#62CB31");
                    }

                    if (String.IsNullOrEmpty(item.RutaImagenLObras))
                    {
                        item.UsarRuta = false;
                        item.UsarLocal = true;
                    }
                    else
                    {
                        item.UsarRuta = true;
                        item.UsarLocal = false;
                    }
                }

               

            }


            IsBusy = false;
        }

        public async Task GetDataLibrosDB()
        {
            IsBusy = true;
           
            IsBusy = false;
        }


        public async Task GetUsers()
        {
                var listAux = await LibroObrasDBServices.FindByContratoLibro(contrato.IdContrato);
                List<int> IdLods = listAux.Select(x => x.IdLod).ToList();
                var listAuxUser = await UsuariosLodDBServices.FindUserLodByContrato(IdLods);
                if (listAuxUser.Count > 0) {
                    foreach (var item in listAuxUser)
                    {
                        if(!UsuariosContrato.Select(x => x.UserId).Contains(item.UserId))
                            UsuariosContrato.Add(item); 
                    }
                    
                 }

                foreach (var item in UsuariosContrato)
                {
                    if (String.IsNullOrEmpty(item.RutaImagen)){
                        item.UsarLocal = true;
                        item.UsarRuta = false;
                    }
                    else
                    {
                        item.UsarLocal = false;
                        item.UsarRuta = true;
                        string rutaEdit = item.RutaImagen.Replace("~/", "");
                        item.RutaCompleta = InternetServices.UrlPlataforma + rutaEdit;
                    }
                }

        }


        async Task GoToDetails(int args)
        {
            try{
                string aux = args.ToString();
                var route = $"{nameof(LoadLibroDataPage)}?IdLibro={aux}";
                await Shell.Current.GoToAsync(route);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task CallUser(string number)
        {
            try
            {
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

        void DelayLoadMoreUsuarios()
        {
            if (UsuariosContrato.Count <= 10)
            {
                return;
            }

        }

        async Task Buscar(string query)
        {
            var librosSearch = await LibroObrasDBServices.SearchLibro(query);
            LibroObras = new ObservableRangeCollection<LOD_LibroObras>();
            LibroObras.AddRange(librosSearch);
        }

        async Task GoToBack()
        {
            try
            {
                var route = $"//{nameof(ListContratosPage)}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
