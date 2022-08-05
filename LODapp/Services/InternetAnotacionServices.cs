using LODapp.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LODapp.Services
{
    public class InternetAnotacionServices : InternetServices
    {

        public static async Task<IEnumerable<LOD_Anotaciones>> GetAnotacionesByUser(AnotacionUserView UserAnotacion)
        {
            var anotacionesResponse = "";
            List<LOD_Anotaciones> anotaciones = new List<LOD_Anotaciones>();
            try
            {
                var json = JsonConvert.SerializeObject(UserAnotacion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    response = await client.PostAsync("Anotaciones/FindByUserLod", content);
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Error: Operación Cancelada");
                    anotaciones = null;
                    return anotaciones;
                }
                

                if (response.IsSuccessStatusCode)
                {
                    anotacionesResponse = await response.Content.ReadAsStringAsync();
                    anotaciones = JsonConvert.DeserializeObject<List<LOD_Anotaciones>>(anotacionesResponse);
                    return anotaciones;
                }
                else
                {
                    anotaciones = null;
                    return anotaciones;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                anotaciones = null;
                return anotaciones;
            }
        }

        public static async Task<LOD_Anotaciones> GetAnotacion(string IdAnotacion)
        {
            var json = "";
            try
            {

                json = await client.GetStringAsync($"Anotaciones/Find/{IdAnotacion}");

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var anotacion = JsonConvert.DeserializeObject<LOD_Anotaciones>(json);
            return anotacion;

        }

        public static async Task<List<LOD_Anotaciones>> GetAnotacionByLod(int IdLod)
        {
            var json = "";
            try
            {

                json = await client.GetStringAsync($"Anotaciones/FindByLod/{IdLod}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var anotacion = JsonConvert.DeserializeObject<List<LOD_Anotaciones>>(json);
            return anotacion;

        }

        public static async Task<IEnumerable<LOD_UserAnotacion>> GetReceptores(string IdAnotacion)
        {
            var json = "";
            try
            {

                json = await client.GetStringAsync($"Anotaciones/FindReceptores/{IdAnotacion}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var anotacion = JsonConvert.DeserializeObject<IEnumerable<LOD_UserAnotacion>>(json);
            return anotacion;

        }

        public static async Task<IEnumerable<LOD_ReferenciasAnot>> GetReferencias(string IdAnotacion)
        {
            var json = "";
            try
            {
                try
                {
                    json = await client.GetStringAsync($"Anotaciones/FindReferencias/{IdAnotacion}");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Error: Operación cancelada");
                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var anotacion = JsonConvert.DeserializeObject<IEnumerable<LOD_ReferenciasAnot>>(json);
            return anotacion;

        }

        public static async Task<IEnumerable<LOD_docAnotacion>> GetDocumentos(string IdAnotacion)
        {
            var json = "";
            try
            {

                json = await client.GetStringAsync($"Anotaciones/GetDocumentos/{IdAnotacion}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var anotacion = JsonConvert.DeserializeObject<IEnumerable<LOD_docAnotacion>>(json);
            return anotacion;

        }


        public static async Task<IEnumerable<VistoBuenoView>> GetVistoBueno(string IdAnotacion)
        {
            var json = "";
            try
            {
                json = await client.GetStringAsync($"Anotaciones/FindVistoBueno/{IdAnotacion}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var anotacion = JsonConvert.DeserializeObject<IEnumerable<VistoBuenoView>>(json);
            return anotacion;

        }

        public static async Task<IEnumerable<LOD_log>> GetLogs(string IdAnotacion)
        {
            var json = "";
            try
            {
                json = await client.GetStringAsync($"Anotaciones/FindLogs/{IdAnotacion}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var anotacion = JsonConvert.DeserializeObject<IEnumerable<LOD_log>>(json);
            return anotacion;

        }

        public static async Task<LOD_Anotaciones> CreateAnotacion(AnotacionesCreate anotacion)
        {
            var anotacionesResponse = "";
            LOD_Anotaciones anotaciones = new LOD_Anotaciones();
            try
            {
                var json = JsonConvert.SerializeObject(anotacion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Anotaciones/Create", content);

                if (response.IsSuccessStatusCode)
                {
                    anotacionesResponse = await response.Content.ReadAsStringAsync();
                    anotaciones = JsonConvert.DeserializeObject<LOD_Anotaciones>(anotacionesResponse);
                    return anotaciones;
                }
                else
                {
                    anotaciones = null;
                    return anotaciones;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                anotaciones = null;
                return anotaciones;
            }
        }

        public static async Task<LOD_Anotaciones> FirmarAnotacion(FirmarAnotacion anotacion)
        {
            var anotacionesResponse = "";
            LOD_Anotaciones anotaciones = new LOD_Anotaciones();
            try
            {
                var json = JsonConvert.SerializeObject(anotacion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Anotaciones/FirmarAnotacion", content);

                if (response.IsSuccessStatusCode)
                {
                    anotacionesResponse = await response.Content.ReadAsStringAsync();
                    anotaciones = JsonConvert.DeserializeObject<LOD_Anotaciones>(anotacionesResponse);
                    return anotaciones;
                }
                else
                {
                    anotaciones = null;
                    return anotaciones;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                anotaciones = null;
                return anotaciones;
            }
        }

        public static async Task<LOD_Anotaciones> SolicitarFirma(SolicitudFirma anotacion)
        {
            var anotacionesResponse = "";
            LOD_Anotaciones anotaciones = new LOD_Anotaciones();
            try
            {
                var json = JsonConvert.SerializeObject(anotacion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Anotaciones/SolicitarFirma", content);

                if (response.IsSuccessStatusCode)
                {
                    anotacionesResponse = await response.Content.ReadAsStringAsync();
                    anotaciones = JsonConvert.DeserializeObject<LOD_Anotaciones>(anotacionesResponse);
                    return anotaciones;
                }
                else
                {
                    anotaciones = null;
                    return anotaciones;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                anotaciones = null;
                return anotaciones;
            }
        }


        public static async Task<LOD_Anotaciones> UpdateAnotacion(UpdateAnotacion anotacion)
        {
            var anotacionesResponse = "";
            LOD_Anotaciones anotaciones = new LOD_Anotaciones();
            try
            {
                var json = JsonConvert.SerializeObject(anotacion);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Anotaciones/UpdateAnotacion", content);

                if (response.IsSuccessStatusCode)
                {
                    anotacionesResponse = await response.Content.ReadAsStringAsync();
                    anotaciones = JsonConvert.DeserializeObject<LOD_Anotaciones>(anotacionesResponse);
                    return anotaciones;
                }
                else
                {
                    anotaciones = null;
                    return anotaciones;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                anotaciones = null;
                return anotaciones;
            }
        }

        public static async Task<LOD_UserAnotacion> CreateReceptor(ReceptoresView receptores)
        {
            var receptorResponse = "";
            LOD_UserAnotacion userAnotacion = new LOD_UserAnotacion();
            try
            {
                var json = JsonConvert.SerializeObject(receptores);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Receptores/Create", content);

                if (response.IsSuccessStatusCode)
                {
                    receptorResponse = await response.Content.ReadAsStringAsync();
                    userAnotacion = JsonConvert.DeserializeObject<LOD_UserAnotacion>(receptorResponse);
                    return userAnotacion;
                }
                else
                {
                    userAnotacion = null;
                    return userAnotacion;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                userAnotacion = null;
                return userAnotacion;
            }
        }

        public static async Task<LOD_UserAnotacion> UpdateReceptor(ReceptoresView receptores)
        {
            var receptorResponse = "";
            LOD_UserAnotacion userAnotacion = new LOD_UserAnotacion();
            try
            {
                var json = JsonConvert.SerializeObject(receptores);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Receptores/Update", content);

                if (response.IsSuccessStatusCode)
                {
                    receptorResponse = await response.Content.ReadAsStringAsync();
                    userAnotacion = JsonConvert.DeserializeObject<LOD_UserAnotacion>(receptorResponse);
                    return userAnotacion;
                }
                else
                {
                    userAnotacion = null;
                    return userAnotacion;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                userAnotacion = null;
                return userAnotacion;
            }
        }


        public static async Task<LOD_UserAnotacion> VBConfirmed(VBConfirmedView receptores)
        {
            var receptorResponse = "";
            LOD_UserAnotacion userAnotacion = new LOD_UserAnotacion();
            try
            {
                var json = JsonConvert.SerializeObject(receptores);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Anotaciones/VBConfirmed", content);

                if (response.IsSuccessStatusCode)
                {
                    receptorResponse = await response.Content.ReadAsStringAsync();
                    userAnotacion = JsonConvert.DeserializeObject<LOD_UserAnotacion>(receptorResponse);
                    return userAnotacion;
                }
                else
                {
                    userAnotacion = null;
                    return userAnotacion;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                userAnotacion = null;
                return userAnotacion;
            }
        }

        public static async Task<LOD_LibroObras> DeleteAnotacion(int IdAnotacion)
        {
            var json = "";
            try
            {

                json = await client.GetStringAsync($"Anotaciones/Delete/{IdAnotacion}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var anotacion = JsonConvert.DeserializeObject<LOD_LibroObras>(json);
            return anotacion;
        }

        public static async Task<bool> RemoveDoc(int IdDocAnotacion)
        {
            var json = "";
            try
            {

                json = await client.GetStringAsync($"Anotaciones/RemoveDoc/{IdDocAnotacion}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var anotacion = JsonConvert.DeserializeObject<bool>(json);
            return anotacion;
        }

        public static async Task<bool> RemoveReferencia(int IdReferencia)
        {
            var json = "";
            try
            {

                json = await client.GetStringAsync($"Anotaciones/RemoveReferencia/{IdReferencia}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var anotacion = JsonConvert.DeserializeObject<bool>(json);
            return anotacion;
        }

        public static async Task<bool> RemoveReceptor(int IdUserLod, int IdAnotacion)
        {
            var json = "";
            try
            {
                string IdCompuesto = $"{IdUserLod};{IdAnotacion}";
                json = await client.GetStringAsync($"Anotaciones/RemoveReceptor/{IdCompuesto}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var anotacion = JsonConvert.DeserializeObject<bool>(json);
            return anotacion;
        }


        public static async Task<LOD_docAnotacion> CreateDocumento(AddDocumentoView documento, FileResult fileBase)
        {
            var docResponse = "";
            LOD_docAnotacion docAnotacion = new LOD_docAnotacion();
            try
            {
                //var fileConvert = JsonConvert.SerializeObject(fileBase);
                //documento.file = fileConvert;
                var json = JsonConvert.SerializeObject(documento);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var contentTotal = new MultipartFormDataContent();
                
                var stream = await fileBase.OpenReadAsync();
                string fileName = fileBase.FileName;
                contentTotal.Add(new StreamContent(stream), "\"File\"", $"\"{fileName}\"");
                contentTotal.Add(content,"JsonDetails");
                var response = await client.PostAsync("Anotaciones/CreateDocumento", contentTotal);

                if (response.IsSuccessStatusCode)
                {
                    docResponse = await response.Content.ReadAsStringAsync();
                    docAnotacion = JsonConvert.DeserializeObject<LOD_docAnotacion>(docResponse);
                    return docAnotacion;
                }
                else
                {
                    docAnotacion = null;
                    return docAnotacion;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                docAnotacion = null;
                return docAnotacion;
            }
        }

        public static async Task<LOD_docAnotacion> CreateOtroDocumento(AddOtroDocumentoView documento, FileResult fileBase)
        {
            var docResponse = "";
            LOD_docAnotacion docAnotacion = new LOD_docAnotacion();
            try
            {
                var json = JsonConvert.SerializeObject(documento);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var contentTotal = new MultipartFormDataContent();

                var stream = await fileBase.OpenReadAsync();
                string fileName = fileBase.FileName;
                contentTotal.Add(new StreamContent(stream), "\"File\"", $"\"{fileName}\"");
                contentTotal.Add(content, "JsonDetails");
                var response = await client.PostAsync("Anotaciones/CreateOtroDocumento", contentTotal);

                if (response.IsSuccessStatusCode)
                {
                    docResponse = await response.Content.ReadAsStringAsync();
                    docAnotacion = JsonConvert.DeserializeObject<LOD_docAnotacion>(docResponse);
                    return docAnotacion;
                }
                else
                {
                    docAnotacion = null;
                    return docAnotacion;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                docAnotacion = null;
                return docAnotacion;
            }
        }


        public static async Task<LOD_ReferenciasAnot> CreateReferencia(CreateReferencia referencia)
        {
            var receptorResponse = "";
            LOD_ReferenciasAnot referenciaCreate = new LOD_ReferenciasAnot();
            try
            {
                var json = JsonConvert.SerializeObject(referencia);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Anotaciones/AddReferencia", content);

                if (response.IsSuccessStatusCode)
                {
                    receptorResponse = await response.Content.ReadAsStringAsync();
                    referenciaCreate = JsonConvert.DeserializeObject<LOD_ReferenciasAnot>(receptorResponse);
                    return referenciaCreate;
                }
                else
                {
                    referenciaCreate = null;
                    return referenciaCreate;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                referenciaCreate = null;
                return referenciaCreate;
            }
        }

        public static async Task<LOD_docAnotacion> AprobarDoc(string IdDocAnotacion)
        {
            var json = "";
            try
            {

                json = await client.GetStringAsync($"Anotaciones/AprobarDoc/{IdDocAnotacion}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var anotacion = JsonConvert.DeserializeObject<LOD_docAnotacion>(json);
            return anotacion;

        }

        public static async Task<LOD_docAnotacion> RechazarDoc(string IdDocAnotacion)
        {
            var json = "";
            try
            {

                json = await client.GetStringAsync($"Anotaciones/RechazarDoc/{IdDocAnotacion}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var anotacion = JsonConvert.DeserializeObject<LOD_docAnotacion>(json);
            return anotacion;

        }
    }
}
