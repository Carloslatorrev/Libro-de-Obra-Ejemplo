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
    public class InternetUsuariosLodServices : InternetServices
    {

        public static async Task<IEnumerable<LOD_UsuariosLod>> GetUsuariosLodByUser(string UserId)
        {
            var json = "";
            try
            {
                
                json = await client.GetStringAsync($"UsuariosLod/FindByUser/{UserId}");
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var libros = JsonConvert.DeserializeObject<IEnumerable<LOD_UsuariosLod>>(json);
            return libros;

        }

        public static async Task<IEnumerable<LOD_UsuariosLod>> GetUsuariosLodByLod(string IdLod)
        {
            var json = "";
            try
            {

                json = await client.GetStringAsync($"UsuariosLod/FindByLod/{IdLod}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var libros = JsonConvert.DeserializeObject<IEnumerable<LOD_UsuariosLod>>(json);
            return libros;

        }

        public static async Task<IEnumerable<LOD_UsuariosLod>> GetUsuariosLodByContrato(string IdContrato)
        {
            var json = "";
            try
            {
                json = await client.GetStringAsync($"UsuariosLod/FindByContrato/{IdContrato}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var libros = JsonConvert.DeserializeObject<IEnumerable<LOD_UsuariosLod>>(json);
            return libros;

        }


        public static async Task<LOD_UsuariosLod> GetUserLod(string id)
        {
            var json = "";
            try
            {
                json = await client.GetStringAsync($"UsuariosLod/Find/{id}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var libro = JsonConvert.DeserializeObject<LOD_UsuariosLod>(json);
            return libro;

        }

        //public static async Task AddContrato(string Nombre, string Numero)
        //{
        //    await Init();
        //    var image = "https://tucontador.ec/wp-content/uploads/2020/07/contract.png";
        //    var Contrato = new Contrato()
        //    {
        //        Nombre = Nombre,
        //        Numero = Numero,
        //        Image = image
        //    };

        //    var id = await db.InsertAsync(Contrato);

        //}

        //public static async Task RemoveContrato(int id) {
        //    await Init();
        //    await db.DeleteAsync<Contrato>(id);

        //}





    }
}
