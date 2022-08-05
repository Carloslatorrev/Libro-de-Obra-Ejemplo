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
    public class InternetLibroObrasServices : InternetServices
    {
        public static async Task<IEnumerable<LOD_LibroObras>> GetSelectLibroObras(int IdContrato)
        {
            var json = "";
            try
            {
                string auxId = IdContrato.ToString();
                json = await client.GetStringAsync($"LibroObras/FindByContrato/{auxId}");
                var libros = JsonConvert.DeserializeObject<IEnumerable<LOD_LibroObras>>(json);
                return libros;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static async Task<IEnumerable<LOD_LibroObras>> GetLibroObrasByUser(LibroObrasUserView libUser)
        {
            var librosResponse = "";
            List<LOD_LibroObras> libros = new List<LOD_LibroObras>();
            try
            {
                var json = JsonConvert.SerializeObject(libUser);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    response = await client.PostAsync($"LibroObras/FindLibrosByUser", content);
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Error: Operación cancelada");
                    libros = null;
                    return libros;
                }
                

                if (response.IsSuccessStatusCode)
                {
                    librosResponse = await response.Content.ReadAsStringAsync();
                    libros = JsonConvert.DeserializeObject<List<LOD_LibroObras>>(librosResponse);
                    return libros;
                }
                else
                {
                    libros = null;
                    return libros;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                libros = null;
                return libros;
            }
        }


        public static async Task<IEnumerable<LOD_LibroObras>> GetLibro(string id)
        {
            var json = "";
            try
            {
                json = await client.GetStringAsync($"LibroObras/Find/{id}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var libro = JsonConvert.DeserializeObject<IEnumerable<LOD_LibroObras>>(json);
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
