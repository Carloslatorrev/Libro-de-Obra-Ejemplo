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
    public partial class EditReceptorPage : ContentPage
    {
        //private EditarReceptorViewModel viewmodel;
        //string obID;
        //public string ObjectID { get { return obID; } set { obID = Uri.UnescapeDataString(value); } }
        public EditReceptorPage()
        {
            InitializeComponent();
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    BindingContext = viewmodel = new EditarReceptorViewModel(ObjectID);
        //    //viewmodel.GetDataCommand.ExecuteAsync(obID);

        //}
    }
}