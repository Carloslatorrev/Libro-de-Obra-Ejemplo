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
using Xamarin.Forms;

namespace LODapp.ViewModels
{
    class LoadDataView : ViewModelBase
    {
        public AsyncCommand CallDataCommand { get; set; }

        public LoadDataView()
        {
            CallDataCommand = new AsyncCommand(CallData);
            CallDataCommand.ExecuteAsync();

        }

        async Task CallData()
        {
            IsBusy = true;
            bool directory = await SaveLocalData.CreateDirectoryApp();

            var user = await ApplicationUserDBServices.FindUser(1);
            bool continueCon = true;
            ConnectivityHelper helperConnection = new ConnectivityHelper();
            bool connection = helperConnection.VerifyConnection();
            if (connection)
            {

                var contratosINET = await InternetContratoServices.GetSelectContrato(user.UserId);
                if(contratosINET != null)
                {
                    if (contratosINET.Count() > 0)
                    {
                        foreach (var item in contratosINET)
                        {
                            if (await ContratosDBServices.FindContrato(item.IdContrato) == null)
                                await ContratosDBServices.AddContrato(item);
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un problema intentando conectar con el Servicio, continuaremos sin actualizar", "OK");
                    IsBusy = false;
                    if (IsBusy == false)
                    {
                        continueCon = false;
                        var route = $"//{nameof(ListContratosPage)}";
                        await Shell.Current.GoToAsync(route);
                    }
                }

                if (continueCon)
                {
                    if (user != null)
                    {
                        var totalTipoDoc = await TipoDocumentoDBServices.GetTipoDoc();
                        var TipoDoc = await InternetTipoDocumentosServices.GetListTipo();
                        if(TipoDoc != null)
                        {
                            if(totalTipoDoc.Count() < TipoDoc.ToList().Count())
                            {
                                foreach (var item in TipoDoc)
                                {
                                    if(await TipoDocumentoDBServices.FindTipoDoc(item.IdTipo) == null)
                                    {
                                        await TipoDocumentoDBServices.AddTipoDoc(item);
                                    }
                                }
                            }
                        }


                        var totalTipoCom = await TipoComunicacionDBServices.GetTipoCom();
                        var TipoCom = await InternetTipoComunicacionServices.GetListTipo();
                        if (TipoCom != null)
                        {
                            if (totalTipoCom.Count() < TipoCom.ToList().Count())
                            {
                                foreach (var item in TipoCom)
                                {
                                    if (await TipoComunicacionDBServices.FindTipoCom(item.IdTipoCom) == null)
                                    {
                                        await TipoComunicacionDBServices.AddTipoCom(item);
                                    }
                                }
                            }
                        }

                        var totalSubTipoCom = await SubtipoComunicacionDBServices.GetSubtipos(); ;
                        var SubtipoCom = await InternetSubtipoComunicacionServices.GetListSubtipo();
                        if (SubtipoCom != null)
                        {
                            if (totalSubTipoCom.Count() < SubtipoCom.ToList().Count())
                            {
                                foreach (var item in SubtipoCom)
                                {
                                    if (await SubtipoComunicacionDBServices.FindSubtipo(item.IdTipoSub) == null)
                                    {
                                        await SubtipoComunicacionDBServices.AddSubtipo(item);
                                    }
                                }
                            }
                        }

                        var CodSubCom = await CodSubComDBServices.GetCodSubCom(); ;
                        var CodSubComInet = await InternetCodSubComServices.GetListCodSubCom();
                        if (CodSubComInet != null)
                        {
                            if (CodSubCom.Count() < CodSubComInet.ToList().Count())
                            {
                                foreach (var item in CodSubComInet)
                                {
                                    if (await CodSubComDBServices.FindCodSubCom(item.IdControl) == null)
                                    {
                                        await CodSubComDBServices.AddCodSubCom(item);
                                    }
                                }
                            }
                        }

                        var ClassOne = await ClassOneDBServices.GetClassOne(); ;
                        var ClassOneInet = await InternetClassOneServices.GetListClassOne();
                        if (ClassOneInet != null)
                        {
                            if (ClassOne.Count() < ClassOneInet.ToList().Count())
                            {
                                foreach (var item in ClassOneInet)
                                {
                                    if (await ClassOneDBServices.FindClassOne(item.IdClassOne) == null)
                                    {
                                        await ClassOneDBServices.AddClassOne(item);
                                    }
                                }
                            }
                        }

                        var ClassTwo = await ClassTwoDBServices.GetClassTwo(); ;
                        var ClassTwoInet = await InternetClassTwoServices.GetListClassTwo();
                        if (ClassTwoInet != null)
                        {
                            if (ClassTwo.Count() < ClassTwoInet.ToList().Count())
                            {
                                foreach (var item in ClassTwoInet)
                                {
                                    if (await ClassTwoDBServices.FindClasTwo(item.IdClassTwo) == null)
                                    {
                                        await ClassTwoDBServices.AddClassTwo(item);
                                    }
                                }
                            }
                        }

                        var ClassDoc = await ClassDocDBServices.GetClassDoc(); ;
                        var ClassDocInet = await InternetClassDocServices.GetListClassDoc();
                        if (ClassDocInet != null)
                        {
                            if (ClassDoc.Count() < ClassDocInet.ToList().Count())
                            {
                                foreach (var item in ClassDocInet)
                                {
                                    if (await ClassDocDBServices.FindClassDoc(item.IdClassDoc) == null)
                                    {
                                        await ClassDocDBServices.AddClassDoc(item);
                                    }
                                }
                            }
                        }

                        UpdateData up = new UpdateData();
                        bool response = await up.UpdateInternetData();

                        await Task.Delay(1000);
                        IsBusy = false;
                        if (IsBusy == false)
                        {
                            var route = $"//{nameof(ListContratosPage)}";
                            await Shell.Current.GoToAsync(route);
                        }

                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un problema", "OK");
                    }
                }
                
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Verifique su conexión", "OK");
                IsBusy = false;
                await Task.Delay(1000);
                if (IsBusy == false)
                {
                    var route = $"//{nameof(ListContratosPage)}";
                    await Shell.Current.GoToAsync(route);
                }
               
            }


            
        }
    }
}
