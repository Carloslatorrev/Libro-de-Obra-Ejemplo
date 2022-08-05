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
    [QueryProperty(nameof(IdAnotacion), nameof(IdAnotacion))]
    class AnotacionDataViewModel : ViewModelBase
    {
        string tituloAnotacion { get; set; }
        public string TituloAnotacion { get => tituloAnotacion; set { tituloAnotacion = value; OnPropertyChanged(); } }

        string libroObras { get; set; }
        public string LibroObras { get => libroObras; set { libroObras = value; OnPropertyChanged(); } }

        string rutaVistaPrevia { get; set; }
        public string RutaVistaPrevia { get => rutaVistaPrevia; set { rutaVistaPrevia = value; OnPropertyChanged(); } }

        int idAnotacion { get; set; }
        public int IdAnotacion { get => idAnotacion; set { idAnotacion = value; OnPropertyChanged();
                GetDataCommand.ExecuteAsync();
            
            } }


        bool estadoFirma = false;
        public bool EstadoFirma { get => estadoFirma; set { estadoFirma = value; OnPropertyChanged(); } }

        bool borrador = false;
        public bool Borrador { get => borrador; set { borrador = value; OnPropertyChanged(); } }

        bool publicada = false;
        public bool Publicada { get => publicada; set { publicada = value; OnPropertyChanged(); } }

        bool pendFirma = false;
        public bool PendFirma { get => pendFirma; set { pendFirma = value; OnPropertyChanged(); } }

        public string Title { get; set; }

        bool destacadoOn = false;
        public bool DestacadoOn { get => destacadoOn; set { destacadoOn = value; OnPropertyChanged(); } }
        bool destacadoOff = false;
        public bool DestacadoOff { get => destacadoOff; set { destacadoOff = value; OnPropertyChanged(); } }

        bool showListLod = false;
        public bool ShowListLod { get => showListLod; set { showListLod = value; OnPropertyChanged(); } }

        bool showListRef = false;
        public bool ShowListRef { get => showListRef; set { showListRef = value; OnPropertyChanged(); } }

        bool showTextRef = false;
        public bool ShowTextRef { get => showTextRef; set { showTextRef = value; OnPropertyChanged(); } }

        bool showVistoBueno = false;
        public bool ShowVistoBueno { get => showVistoBueno; set { showVistoBueno = value; OnPropertyChanged(); } }

        bool showTextVistoBueno = false;
        public bool ShowTextVistoBueno { get => showTextVistoBueno; set { showTextVistoBueno = value; OnPropertyChanged(); } }

        bool showDocumentos = false;
        public bool ShowDocumentos { get => showDocumentos; set { showDocumentos = value; OnPropertyChanged(); } }

        bool showTextDocumentos = false;
        public bool ShowTextDocumentos { get => showTextDocumentos; set { showTextDocumentos = value; OnPropertyChanged(); } }

        bool showReceptores = false;
        public bool ShowReceptores { get => showReceptores; set { showReceptores = value; OnPropertyChanged(); } }

        bool showTextReceptores = false;
        public bool ShowTextReceptores { get => showTextReceptores; set { showTextReceptores = value; OnPropertyChanged(); } }


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

        public ObservableRangeCollection<LOD_ReferenciasAnot> ReferenciasAnot { get; set; }
        public ObservableRangeCollection<LOD_UserAnotacion> Receptores { get; set; }
        public ObservableRangeCollection<LOD_UserAnotacion> VistosBuenos { get; set; }
        public ObservableRangeCollection<LOD_docAnotacion> Documentos { get; set; }
        public ObservableRangeCollection<MAE_TipoDocumento> TipoDocumentos { get; set; }
        public ObservableRangeCollection<LOD_log> Logs { get; set; }

        public AsyncCommand<int> AddDocumentoCommand { get; }
        public AsyncCommand AddOtroDocumentoCommand { get; }
        public AsyncCommand AddReceptorCommand { get; }
        public AsyncCommand AddReferenciaCommand { get; }
        public AsyncCommand EliminarCommand { get; }
        public AsyncCommand DarVBCommand { get; }
        public AsyncCommand FirmarCommand { get; }
        public AsyncCommand SolicitarFirmaCommand { get; }
        public AsyncCommand BuscarCommand { get; }
        public AsyncCommand EditCommand { get; }
        public AsyncCommand SetDestacadaCommand { get; }
        public AsyncCommand SetNotDestacadaCommand { get; }
        public AsyncCommand<int> GoToReferenceCommand { get; }
        public AsyncCommand ListCommand { get; }
        //public AsyncCommand<int> LongPressedCommand { get; }
        public AsyncCommand<int> DetailsCommand { get; }

        public AsyncCommand BackCommand { get; }
        public AsyncCommand GetDataCommand { get; }
        public AsyncCommand GetReferenciasInetCommand { get; }
        public AsyncCommand GetReferenciasDBCommand { get; }
        public AsyncCommand GetReceptoresInetCommand { get; }
        public AsyncCommand GetReceptoresDBCommand { get; }
        public AsyncCommand GetUserCommand { get; }

        public AsyncCommand<int> EliminarReferenciaCommand { get; }
        public AsyncCommand<int> EliminarReceptorCommand { get; }
        public AsyncCommand<int> EliminarDocumentoCommand { get; }
        public AsyncCommand<int> EditarReceptorCommand { get; }
        public AsyncCommand<string> DescargarCommand { get; }
        public AsyncCommand VistaPreviaCommand { get; }
        //public ICommand SearchCommand => new AsyncCommand<string>(async (string query) => await Buscar(query));

        public Command DelayLoadMoreUserCommand { get; set; }
        

        public AnotacionDataViewModel()
        {
            //IdContrato = id;
            Title = "Detalle de Anotación";
            Anotacion = new LOD_Anotaciones();
            ReferenciasAnot = new ObservableRangeCollection<LOD_ReferenciasAnot>();
            Receptores = new ObservableRangeCollection<LOD_UserAnotacion>();
            Documentos = new ObservableRangeCollection<LOD_docAnotacion>();
            TipoDocumentos = new ObservableRangeCollection<MAE_TipoDocumento>();
            VistosBuenos = new ObservableRangeCollection<LOD_UserAnotacion>();
            Logs = new ObservableRangeCollection<LOD_log>();
            GetDataCommand = new AsyncCommand(GetData);
            EditCommand = new AsyncCommand(GoToEdit);
            SetDestacadaCommand = new AsyncCommand(SetDestacada);
            SetNotDestacadaCommand = new AsyncCommand(SetNotDestacada);
            AddReferenciaCommand = new AsyncCommand(GoToAddReferencia);
            AddReceptorCommand = new AsyncCommand(GoToAddReceptor);
            AddDocumentoCommand = new AsyncCommand<int>(GoToAddDocumento);
            AddOtroDocumentoCommand = new AsyncCommand(GoToAddOtroDocumento);
            FirmarCommand = new AsyncCommand(GoToFirmar);
            EliminarCommand = new AsyncCommand(GoToEliminar);
            SolicitarFirmaCommand = new AsyncCommand(GoToSolicitarFirma);
            GoToReferenceCommand = new AsyncCommand<int>(GoToReferencia);
            DescargarCommand = new AsyncCommand<string>(Descargar);
            VistaPreviaCommand = new AsyncCommand(GoToVistaPrevia);
            DarVBCommand = new AsyncCommand(GoToVBConfirmed);
            //DelayLoadMoreUserCommand = new Command(DelayLoadMoreUsuarios);
            BackCommand = new AsyncCommand(GoToBack);
            EliminarDocumentoCommand = new AsyncCommand<int>(GoToEliminarDocumento);
            EliminarReferenciaCommand = new AsyncCommand<int>(GoToEliminarReferencia);
            EliminarReceptorCommand = new AsyncCommand<int>(GoToEliminarReceptor);
            EditarReceptorCommand = new AsyncCommand<int>(GoToEditarReceptor);
        }


        public async Task GetData()
        {
            IsBusy = true;
            Anotacion = await AnotacionesDBServices.FindAnotacion(Convert.ToInt32(IdAnotacion));
            LOD_LibroObras lod = await LibroObrasDBServices.FindLibro(Anotacion.IdLod);
            LibroObras = lod.NombreLibroObra;
            RutaVistaPrevia = InternetServices.UrlPlataforma + "Report/VistaPreviaAnotacion/" + Anotacion.IdAnotacion;


            if(Anotacion.EstadoFirma == true)
            {
                EstadoFirma = false;
                Anotacion.Borrador = false;
                Anotacion.Publicada = true;
                Anotacion.PendienteFirma = false;
                Borrador = false;
                Publicada = true;
                PendFirma = false;
            }
            else
            {
                EstadoFirma = true;
                Anotacion.Borrador = true;
                Publicada = false;
                Anotacion.Publicada = false;
                if (String.IsNullOrEmpty(Anotacion.UserId))
                {
                    Borrador = true;
                    Anotacion.PendienteFirma = false;
                    PendFirma = false;
                }
                else
                {
                    Borrador = false;
                    Anotacion.PendienteFirma = true;
                    PendFirma = true;
                }

            }

            ApplicationUser user = await ApplicationUserDBServices.FindUser(1);
            LOD_UsuariosLod userLod = await UsuariosLodDBServices.FindIdByUser(Anotacion.IdLod, user.UserId);
            LOD_UserAnotacion userAnot = await UserAnotacionDBServices.FindUserAnotacion(userLod.IdUsLod, IdAnotacion);
            bool permiteEdicion = userAnot.EsPrincipal;
            ShowVistoBueno = false;
            if (Anotacion.EstadoFirma)
            {
                if (userAnot.EsPrincipal)
                {
                    if (userAnot.VistoBueno == false)
                    {
                        ShowVistoBueno = true;
                    }
                    else
                    {
                        ShowVistoBueno = false;
                    }
                }
            }
            

            List<LOD_UserAnotacion> vistosBuenos = await UserAnotacionDBServices.FindUserAnotacionByAnot(Anotacion.IdAnotacion);
            foreach (var item in vistosBuenos)
            {
                if (item.RespVB && item.VistoBueno != false && item.FechaVB != null)
                {
                    VistosBuenos.Add(item);
                }
            }


            if(userAnot.Destacado == true)
            {
                DestacadoOn = true;
                DestacadoOff = false;
            }
            else
            {
                DestacadoOn = false;
                DestacadoOff = true;
            }

            var controles = await CodSubComDBServices.FindCodSubComBySubtipo(Anotacion.IdTipoSub);
            if(controles.Count > 0)
            {
                ShowDocumentos = true;
                ShowTextDocumentos = false;
            }
            else
            {
                ShowDocumentos = false;
                ShowTextDocumentos = true;
            }
            List<int> listadoIdDocReq = new List<int>();
            foreach (var item in controles)
            {
                listadoIdDocReq.Add(item.IdTipo);
            }

            List<MAE_TipoDocumento> listTipoDocRequeridos = new List<MAE_TipoDocumento>();
            foreach (var item in listadoIdDocReq)
            {
                listTipoDocRequeridos.Add(await TipoDocumentoDBServices.FindTipoDoc(item));
            }

            var listDocumentos = await DocAnotacionDBServices.FindDocAnotacionByAnotacion(IdAnotacion);
            if (listDocumentos != null && listDocumentos.Count > 0)
            {
                //if (this.IdTipoDoc == 19 && this.IdEstado != 0)
                //{
                //    estado = "Cargado";
                //}
                //else if (this.IdEstado == 0)
                //{
                //    estado = "Pendiente";
                //}
                //else if (this.IdEstado == 1)
                //{
                //    estado = "Pend. Aprobación";
                //}
                //else if (this.IdEstado == 2)
                //{
                //    estado = "Aprobado";
                //}
                //else if (this.IdEstado == 3)
                //{
                //    estado = "Rechazado";
                //}
                ShowTextDocumentos = false;
                ShowDocumentos = true;
                List<int> IdCargados = listDocumentos.Select(x => x.IdTipoDoc).ToList();
                
                foreach (var item in listTipoDocRequeridos)
                {
                    if (!IdCargados.Contains(item.IdTipo))
                    {
                        item.Cargado = false;
                        item.SinCargar = true;
                        item.Rechazado = false;
                        item.Aprobado = false;
                        item.PendAprobacion = false;
                        item.PermiteBorrar = false;
                        item.PermiteDescargar = false;
                        item.PermiteEdicion = false;
                    }
                    else
                    {
                        int estadoDoc = listDocumentos.Where(x => IdCargados.Contains(x.IdTipoDoc)).Select(x => x.EstadoDoc).FirstOrDefault();
                        int tipoDoc = listDocumentos.Where(x => IdCargados.Contains(x.IdTipoDoc)).Select(x => x.IdTipoDoc).FirstOrDefault();
                        int idDoc = listDocumentos.Where(x => IdCargados.Contains(x.IdTipoDoc)).Select(x => x.IdDoc).FirstOrDefault();
                        item.Ruta = InternetServices.UrlPlataformaDescargar+listDocumentos.Where(x => IdCargados.Contains(x.IdTipoDoc)).Select(x => x.IdDocAnotacion).FirstOrDefault();
                       
                        int IdDocAnot = listDocumentos.Where(x => IdCargados.Contains(x.IdTipoDoc)).Select(x => x.IdDocAnotacion).FirstOrDefault();
                        item.IdDocAnotacion = IdDocAnot;
                        if (Anotacion.EstadoFirma == true)
                        {
                            if(estadoDoc == 2)
                            {
                                item.Cargado = false;
                                item.SinCargar = false;
                                item.Rechazado = false;
                                item.Aprobado = true;
                                item.PendAprobacion = false;
                                item.PermiteBorrar = false;
                                item.PermiteDescargar = true;
                                item.PermiteEdicion = false;
                            }
                            else if(estadoDoc == 3)
                            {
                                item.Cargado = false;
                                item.SinCargar = false;
                                item.Rechazado = true;
                                item.Aprobado = false;
                                item.PendAprobacion = false;
                                item.PermiteBorrar = false;
                                item.PermiteDescargar = true;
                                item.PermiteEdicion = false;
                            }
                            else
                            {
                                item.Cargado = false;
                                item.SinCargar = false;
                                item.Rechazado = false;
                                item.Aprobado = true;
                                item.PendAprobacion = false;
                                item.PermiteBorrar = false;
                                item.PermiteDescargar = true;
                                item.PermiteEdicion = false;
                            }
                        }
                        else if (tipoDoc == 19 && estadoDoc != 0)
                        {
                            item.Cargado = true;
                            item.SinCargar = false;
                            item.Rechazado = false;
                            item.Aprobado = false;
                            item.PendAprobacion = false;
                            item.PermiteBorrar = false;
                            item.PermiteDescargar = true;
                            item.PermiteEdicion = false;
                        }else if (estadoDoc == 0)
                        {
                            item.Cargado = false;
                            item.SinCargar = true;
                            item.Rechazado = false;
                            item.Aprobado = false;
                            item.PendAprobacion = false;
                            item.PermiteBorrar = false;
                            item.PermiteDescargar = false;
                            item.PermiteEdicion = false;
                        }
                        else if (estadoDoc == 1)
                        {
                            item.Cargado = false;
                            item.SinCargar = false;
                            item.Rechazado = false;
                            item.Aprobado = false;
                            item.PendAprobacion = true;
                            item.PermiteBorrar = true;
                            item.PermiteDescargar = true;
                            item.PermiteEdicion = permiteEdicion;
                        }
                        else if (estadoDoc == 2)
                        {
                            item.Cargado = false;
                            item.SinCargar = false;
                            item.Rechazado = false;
                            item.Aprobado = true;
                            item.PendAprobacion = false;
                            item.PermiteBorrar = false;
                            item.PermiteDescargar = true;
                            item.PermiteEdicion = false;
                        }
                        else if (estadoDoc == 3)
                        {
                            item.Cargado = false;
                            item.SinCargar = false;
                            item.Rechazado = true;
                            item.Aprobado = false;
                            item.PendAprobacion = false;
                            item.PermiteBorrar = false;
                            item.PermiteDescargar = true;
                            item.PermiteEdicion = false;
                        }
                    }


                }

                TipoDocumentos.AddRange(listTipoDocRequeridos);
                foreach (var idcargado in IdCargados)
                {
                    if (!listadoIdDocReq.Contains(idcargado))
                    {
                        MAE_TipoDocumento otroDocCargado = await TipoDocumentoDBServices.FindTipoDoc(idcargado);
                        otroDocCargado.Cargado = false;
                        otroDocCargado.SinCargar = false;
                        otroDocCargado.Rechazado = false;
                        otroDocCargado.Aprobado = true;
                        otroDocCargado.PendAprobacion = false;
                        otroDocCargado.PermiteBorrar = false;
                        otroDocCargado.PermiteDescargar = true;
                        TipoDocumentos.Add(otroDocCargado);
                    }
                }
                
            }
            else
            {
                foreach (var doc in listTipoDocRequeridos)
                {
                    doc.Cargado = false;
                    doc.SinCargar = true;
                    doc.Rechazado = false;
                    doc.Aprobado = false;
                    doc.PendAprobacion = false;
                    doc.PermiteBorrar = false;
                    doc.PermiteDescargar = false;
                    doc.PermiteEdicion = false;
                }

                TipoDocumentos.AddRange(listTipoDocRequeridos);
            }

            foreach (var item in TipoDocumentos)
            {
                if(item.Cargado == true)
                {

                }
            }

            //Receptores
            var listReceptores = await UserAnotacionDBServices.FindReceptoresByAnot(IdAnotacion);
            if (listReceptores != null && listReceptores.Count > 0)
            {
                Receptores.AddRange(listReceptores);
                foreach (var item in Receptores)
                {
                    LOD_UsuariosLod userLod2 = await UsuariosLodDBServices.FindUserLod(item.IdUsLod);
                    if (userLod2 == null)
                    {
                        userLod2 = await InternetUsuariosLodServices.GetUserLod(item.IdUsLod.ToString());
                        if (userLod2 != null) { await UsuariosLodDBServices.AddUserLod(userLod2); }
                    }

                    if (userLod2 != null)
                    {
                        item.UserLod = userLod2;
                    }
                }

                ShowReceptores = true;
                ShowTextReceptores = false;
            }
            else
            {
                ShowReceptores = false;
                ShowTextReceptores = true;
            }

            //Referencias
            if (anotacion != null)
            {
                var listAux = await ReferenciasAnotDBServices.FindReferenciaAnotByAnotacion(Anotacion.IdAnotacion);
                if (listAux.Count > 0) { 
                    ReferenciasAnot.AddRange(listAux);
                    ShowListRef = true;
                    ShowTextRef = false;

                }
                else
                {
                    ShowListRef = false;
                    ShowTextRef = true;
                }
                foreach (var item in ReferenciasAnot)
                {
                    LOD_Anotaciones anotRef = await AnotacionesDBServices.FindAnotacion(item.IdAnontacionRef);
                    if (anotRef == null)
                    {
                        anotRef = await InternetAnotacionServices.GetAnotacion(item.IdAnontacionRef.ToString());
                        if (anotRef != null) { await AnotacionesDBServices.AddAnotacion(anotRef); }
                    }

                    if (anotRef != null)
                        item.AnotacionRef = anotRef;
                }
            }



            //Logs
            var listLogs = await LogDBServices.FindLogByObjeto(IdAnotacion);
            if (listLogs != null && listLogs.Count > 0)
            {
                if(listLogs.Count < 5)
                {
                    for (int i = 0; i < listLogs.Count; i++)
                    {
                        Logs.Add(listLogs[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Logs.Add(listLogs[i]);
                    }
                }
                
            }

            IsBusy = false;
        }


        async Task GoToEdit()
        {
            try
            {
                string aux = IdAnotacion.ToString();
                var route = $"{nameof(EditarAnotacionPage)}?IdAnotacion={IdAnotacion}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task GoToEliminar()
        {
            try
            {
                string aux = IdAnotacion.ToString();
                var route = $"{nameof(EliminarAnotacionPage)}?IdAnotacion={aux}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task GoToEliminarDocumento(int args)
        {
            try
            {
                string aux = args.ToString();
                var route = $"{nameof(EliminarDocPage)}?IdDocAnotacion={aux}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task GoToEliminarReferencia(int args)
        {
            try
            {
                string aux = args.ToString();
                var route = $"{nameof(EliminarReferenciaPage)}?IdRefAnot={aux}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task GoToEliminarReceptor(int args)
        {
            try
            {
                string aux = args.ToString();
                var route = $"{nameof(EliminarReceptorPage)}?IdUserLod={aux}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task GoToEditarReceptor(int args)
        {
            try
            {
                string aux = args.ToString();
                var route = $"{nameof(EditReceptorPage)}?IdUserAnotacion={aux}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task GoToFirmar()
        {
            try
            {
                string aux = IdAnotacion.ToString();
                var route = $"{nameof(FirmarAnotacionPage)}?IdAnotacion={aux}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task GoToSolicitarFirma()
        {
            try
            {
                string aux = IdAnotacion.ToString();
                var route = $"{nameof(SolicitarFirmaAnotacionPage)}?IdAnotacion={aux}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task GoToAddReceptor()
        {
            try
            {
                string aux = IdAnotacion.ToString();
                var route = $"{nameof(AddReceptorPage)}?IdAnotacion={aux}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        async Task GoToAddReferencia()
        {
            try
            {
                string aux = IdAnotacion.ToString();
                var route = $"{nameof(LoadReferenciaDataPage)}?IdAnotacion={aux}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task GoToReferencia(int args)
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

        async Task GoToVBConfirmed()
        {
            try
            {
                string aux = IdAnotacion.ToString();
                var route = $"{nameof(VBConfirmedPage)}?IdAnotacion={aux}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task GoToAddDocumento(int args)
        {
            try
            {
                string aux = IdAnotacion.ToString();
                string aux2 = args.ToString();
                string idCompuesto = $"{aux};{aux2}";
                
                var route = $"{nameof(AddDocumentoPage)}?IdCompuesto={idCompuesto}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task GoToAddOtroDocumento()
        {
            try
            {
                string idCompuesto = IdAnotacion.ToString();

                var route = $"{nameof(AddOtroDocumentoPage)}?IdAnotacion={idCompuesto}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task SetDestacada()
        {
            try
            {
                LOD_Anotaciones anot = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
                ApplicationUser user = await ApplicationUserDBServices.FindUser(1);
                LOD_UsuariosLod userLod = await UsuariosLodDBServices.FindIdByUser(anot.IdLod, user.UserId);
                LOD_UserAnotacion userAnot = await UserAnotacionDBServices.FindUserAnotacion(userLod.IdUsLod, IdAnotacion);
                userAnot.Destacado = true;
                userAnot.Actualizado = 4;
                await UserAnotacionDBServices.UpdateUserAnotacion(userAnot);
                DestacadoOn = true;
                DestacadoOff = false;
                await Application.Current.MainPage.DisplayAlert("Destacado", "Se ha destacado la Anotación", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task SetNotDestacada()
        {
            try
            {
                LOD_Anotaciones anot = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
                ApplicationUser user = await ApplicationUserDBServices.FindUser(1);
                LOD_UsuariosLod userLod = await UsuariosLodDBServices.FindIdByUser(anot.IdLod, user.UserId);
                LOD_UserAnotacion userAnot = await UserAnotacionDBServices.FindUserAnotacion(userLod.IdUsLod, IdAnotacion);
                userAnot.Destacado = false;
                userAnot.Actualizado = 4;
                await UserAnotacionDBServices.UpdateUserAnotacion(userAnot);
                DestacadoOn = false;
                DestacadoOff = true;
                await Application.Current.MainPage.DisplayAlert("No Destacado", "Se ha removido estado de Destacado a la Anotación", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task GoToDarVB()
        {
            try
            {
                var route = $"{nameof(VBConfirmedPage)}?IdAnotacion={Anotacion.IdAnotacion}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task GoToBack()
        {
            try
            {
                var route = $"{nameof(LoadLibroDataPage)}?IdLibro={Anotacion.IdLod}";
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task Descargar(string ruta)
        {
            try
            {
                string uri = ruta;
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async Task GoToVistaPrevia()
        {
            try
            {
                string uri = RutaVistaPrevia;
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

