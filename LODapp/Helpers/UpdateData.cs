using LODapp.Data;
using LODapp.Models;
using LODapp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LODapp.Helpers
{
    public class UpdateData
    {
        public async Task<bool> UpdateInternetData()
        {
            /*
             Estados: 1 = Creado y Actualizado.
                      2 = Creado pero no Actualizado.
                      3 = Creado, Editado y Actualizado.
                      4 = Creado, Editado y No Actualizado.
             
             */

            ApplicationUser user = await ApplicationUserDBServices.FindUser(1);

            bool actualizado = false;
            List<LOD_Anotaciones> anotNoCreadas = await AnotacionesDBServices.GetAnotacionesNoCreadas();
            foreach (var item in anotNoCreadas)
            {
                AnotacionesCreate anotCreate = new AnotacionesCreate();
                anotCreate.FechaSolicitud = item.FechaTopeRespuesta;
                anotCreate.titulo = item.Titulo;
                anotCreate.SolicitudVB = item.SolicitudVB;
                anotCreate.SolicitudResp = item.SolicitudRest;
                anotCreate.cuerpo = item.Cuerpo;
                anotCreate.IdLod = item.IdLod;
                anotCreate.IdSubTipo = item.IdTipoSub;
                anotCreate.UserId = item.UserId;

                LOD_Anotaciones anotActualizada = new LOD_Anotaciones();
                try
                {
                    anotActualizada = await InternetAnotacionServices.CreateAnotacion(anotCreate);
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (anotActualizada != null)
                {
                    item.IdAnotacion = anotActualizada.IdAnotacion;
                    await AnotacionesDBServices.UpdateAnotacion(item);
                    actualizado = true;
                }
            }

            List<LOD_Anotaciones> anotNoActualizadas = await AnotacionesDBServices.GetAnotacionesNoCreadas();
            foreach (var item in anotNoActualizadas)
            {
                UpdateAnotacion anotUpdate = new UpdateAnotacion();
                anotUpdate.FechaSolicitud = item.FechaTopeRespuesta;
                anotUpdate.titulo = item.Titulo;
                anotUpdate.SolicitudResp = item.SolicitudRest;
                anotUpdate.cuerpo = item.Cuerpo;
                anotUpdate.IdLod = item.IdLod;
                anotUpdate.IdSubTipo = item.IdTipoSub;
                anotUpdate.IdAnotacion = item.IdAnotacion;

                LOD_Anotaciones anotUpActualizada = new LOD_Anotaciones();
                try
                {
                    anotUpActualizada = await InternetAnotacionServices.UpdateAnotacion(anotUpdate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (anotUpActualizada != null)
                {
                    item.Actualizado = 3;
                    await AnotacionesDBServices.UpdateAnotacion(item);
                    actualizado = true;
                }
            }

            List<LOD_Anotaciones> anotNoFirmadas = await AnotacionesDBServices.GetAnotacionesNoFirmadas();
            foreach (var item in anotNoFirmadas)
            {
                FirmarAnotacion anotFirmar = new FirmarAnotacion();
                anotFirmar.IdAnotacion = item.IdAnotacion;
                anotFirmar.password = user.Password;

                LOD_Anotaciones anotUpActualizada = new LOD_Anotaciones();
                try
                {
                    anotUpActualizada = await InternetAnotacionServices.FirmarAnotacion(anotFirmar);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (anotUpActualizada != null)
                {
                    item.Actualizado = 3;
                    await AnotacionesDBServices.UpdateAnotacion(item);
                    actualizado = true;
                }
            }


            List<LOD_ReferenciasAnot> anotRefNoActualizadas = await ReferenciasAnotDBServices.GetReferenciaAnotNoCreadas();
            foreach (var item in anotRefNoActualizadas)
            {
                CreateReferencia anotRefCreate = new CreateReferencia();
                anotRefCreate.Observacion = item.Observacion;
                anotRefCreate.IdAnotacion = item.IdAnotacion;
                anotRefCreate.IdAnontacionRef = item.IdAnontacionRef;
                anotRefCreate.EsRepuesta = item.EsRepuesta;

                LOD_ReferenciasAnot anotRefCreada = new LOD_ReferenciasAnot();
                try
                {
                    anotRefCreada = await InternetAnotacionServices.CreateReferencia(anotRefCreate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (anotRefCreada != null)
                {
                    item.IdRefAnot = anotRefCreada.IdRefAnot;
                    item.Actualizado = 1;
                    await ReferenciasAnotDBServices.UpdateRefAnotacion(item);
                    actualizado = true;
                }
            }

            List<LOD_UserAnotacion> receptoresNoCreados = await UserAnotacionDBServices.GetReceptoresNoCreados();
            foreach (var item in receptoresNoCreados)
            {
                ReceptoresView anotReceptorCreate = new ReceptoresView();
                anotReceptorCreate.IdAnotacion = item.IdAnotacion;
                anotReceptorCreate.IdUsLod = item.IdUsLod;
                anotReceptorCreate.EsRespRespuesta = item.EsRespRespuesta;
                anotReceptorCreate.EsPrincipal = item.EsPrincipal;

                LOD_UserAnotacion anotReceptorCreado = new LOD_UserAnotacion();
                try
                {
                    anotReceptorCreado = await InternetAnotacionServices.CreateReceptor(anotReceptorCreate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (anotReceptorCreado != null)
                {
                    item.Actualizado = 1;
                    await UserAnotacionDBServices.UpdateUserAnotacion(item);
                    actualizado = true;
                }
            }

            List<LOD_UserAnotacion> receptoresNoActualizados = await UserAnotacionDBServices.GetReceptoresNoActualizados();
            foreach (var item in receptoresNoActualizados)
            {
                ReceptoresView anotReceptorCreate = new ReceptoresView();
                anotReceptorCreate.IdAnotacion = item.IdAnotacion;
                anotReceptorCreate.IdUsLod = item.IdUsLod;
                anotReceptorCreate.EsRespRespuesta = item.EsRespRespuesta;
                anotReceptorCreate.EsPrincipal = item.EsPrincipal;

                LOD_UserAnotacion anotReceptorCreado = new LOD_UserAnotacion();
                try
                {
                    anotReceptorCreado = await InternetAnotacionServices.UpdateReceptor(anotReceptorCreate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (anotReceptorCreado != null)
                {
                    item.Actualizado = 3;
                    await UserAnotacionDBServices.UpdateUserAnotacion(item);
                    actualizado = true;
                }
            }

            List<LOD_docAnotacion> documentosNoActualizados = await DocAnotacionDBServices.GetDocumentosNoCreados();
            foreach (var item in documentosNoActualizados)
            {
                AddDocumentoView addDoc = new AddDocumentoView();
                LOD_docAnotacion docAnot = new LOD_docAnotacion();
                addDoc.IdAnotacion = item.IdAnotacion;
                addDoc.IdContrato = item.IdContrato;
                addDoc.IdTipoDoc = item.IdTipoDoc;
                addDoc.Nombre = item.documento;
                addDoc.Descripcion = item.Observaciones;
                addDoc.UserId = user.UserId;
                FileResult doc = await SaveLocalData.GetFileFromLocal(item.ruta);

                try
                {
                    docAnot = await InternetAnotacionServices.CreateDocumento(addDoc,doc);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (docAnot != null)
                {
                    item.Actualizado = 3;
                    await DocAnotacionDBServices.UpdateDocAnotacion(item);
                    actualizado = true;
                }
            }


            return actualizado;


        }

    }
}
