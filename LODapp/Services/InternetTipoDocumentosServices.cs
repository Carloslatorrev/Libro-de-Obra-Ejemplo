using LODapp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LODapp.Services
{
    public class InternetTipoDocumentosServices : InternetServices
    {
        public static async Task<IEnumerable<MAE_TipoDocumento>> GetListTipo()
        {
            var json = "";
            try
            {

                json = await client.GetStringAsync($"TipoDocumento/List");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var tipos = JsonConvert.DeserializeObject<IEnumerable<MAE_TipoDocumento>>(json);
            return tipos;

        }
    }
}
