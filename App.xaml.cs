﻿namespace PM2App2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new View.PageListEmple());
        }
    }
}
