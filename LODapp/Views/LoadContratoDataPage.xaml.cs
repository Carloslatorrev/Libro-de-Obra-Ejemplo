﻿using LODapp.ViewModels;
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
    //[QueryProperty(nameof(ObjectID), nameof(ObjectID))]
    public partial class LoadContratoDataPage : ContentPage
    {
        //private LoadContratoDataViewModel viewmodel;
        //string obID;
        //public string ObjectID { get { return obID; } set { obID = Uri.UnescapeDataString(value); } }

        public LoadContratoDataPage()
        {
            InitializeComponent();
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    BindingContext = viewmodel = new LoadContratoDataViewModel(ObjectID);
        //    //viewmodel.GetDataCommand.ExecuteAsync(obID);

        //}
    }
}