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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LODapp.ViewModels
{
    [QueryProperty(nameof(IdAnotacion), nameof(IdAnotacion))]
    class AddOtroDocumentoViewModel : ViewModelBase
    {
        FileResult file { get; set; }
        public FileResult File { get => file; set { file = value; OnPropertyChanged(); } }

        string fileName { get; set; }
        public string FileName { get => fileName; set { fileName = value; OnPropertyChanged(); } }

        int idAnotacion { get; set; }
        public int IdAnotacion { get => idAnotacion; set { idAnotacion = value; OnPropertyChanged();
                GetClassOneCommand.ExecuteAsync();
                GetClassTwoCommand.ExecuteAsync();
                GetTipoDocCommand.ExecuteAsync();
            } }

        int idClassOne { get; set; }
        public int IdClassOne { get => idClassOne; set { idClassOne = value; OnPropertyChanged(); } }

        int idClassTwo { get; set; }
        public int IdClassTwo { get => idClassTwo; set { idClassTwo = value; OnPropertyChanged(); } }



        MAE_ClassOne classOneSeleccionado;

        public MAE_ClassOne selectedClassOne
        {
            get => classOneSeleccionado;
            set
            {
                classOneSeleccionado = value;
                IdClassOne = value.IdClassOne;
                OnPropertyChanged();
                GetClassTwoCommand.ExecuteAsync();
            }
        }

        MAE_ClassTwo maeClassTwoSeleccionado;

        public MAE_ClassTwo selectedClassTwo
        {
            get => maeClassTwoSeleccionado;
            set
            {
                maeClassTwoSeleccionado = value;
                IdClassTwo = value.IdClassTwo;
                OnPropertyChanged();
                GetTipoDocCommand.ExecuteAsync();
            }
        }

        MAE_TipoDocumento tipoDocumentoSeleccionado;

        public MAE_TipoDocumento selectedTipoDocumento
        {
            get => tipoDocumentoSeleccionado;
            set
            {
                tipoDocumentoSeleccionado = value;
                IdTipoDoc = value.IdTipo;
                OnPropertyChanged();
            }
        }


        int idTipoDoc { get; set; }
        public int IdTipoDoc { get => idTipoDoc; set { idTipoDoc = value; OnPropertyChanged(); } }

        string tituloDocumento { get; set; }
        public string TituloDocumento { get => tituloDocumento; set { tituloDocumento = value; OnPropertyChanged(); } }

        string observacionDoc { get; set; }
        public string ObservacionDoc { get => observacionDoc; set { observacionDoc = value; OnPropertyChanged(); } }

        private ObservableRangeCollection<MAE_ClassOne> _ListClassOne;
        public ObservableRangeCollection<MAE_ClassOne> ListClassOne
        {
            get { return _ListClassOne; }
            set
            {
                _ListClassOne = value;
                
                OnPropertyChanged("ListClassOne");
            }
        }

        private ObservableRangeCollection<MAE_ClassTwo> _ListClassTwo;
        public ObservableRangeCollection<MAE_ClassTwo> ListClassTwo
        {
            get { return _ListClassTwo; }
            set
            {
                _ListClassTwo = value;
                OnPropertyChanged("ListClassTwo");
            }
        }

        private ObservableRangeCollection<MAE_TipoDocumento> _ListTipoDocumento;
        public ObservableRangeCollection<MAE_TipoDocumento> ListTipoDocumento
        {
            get { return _ListTipoDocumento; }
            set
            {
                _ListTipoDocumento = value;
                OnPropertyChanged("ListTipoDocumento");
            }
        }

        public AsyncCommand CreateDocumentoCommand { get; }
        public AsyncCommand UploadArchiveCommand { get; }
        public AsyncCommand TakePhotoCommand { get; }
        public AsyncCommand SetDataCommand { get; }
        public AsyncCommand BackCommand { get; }

        public AsyncCommand GetClassTwoCommand { get; }
        public AsyncCommand GetTipoDocCommand { get; }
        public AsyncCommand GetClassOneCommand { get; }

        public AddOtroDocumentoViewModel()
        {
            SetDataCommand = new AsyncCommand(SetData);
            UploadArchiveCommand = new AsyncCommand(UploadArchive);
            CreateDocumentoCommand = new AsyncCommand(CreateDocumento);
            BackCommand = new AsyncCommand(GoToBack);
            TakePhotoCommand = new AsyncCommand(TakePhotoAsync);
            GetClassOneCommand = new AsyncCommand(GetClassOne);
            GetClassTwoCommand = new AsyncCommand(GetClassTwo);
            GetTipoDocCommand = new AsyncCommand(GetTipoDocumento);
            ListTipoDocumento = new ObservableRangeCollection<MAE_TipoDocumento>();
        }


        public async Task SetData()
        {
            LOD_Anotaciones anotaciones = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
        }

        public async Task GetClassOne()
        {
            IsBusy = true;
            ListClassOne = new ObservableRangeCollection<MAE_ClassOne>();
            var listado = await ClassOneDBServices.GetClassOne();
            ListClassOne.AddRange(listado);
            IsBusy = false;
        }

        public async Task GetClassTwo()
        {
            IsBusy = true;

            if (IdClassOne == null || IdClassOne == 0)
            {
                ListClassTwo = null;
                ListClassTwo = new ObservableRangeCollection<MAE_ClassTwo>();
                var listado = await ClassTwoDBServices.GetClassTwo();
                ListClassTwo.AddRange(listado);
            }
            else
            {
                ListClassTwo = null;
                ListClassTwo = new ObservableRangeCollection<MAE_ClassTwo>();
                var listado = await ClassTwoDBServices.GetClassTwo(IdClassOne);
                ListClassTwo.AddRange(listado);

            }

            IsBusy = false;
        }

        public async Task GetTipoDocumento()
        {
            IsBusy = true;

            if (IdClassTwo == null || IdClassTwo == 0)
            {
                ListTipoDocumento = null;
                ListTipoDocumento = new ObservableRangeCollection<MAE_TipoDocumento>();
                var listado = await TipoDocumentoDBServices.GetTipoDoc();
                ListTipoDocumento.AddRange(listado);
            }
            else
            {
                List<MAE_ClassDoc> listadoClassDoc = await ClassDocDBServices.GetClassDoc(IdClassTwo);
                ListTipoDocumento = null;
                ListTipoDocumento = new ObservableRangeCollection<MAE_TipoDocumento>();
                List<MAE_TipoDocumento> listado = new List<MAE_TipoDocumento>();
                foreach (var item in listadoClassDoc)
                {
                    MAE_TipoDocumento tipoAux = await TipoDocumentoDBServices.FindTipoDoc(item.IdTipo);
                    listado.Add(tipoAux);
                }

                ListTipoDocumento.AddRange(listado);
            }

            IsBusy = false;
        }

        public async Task UploadArchive()
        {
            IsBusy = true;
            try
            {
                File = await FilePicker.PickAsync(
                    new PickOptions
                    {
                        FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                        {
                            {DevicePlatform.Android, new[]{"application/pdf", "image/jpeg" , "image/gif", "video/x-msvideo" ,
                                "video/x-msvideo" ,"video/jpeg","video/mp4","video/webm", "image/png","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                            ,"application/vnd.openxmlformats-officedocument.wordprocessingml.document", "text/plain"}  }

                        })
                    }
                );

                FileName = File.FileName;

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "OK");
            }

            IsBusy = false;

        }

        public async Task TakePhotoAsync()
        {
            IsBusy = true;
            try
            {
                try
                {
                    File = await MediaPicker.CapturePhotoAsync();
                    if (String.IsNullOrEmpty(File.FileName))
                    {
                        Random generator = new Random();
                        string num = generator.Next(1, 99999).ToString();
                        File.FileName = $"IMG_LODAPR_{num}";
                    }
                    FileName = File.FileName;
                    Console.WriteLine($"CapturePhotoAsync COMPLETED:");
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"{fnsEx.Message}", "OK");
                }
                catch (PermissionException pEx)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"{pEx.Message}", "OK");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "OK");
            }

            IsBusy = false;

        }

        public async Task CreateDocumento()
        {
            IsBusy = true;
            try
            {
                AddOtroDocumentoView addDoc = new AddOtroDocumentoView();
                ApplicationUser user = await ApplicationUserDBServices.FindUser(1);
                LOD_Anotaciones lOD_Anotaciones = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
                LOD_LibroObras libro = await LibroObrasDBServices.FindLibro(lOD_Anotaciones.IdLod);
                int IdContrato = libro.IdContrato;
                addDoc.IdAnotacion = lOD_Anotaciones.IdAnotacion;
                addDoc.IdContrato = IdContrato;
                addDoc.IdTipoDoc = IdTipoDoc;
                addDoc.Nombre = TituloDocumento;
                addDoc.Descripcion = ObservacionDoc;
                addDoc.UserId = user.UserId;
                addDoc.IdClassTwo = IdClassTwo;

                LOD_docAnotacion docAnot = new LOD_docAnotacion();
                ConnectivityHelper helperConnection = new ConnectivityHelper();
                bool connection = helperConnection.VerifyConnection();
                if (connection)
                {
                    docAnot = await InternetAnotacionServices.CreateOtroDocumento(addDoc, File);
                }
                else
                {
                    docAnot = null;
                }


                if (docAnot != null)
                {
                    docAnot.Actualizado = 1;
                    await DocAnotacionDBServices.AddDocAnotacion(docAnot);
                }
                else
                {
                    docAnot = new LOD_docAnotacion();
                    MAE_TipoDocumento tipoDoc = await TipoDocumentoDBServices.FindTipoDoc(addDoc.IdTipoDoc);
                    List<LOD_docAnotacion> listadoDocAnot = await DocAnotacionDBServices.GetDocAnotacion();
                    int lastId = listadoDocAnot.OrderByDescending(x => x.IdDocAnotacion).Select(x => x.IdDocAnotacion).FirstOrDefault() + 1;
                    int lastIdDoc = listadoDocAnot.OrderByDescending(x => x.IdDoc).Select(x => x.IdDoc).FirstOrDefault() + 1;
                    docAnot.IdDocAnotacion = lastId;
                    docAnot.IdAnotacion = addDoc.IdAnotacion;
                    docAnot.IdTipoDoc = addDoc.IdTipoDoc;
                    docAnot.IdContrato = addDoc.IdContrato;
                    docAnot.IdDoc = lastIdDoc;
                    docAnot.Anotacion = lOD_Anotaciones.Correlativo + " - " + lOD_Anotaciones.Titulo;
                    docAnot.TipoDocumento = tipoDoc.Tipo;
                    docAnot.IdUserEvento = user.Nombres + " " + user.Apellidos;
                    docAnot.Observaciones = addDoc.Descripcion;
                    docAnot.FechaEvento = DateTime.Now;
                    docAnot.EstadoDoc = 1;
                    docAnot.Actualizado = 2;
                    await DocAnotacionDBServices.AddDocAnotacion(docAnot);
                }


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "OK");
            }

            IsBusy = false;
            if (IsBusy == false)
            {
                var route = $"{nameof(LoadAnotacionDataPage)}?IdAnotacion={IdAnotacion}";
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

