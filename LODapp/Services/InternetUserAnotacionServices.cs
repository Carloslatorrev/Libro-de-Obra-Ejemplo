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
    public class InternetUserAnotacionServices : InternetServices
    {


        public static async Task<IEnumerable<LOD_UserAnotacion>> GetUserAnotacionByUser(string UserId)
        {
            var json = "";
            try
            {
                json = await client.GetStringAsync($"UserAnotacion/FindByUser/{UserId}");
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var userAnots = JsonConvert.DeserializeObject<IEnumerable<LOD_UserAnotacion>>(json);
            return userAnots;

        }

        public static async Task<LOD_UserAnotacion> CreateUserBorrador(UserBorradorView userBorrador)
        {
            var receptorResponse = "";
            LOD_UserAnotacion userAnotacion = new LOD_UserAnotacion();
            try
            {
                var json = JsonConvert.SerializeObject(userBorrador);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("UserAnotacion/CreateUserBorrador", content);

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




    }
}
