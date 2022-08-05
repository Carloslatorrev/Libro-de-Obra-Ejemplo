using LODapp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LODapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ObjectID),nameof(ObjectID))]
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel viewmodel;
        string obID;
        public string ObjectID { get { return obID; } set { obID = Uri.UnescapeDataString(value); } }

        public LoginPage()
        {
           
            //var vm = new LoginViewModel();
            //BindingContext = vm;
            
            //vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Credenciales Incorrectas, Intente de nuevo.", "OK");
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = viewmodel = new LoginViewModel(ObjectID);

        }

    }
}