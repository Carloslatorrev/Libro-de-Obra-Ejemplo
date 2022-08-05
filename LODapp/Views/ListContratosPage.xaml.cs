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
    public partial class ListContratosPage : ContentPage
    {
       
        public ListContratosPage()
        {
            InitializeComponent();
            //contratos = 3;
            //LabelBienvenida.Text = $"Tiene un total de {contratos}";

            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Orange;
        }

        private void BtnVisualizar_Clicked(object sender, EventArgs e)
        {

        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void searchBarContrato_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}