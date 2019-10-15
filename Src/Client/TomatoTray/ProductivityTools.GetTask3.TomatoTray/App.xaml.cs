﻿using Microsoft.AspNetCore.SignalR.Client;
using ProductivityTools.GetTask3.TomatoTray.Managers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TomatoesTray;
using TomatoesTray.Events;

namespace ProductivityTools.GetTask3.TomatoTray
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        TomatoManager TomatoManager;
        EventAggregator.EventAggregator EventAggregator = new EventAggregator.EventAggregator();
        IconNotyfication IconNotyfication;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application.Current.MainWindow = null;
            //base.OnStartup(e);
            IconNotyfication = new IconNotyfication(EventAggregator);
            //   IconNotyfication.TaskbarIcon.ContextMenu.Items.Add(new MenuItem() { Header = "close", Command = ExitApplicationCommand });

            IconNotyfication.TaskbarIcon.DataContext = this;

            TomatoManager = new TomatoManager(EventAggregator);
            Connect(URL);
            ////this.EventAggregator.Subscribe(this);
            //WCFServer server = new WCFServer(this.EventAggregator);
            //server.Run();
        }

        HubConnection connection;
        private const string URL = "http://localhost:5501/TomatoHub/";
        private void Connect(string uri)
        {
            try
            {
                connection = new HubConnectionBuilder().WithUrl(uri).Build();
                connection.On<string>("NewTomato", update =>
                {
                    EventAggregator.PublishEvent(new ShowBalonEvent { Text = update, Status = TomatoStatus.Work});
                    Console.Write("Fdsa");
                    //this.Dispatcher.Invoke(() => lblContent.Content = update);
                });
                connection.StartAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
