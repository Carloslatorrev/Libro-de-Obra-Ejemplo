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
    [QueryProperty(nameof(IdDocAnotacion), nameof(IdDocAnotacion))]
    public class AprobarDocumentoViewModel : ViewModelBase
    {
        int idDocAnotacion { get; set; }
        public int IdDocAnotacion
        {
            get => idDocAnotacion; set
            {
                idDocAnotacion = value; OnPropertyChanged();
                SetDataCommand.ExecuteAsync();
            }
        }

        string nombreTipoDoc { get; set; }
        public string NombreTipoDoc { get => nombreTipoDoc; set { nombreTipoDoc = value; OnPropertyChanged(); } }


        public AsyncCommand SetDataCommand { get; }
        public AsyncCommand AprobarDocCommand { get; }


        public AprobarDocumentoViewModel()
        {

        }

        public async Task SetData()
        {
            IsBusy = true;
            LOD_docAnotacion docAnot = await DocAnotacionDBServices.FindDocAnotacion(IdDocAnotacion);
            MAE_TipoDocumento tipoDoc = await TipoDocumentoDBServices.FindTipoDoc(docAnot.IdTipoDoc);
            NombreTipoDoc = tipoDoc.Tipo;
            IsBusy = false;
        }

        public async Task AprobarDoc()
        {
            IsBusy = true;
            LOD_docAnotacion docAnot = await DocAnotacionDBServices.FindDocAnotacion(IdDocAnotacion);
            docAnot.EstadoDoc = 2;
            LOD_docAnotacion docAnotInet = new LOD_docAnotacion();
            ConnectivityHelper helperConnection = new ConnectivityHelper();
            bool connection = helperConnection.VerifyConnection();
            if (connection)
            {
                docAnotInet = await InternetAnotacionServices.AprobarDoc(IdDocAnotacion.ToString());
            }
            else
            {
                docAnotInet = null;
            }

            if (docAnotInet != null)
            {
                docAnotInet.Actualizado = 3;
                await DocAnotacionDBServices.UpdateDocAnotacion(docAnotInet);
            }
            else
            {
                docAnot.Actualizado = 4;
                await DocAnotacionDBServices.UpdateDocAnotacion(docAnot);
            }
            IsBusy = false;

            if (!IsBusy)
            {
                var route = $"{nameof(LoadAnotacionDataPage)}?IdAnotacion={docAnot.IdAnotacion}";
                await Shell.Current.GoToAsync(route);
            }
        }

    }
}
