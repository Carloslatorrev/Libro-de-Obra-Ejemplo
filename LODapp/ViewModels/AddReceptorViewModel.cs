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
    class AddReceptorViewModel :ViewModelBase
    {
        int idAnotacion { get; set; }
        public int IdAnotacion { get => idAnotacion; set { idAnotacion = value; OnPropertyChanged();
                GetReceptoresCommand.ExecuteAsync();
            } }

        int idLibro { get; set; }
        public int IdLibro { get => idLibro; set { idLibro = value; OnPropertyChanged(); } }

        int receptor { get; set; }
        public int Receptor { get => receptor; set { receptor = value; OnPropertyChanged(); } }

        bool principal = false;
        public bool Principal { get => principal; set { principal = value; OnPropertyChanged(); } }

        bool solicitaRespuesta = false;
        public bool SolicitaRespuesta { get => solicitaRespuesta; set { solicitaRespuesta = value; OnPropertyChanged(); } }

        bool permiteSolRespuesta = false;
        public bool PermiteSolRespuesta { get => permiteSolRespuesta; set { permiteSolRespuesta = value; OnPropertyChanged(); } }
        bool permitePrincipal = false;
        public bool PermitePrincipal { get => permitePrincipal; set { permitePrincipal = value; OnPropertyChanged(); } }

        LOD_UsuariosLod ReceptorSeleccionado;

        public LOD_UsuariosLod selectedReceptor
        {
            get => ReceptorSeleccionado;
            set
            {

                ReceptorSeleccionado = value;
                receptor = value.IdUsLod;
                OnPropertyChanged();
            }
        }


        private ObservableRangeCollection<LOD_UsuariosLod> _ListReceptores;
        public ObservableRangeCollection<LOD_UsuariosLod> ListReceptores
        {
            get { return _ListReceptores; }
            set
            {
                _ListReceptores = value;
                OnPropertyChanged("ListReceptores");
            }
        }

        public AsyncCommand CreateReceptorCommand { get; }
        public AsyncCommand GetReceptoresCommand { get; }
        public AsyncCommand BackCommand { get; }

        public AddReceptorViewModel()
        {
            CreateReceptorCommand = new AsyncCommand(CreateNewReceptor);
            GetReceptoresCommand = new AsyncCommand(GetReceptores);
            BackCommand = new AsyncCommand(GoToBack);
            ListReceptores = new ObservableRangeCollection<LOD_UsuariosLod>();
        }

        public async Task CreateNewReceptor()
        {
            IsBusy = true;
            LOD_UsuariosLod userLod = await UsuariosLodDBServices.FindUserLod(receptor);
            LOD_Anotaciones anot = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
            LOD_UserAnotacion userAnot = new LOD_UserAnotacion();
            ReceptoresView receptoresView = new ReceptoresView();
            receptoresView.IdAnotacion = IdAnotacion;
            receptoresView.EsRespRespuesta = SolicitaRespuesta;
            receptoresView.IdUsLod = userLod.IdUsLod;
            receptoresView.EsPrincipal = Principal;
            
            ConnectivityHelper helperConnection = new ConnectivityHelper();
            bool connection = helperConnection.VerifyConnection();
            if (connection)
            {
                userAnot = await InternetAnotacionServices.CreateReceptor(receptoresView);
            }
            else
            {
                userAnot = null;
            }



            if (userAnot != null)
            {
                userAnot.Actualizado = 1;
                await UserAnotacionDBServices.AddUserAnotacion(userAnot);
            }
            else
            {
                userAnot.IdUsLod = userLod.IdUsLod;
                userAnot.IdAnotacion = IdAnotacion;
                userAnot.FechaVB = null;
                userAnot.EsPrincipal = Principal;
                userAnot.EsRespRespuesta = SolicitaRespuesta;
                userAnot.RespVB = false;
                userAnot.Leido = false;
                userAnot.Destacado = false;
                userAnot.UsuarioLod = userLod.Usuario;
                userAnot.Anotacion = anot.Correlativo + " - " + anot.Titulo;
                userAnot.Actualizado = 2;
                await UserAnotacionDBServices.AddUserAnotacion(userAnot);
            }

            IsBusy = false;
            if (IsBusy == false)
            {
                var route = $"{nameof(LoadAnotacionDataPage)}?IdAnotacion={IdAnotacion}";
                await Shell.Current.GoToAsync(route);
            }
        }

        public async Task GetReceptores()
        {
            IsBusy = true;
            LOD_Anotaciones anot = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
            IdLibro = anot.IdLod;
            ApplicationUser user = await ApplicationUserDBServices.FindUser(1);
            List<LOD_UserAnotacion> users = await UserAnotacionDBServices.FindUserAnotacionByAnot(IdAnotacion);
            foreach (var item in users)
            {
                if (item.EsPrincipal)
                {
                    PermitePrincipal = false;
                }

                if (item.EsRespRespuesta)
                {
                    PermiteSolRespuesta = false;
                }
            }

            var listadoAux = await UsuariosLodDBServices.GetReceptores(IdLibro, user.UserId);
            List<LOD_UsuariosLod> listado = new List<LOD_UsuariosLod>();
            foreach (var item in listadoAux)
            {
                if(!users.Select(x => x.IdUsLod).Contains(item.IdUsLod))
                {
                    listado.Add(item);
                }
            }

            ListReceptores.AddRange(listado);
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
