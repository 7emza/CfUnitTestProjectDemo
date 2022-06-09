using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CfUnitTestProjectDemo.Common.Contracts;
using CfUnitTestProjectDemo.DataLayer.DataLayer;
using CfUnitTestProjectDemoUI.ViewModels;
using CfUnitTestProjectDemoUI.Views;
using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;

namespace CfUnitTestProjectDemoUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow,HomeViewModel>();

            containerRegistry.RegisterSingleton<IDataLayerService, DataLayerService>();
            //containerRegistry.RegisterSingleton<IMemberRepository, MemberRepository>();
        }
    }
}
