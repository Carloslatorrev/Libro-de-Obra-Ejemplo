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
    public class InternetClassDocServices : InternetServices
    {

        public static async Task<IEnumerable<MAE_ClassDoc>> GetListClassDoc()
        {
            var json = "";
            try
            {

                json = await client.GetStringAsync($"ClassDoc/List");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var tipos = JsonConvert.DeserializeObject<IEnumerable<MAE_ClassDoc>>(json);
            return tipos;

        }

        public static async Task<IEnumerable<MAE_ClassDoc>> GetClassDoc(string IdClassDoc)
        {
            var json = "";
            try
            {
                
                json = await client.GetStringAsync($"ClassTwo/Find/{IdClassDoc}");
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var libros = JsonConvert.DeserializeObject<IEnumerable<MAE_ClassDoc>>(json);
            return libros;

        }

        public static async Task<IEnumerable<MAE_ClassDoc>> GetClassDocByClassTwo(string id)
        {
            var json = "";
            try
            {
                json = await client.GetStringAsync($"ClassTwo/FindByTwo/{id}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var libro = JsonConvert.DeserializeObject<IEnumerable<MAE_ClassDoc>>(json);
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
