﻿using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace GameSpec.App.Explorer
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage() => InitializeComponent();

        public void OnFirstLoad() { }

        public void Open(Family familySelectedItem, IList<Uri> pakUris) { }

        void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";
        }
    }
}