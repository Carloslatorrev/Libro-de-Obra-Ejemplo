using Java.Net;
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
using Command = MvvmHelpers.Commands.Command;

namespace LODapp.ViewModels
{

    public class ListContratoViewModel : ViewModelBase
    {
        public string Title { get; set; }
        public bool showListLod = false;
        public bool ShowListLod
        {
            get => showListLod;
            set
            {
                showListLod = value;
                OnPropertyChanged("ShowListLod");
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

        private ObservableRangeCollection<CON_Contratos> _Contratos { get; set; }
        public ObservableRangeCollection<CON_Contratos> Contratos
        {
            get { return _Contratos; }
            set
            {
                _Contratos = value;
                OnPropertyChanged("Contratos");
            }
        }

        public ObservableRangeCollection<LOD_LibroObras> LibroObras { get; set; }
        public ObservableRangeCollection<Grouping<string, CON_Contratos>> ContratoGroups { get; set; }

        public AsyncCommand<string> BuscarCommand { get; }
        public AsyncCommand ListCommand { get; }
        public AsyncCommand EmptyCommand { get; }
        public AsyncCommand<int> LongPressedCommand { get; }
        public AsyncCommand<int> DetailsCommand { get; }


        public ICommand SearchCommand => new AsyncCommand<string>(async (string query) => await Buscar(query));


        //public AsyncCommand SearchCommand { get; }

        public AsyncCommand DelayLoadMoreCommand { get; set; }
        //public AsyncCommand<CON_Contratos> FavoriteCommand { get; }

        public ListContratoViewModel()
        {
            Title = "Listado de Contratos";
            Contratos = new ObservableRangeCollection<CON_Contratos>();
            LibroObras = new ObservableRangeCollection<LOD_LibroObras>();
            ContratoGroups = new ObservableRangeCollection<Grouping<string, CON_Contratos>>();
            BuscarCommand = new AsyncCommand<string>(Buscar);
            //FavoriteCommand = new AsyncCommand<Contrato>(Favorite);
            ListCommand = new AsyncCommand(Listar);
            DelayLoadMoreCommand = new AsyncCommand(DelayLoadMoreAsync);
            LongPressedCommand = new AsyncCommand<int>(LongPressed);
            DetailsCommand = new AsyncCommand<int>(GoToDetails);
            ListCommand.ExecuteAsync();
            //SearchCommand = new AsyncCommand(Buscar());
        }

        //async Task Favorite(CON_Contratos contrato)
        //{
        //    if (contrato == null)
        //        return;

        //    await Application.Current.MainPage.DisplayAlert("Favorito", contrato.Nombre, "OK");
        //}


        CON_Contratos contratoPrevio;
        CON_Contratos contratoSeleccionado;

        public CON_Contratos selectedContrato
        {
            get => contratoSeleccionado;
            set
            {
                if (value != null)
                {
                    //Application.Current.MainPage.DisplayAlert("Seleccionado", value.NombreContrato, "OK");
                    value = null;
                }

                contratoSeleccionado = value;
                OnPropertyChanged();
            }
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

        async Task GoToDetails(int args)
        {
            string aux = args.ToString();
            var route = $"{nameof(LoadContratoDataPage)}?IdContrato={aux}";
            await Shell.Current.GoToAsync(route);
        }


        async Task LongPressed(int args) 
        {
            contratoSeleccionado = await ContratosDBServices.FindContrato(args);
            var librosInet = await InternetLibroObrasServices.GetSelectLibroObras(args);
            foreach (var item in librosInet)
            {
                if (await LibroObrasDBServices.FindLibro(item.IdLod) == null)
                    await LibroObrasDBServices.AddLibroObras(item);
            }

            var librosDB = await LibroObrasDBServices.FindByContratoLibro(args);

            if (LibroObras.Count() > 0)
            {
                foreach (var item in librosDB)
                {
                    if (!LibroObras.Contains(item))
                    {
                        LibroObras.Add(item);
                    }
                }
            }
            else
            {
                LibroObras.AddRange(librosDB);
            }

           
            //contratoSeleccionado.LibroObras = libros.ToList();
            contratoSeleccionado.ShowListLod = true;
            await Listar();
            LibroObras.AddRange(librosInet);
            //ShowListLod = true;

            //OnPropertyChanged();
        }

        async Task Listar()
        {
            IsBusy = true;
            //await Task.Delay(2000);

            var contratosDB = await ContratosDBServices.GetContratos();
            ConnectivityHelper helperConnection = new ConnectivityHelper();
            bool connection = helperConnection.VerifyConnection();
            //var contratosDB = await ContratoServices.GetContrato();
            if (Contratos.Count() > 0)
            {
                foreach (var item in contratosDB)
                {
                    if (!Contratos.Select(x => x.IdContrato).Contains(item.IdContrato))
                    {
                        Contratos.Add(item);
                    }                   
                }

                foreach (var item in Contratos)
                {
                    if (contratoSeleccionado != null )
                    {
                        if(contratoSeleccionado.IdContrato.Equals(item.IdContrato))
                            item.ShowListLod = true;
                    }
                    else
                    {
                        item.ShowListLod = false;
                    }
                    
                    if (connection)
                    {
                        if (String.IsNullOrEmpty(item.RutaImagenContrato))
                        {
                            item.UsarLocal = true;
                            item.UsarRuta = false;
                        }
                        else
                        {
                            item.UsarLocal = false;
                            item.UsarRuta = true;
                            string rutaLimpia = item.RutaImagenContrato.Replace("~/", "");
                            item.rutaCompleta = InternetServices.UrlPlataforma + rutaLimpia;
                        }
                    }
                    else
                    {
                        item.UsarLocal = true;
                        item.UsarRuta = false;
                    }
                    
                }
            }
            else
            {
                Contratos.AddRange(contratosDB);
                if(Contratos.Count > 0)
                {
                    foreach (var item in Contratos)
                    {
                        if(contratoSeleccionado != null && contratoSeleccionado.IdContrato.Equals(item.IdContrato))
                        {
                            item.ShowListLod = true;
                        }
                        else
                        {
                            item.ShowListLod = false;
                        }

                        if (connection)
                        {
                            if (String.IsNullOrEmpty(item.RutaImagenContrato))
                            {
                                item.UsarLocal = true;
                                item.UsarRuta = false;
                            }
                            else
                            {
                                item.UsarLocal = false;
                                item.UsarRuta = true;
                                string rutaLimpia = item.RutaImagenContrato.Replace("~/", "");
                                item.rutaCompleta = InternetServices.UrlPlataforma + rutaLimpia;
                            }
                        }
                        else
                        {
                            item.UsarLocal = true;
                            item.UsarRuta = false;
                        }
                    }
                }
            }

           

            IsBusy = false;
        }

        async Task DelayLoadMoreAsync()
        {
            if(Contratos.Count <= 10)
            {
                return;
            }

            await Listar();
        }

        async Task Buscar(string query)
        {
            if(String.IsNullOrEmpty(query))
            {
                var contratos = await ContratosDBServices.GetContratos();
                Contratos = new ObservableRangeCollection<CON_Contratos>();
                Contratos.AddRange(contratos);
            }
            else
            {
                var contratos = await ContratosDBServices.SearchContrato(query);
                Contratos = new ObservableRangeCollection<CON_Contratos>();
                Contratos.AddRange(contratos);
            }
            
        }


    }
}
