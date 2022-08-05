using LODapp.Data;
using LODapp.Helpers;
using LODapp.Models;
using LODapp.Services;
using LODapp.Views;
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
    [QueryProperty(nameof(IdCompuesto), nameof(IdCompuesto))]
    class AddDocumentoViewModel : ViewModelBase
    {
        FileResult file { get; set; }
        public FileResult File { get => file; set { file = value; OnPropertyChanged(); } }

        string fileName { get; set; }
        public string FileName { get => fileName; set { fileName = value; OnPropertyChanged(); } }

        string idCompuesto { get; set; }
        public string IdCompuesto { get => idCompuesto; set { idCompuesto = value; OnPropertyChanged();
                SetDataCommand.ExecuteAsync();
            } }

        int idAnotacion { get; set; }
        public int IdAnotacion { get => idAnotacion; set { idAnotacion = value; OnPropertyChanged(); } }

        int idTipoDoc { get; set; }
        public int IdTipoDoc { get => idTipoDoc; set { idTipoDoc = value; OnPropertyChanged(); } }

        string tituloDocumento { get; set; }
        public string TituloDocumento { get => tituloDocumento; set { tituloDocumento = value; OnPropertyChanged(); } }

        string observacionDoc { get; set; }
        public string ObservacionDoc { get => observacionDoc; set { observacionDoc = value; OnPropertyChanged(); } }

        public AsyncCommand CreateDocumentoCommand { get; }
        public AsyncCommand UploadArchiveCommand { get; }
        public AsyncCommand TakePhotoCommand { get; }

        public AsyncCommand SetDataCommand { get; }
        public AsyncCommand BackCommand { get; }

        public AddDocumentoViewModel()
        {
            SetDataCommand = new AsyncCommand(SetData);
            UploadArchiveCommand = new AsyncCommand(UploadArchive);
            TakePhotoCommand = new AsyncCommand(TakePhotoAsync);
            CreateDocumentoCommand = new AsyncCommand(CreateDocumento);
            BackCommand = new AsyncCommand(GoToBack);
        }


        public async Task SetData()
        {
            var Ids = IdCompuesto.Split(';');
            IdAnotacion = Convert.ToInt32(Ids[0]);
            IdTipoDoc = Convert.ToInt32(Ids[1]);
            LOD_Anotaciones anotaciones = await AnotacionesDBServices.FindAnotacion(IdAnotacion);

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

            }catch(Exception ex)
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
                AddDocumentoView addDoc = new AddDocumentoView();
                ApplicationUser user = await ApplicationUserDBServices.FindUser(1);
                LOD_Anotaciones lOD_Anotaciones = await AnotacionesDBServices.FindAnotacion(IdAnotacion);
                LOD_LibroObras  libro = await LibroObrasDBServices.FindLibro(lOD_Anotaciones.IdLod);
                int IdContrato = libro.IdContrato;
                addDoc.IdAnotacion = lOD_Anotaciones.IdAnotacion;
                addDoc.IdContrato = IdContrato;
                addDoc.IdTipoDoc = IdTipoDoc;
                addDoc.Nombre = TituloDocumento;
                addDoc.Descripcion = ObservacionDoc;
                addDoc.UserId = user.UserId;

                LOD_docAnotacion docAnot = new LOD_docAnotacion();
                ConnectivityHelper helperConnection = new ConnectivityHelper();
                bool connection = helperConnection.VerifyConnection();
                if (connection)
                {
                    docAnot = await InternetAnotacionServices.CreateDocumento(addDoc, File);
                }
                else
                {
                    docAnot = null;
                }

                 
                if(docAnot != null)
                {
                    docAnot.Actualizado = 1;
                    docAnot.useRutaLocal = false;
                    await DocAnotacionDBServices.AddDocAnotacion(docAnot);
                }
                else
                {
                    

                    docAnot = new LOD_docAnotacion();
                    MAE_TipoDocumento tipoDoc = await TipoDocumentoDBServices.FindTipoDoc(addDoc.IdTipoDoc);
                    List<LOD_docAnotacion> listadoDocAnot = await DocAnotacionDBServices.GetDocAnotacion();
                    int lastId = listadoDocAnot.OrderByDescending(x => x.IdDocAnotacion).Select(x => x.IdDocAnotacion).FirstOrDefault() +1;
                    int lastIdDoc = listadoDocAnot.OrderByDescending(x => x.IdDoc).Select(x => x.IdDoc).FirstOrDefault() + 1;
                    bool directoryAnot = await SaveLocalData.CreateDirectoryAnotacion(IdAnotacion);
                    string rutaLocal = "";
                    if (directoryAnot)
                    {
                        rutaLocal = await SaveLocalData.CreateLocalFileAnotacion(IdAnotacion, lastId, IdTipoDoc, TituloDocumento, File);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", $"Ha ocurrido un Error al guardar el Archivo: '{TituloDocumento}' localmente, Recuerda habilitar los permisos de la aplicación de guardado de archivos o Contacte a Soporte para obtener ayuda", "OK");
                    }
                    

                    if (!String.IsNullOrEmpty(rutaLocal))
                    {
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
                        docAnot.ruta = rutaLocal;
                        docAnot.useRutaLocal = true;
                        docAnot.documento = TituloDocumento;
                        await DocAnotacionDBServices.AddDocAnotacion(docAnot);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", $"Ha ocurrido un Error al guardar el Archivo: '{TituloDocumento}' localmente, Recuerda habilitar los permisos de la aplicación de guardado de archivos o Contacte a Soporte para obtener ayuda", "OK");
                    }
                    
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
