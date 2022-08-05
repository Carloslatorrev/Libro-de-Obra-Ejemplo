using LODapp.Models;
using LODapp.ViewModels;
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
    public class InternetLoginServices : InternetServices
    {

        public static async Task<ApplicationUserView> Login(string Rut, string Password)
        {
            var userResponse = "";
            ApplicationUserView user = new ApplicationUserView();
            try
            {
                LoginModel login = new LoginModel();
                login.Run = Rut;
                login.Password = Password;
                var json = JsonConvert.SerializeObject(login);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"Account/Login", content);

                if(response.IsSuccessStatusCode)
                {
                    userResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<ApplicationUserView>(userResponse);
                    return user;
                }else
                {
                    if(response.Content.ReadAsStringAsync().Equals("Usuario no existe."))
                    {
                        ApplicationUserView userError = new ApplicationUserView();
                        userError.Nombres = "Error";
                        return userError;
                    }
                    else
                    {
                        user = null;
                        return user;
                    }
                }


            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                user = null;
                return user;
            }

        }

    }
}
