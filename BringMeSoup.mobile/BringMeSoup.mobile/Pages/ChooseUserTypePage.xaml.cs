﻿using BringMeSoup.mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BringMeSoup.mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseUserTypePage : ContentPage
    {
        public ChooseUserTypePage()
        {
            InitializeComponent();
            ((BaseViewModel)this.BindingContext).Navigation = Navigation;
        }
    }
}