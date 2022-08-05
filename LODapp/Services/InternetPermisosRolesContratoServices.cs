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
    public class InternetPermisosRolesContratoServices : InternetServices
    {

        public static async Task<IEnumerable<LOD_PermisosRolesContrato>> GetPermisos(string UserId)
        {
            var json = "";
            try
            {

                json = await client.GetStringAsync($"PermisosRolesContrato/FindByUsuario/{UserId}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var permisos = JsonConvert.DeserializeObject<IEnumerable<LOD_PermisosRolesContrato>>(json);
            return permisos;

        }



    }
}
