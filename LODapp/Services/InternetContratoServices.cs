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
    public class InternetContratoServices : InternetServices
    {
        

        public static async Task<IEnumerable<CON_Contratos>> GetSelectContrato(string UserId)
        {
            var json = "";
            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    json = await client.GetStringAsync($"/Contratos/FindByUser/{UserId}");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Error: Operación cancelada");
                }
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var contratos = JsonConvert.DeserializeObject<IEnumerable<CON_Contratos>>(json);
            return contratos;

        }

        public static async Task<IEnumerable<CON_Contratos>> GetContrato(string id)
        {
            var json = "";
            try
            {
                json = await client.GetStringAsync($"/Contratos/Find/{id}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var contratos = JsonConvert.DeserializeObject<IEnumerable<CON_Contratos>>(json);
            return contratos;

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
