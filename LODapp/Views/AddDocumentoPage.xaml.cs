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
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    //[QueryProperty(nameof(ObjectID), nameof(ObjectID))]
    //[QueryProperty(nameof(ObjectID2), nameof(ObjectID2))]
    public partial class AddDocumentoPage : ContentPage
    {
        //private AddDocumentoViewModel viewmodel;
        //string obID;
        //public string ObjectID { get { return obID; } set { obID = Uri.UnescapeDataString(value); } }
        //string obID2;
        //public string ObjectID2 { get { return obID2; } set { obID2 = Uri.UnescapeDataString(value); } }
        public AddDocumentoPage()
        {
            InitializeComponent();
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    BindingContext = viewmodel = new AddDocumentoViewModel(ObjectID,ObjectID2);
        //    //viewmodel.GetDataCommand.ExecuteAsync(obID);

        //}
    }
}