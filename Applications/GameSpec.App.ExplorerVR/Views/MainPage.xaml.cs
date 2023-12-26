﻿using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSpec.App.Explorer.Views
{
    /// <summary>
    /// ExplorerMainTab
    /// </summary>
    public class ExplorerMainTab
    {
        public string Name { get; set; }
        public PakFile PakFile { get; set; }
        public string Text { get; set; }
        public string OpenPath { get; set; }
    }

    public partial class MainPage : ContentPage
    {
        public static MainPage Instance;

        public MainPage()
        {
            InitializeComponent();
            Instance = this;
            BindingContext = this;
        }

        // https://dev.to/davidortinau/making-a-tabbar-or-segmentedcontrol-in-net-maui-54ha
        void MainTab_Changed(object sender, CheckedChangedEventArgs e) => MainTabContent.BindingContext = ((RadioButton)sender).BindingContext;

        public void Open(Family family, IEnumerable<Uri> pakUris, string path = null)
        {
            foreach (var pakFile in PakFiles) pakFile?.Dispose();
            PakFiles.Clear();
            if (family == null) return;
            foreach (var pakUri in pakUris)
            {
                Log.WriteLine($"Opening {pakUri}");
                var pak = family.OpenPakFile(pakUri);
                if (pak != null) PakFiles.Add(pak);
            }
            Log.WriteLine("Done");
            OnOpenedAsync(family, path).Wait();
        }

        public static readonly BindableProperty MainTabsProperty = BindableProperty.Create(nameof(MainTabs), typeof(IList<ExplorerMainTab>), typeof(MainPage),
            propertyChanged: (d, e, n) =>
            {
                var mainTab = ((MainPage)d).MainTab;
                var firstTab = mainTab.Children.FirstOrDefault() as RadioButton;
                if (firstTab != null) firstTab.IsChecked = true;
            });
        public IList<ExplorerMainTab> MainTabs
        {
            get => (IList<ExplorerMainTab>)GetValue(MainTabsProperty);
            set => SetValue(MainTabsProperty, value);
        }

        public readonly IList<PakFile> PakFiles = new List<PakFile>();

        public Task OnOpenedAsync(Family family, string path = null)
        {
            var tabs = PakFiles.Select(pakFile => new ExplorerMainTab
            {
                Name = pakFile.Name,
                PakFile = pakFile,
                OpenPath = path,
            }).ToList();
            if (family.Description != null)
                tabs.Add(new ExplorerMainTab
                {
                    Name = "Information",
                    Text = family.Description,
                });
            MainTabs = tabs;
            return Task.CompletedTask;
        }

        internal void OnReady()
        {
            OpenPage_Click(null, null);
        }

        #region Menu

        void OpenPage_Click(object sender, EventArgs e)
        {
            var openPage = new OpenPage();
            openPage.OnReady();
            //App.Instance.MainPage = openPage;
            Navigation.PushAsync(openPage).Wait();
        }

        void OptionsPage_Click(object sender, EventArgs e)
        {
            var optionsPage = new OptionsPage();
            //Navigation.PushAsync(optionsPage).Wait();
        }

        void WorldMap_Click(object sender, EventArgs e)
        {
            //if (DatManager.CellDat == null || DatManager.PortalDat == null) return;
            //EngineView.ViewMode = ViewMode.Map;
        }

        void AboutPage_Click(object sender, EventArgs e)
        {
            var aboutPage = new AboutPage();
            //Navigation.PushModalAsync(aboutPage).Wait();
        }

        void Guide_Click(object sender, EventArgs e)
        {
            //Process.Start(@"docs\index.html");
        }

        #endregion
    }
}