using CfUnitTestProjectDemo.Contracts.Repositories;
using CfUnitTestProjectDemo.Models.Base;
using CfUnitTestProjectDemo.Persistances;
using CfUnitTestProjectDemo.Persistances.Base;
using CfUnitTestProjectDemo.Services.Base;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CfUnitTestProjectDemo
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
            containerRegistry.RegisterSingleton<IRepositoryBase<IEntity>, RepositoryBase<IEntity>>();
            containerRegistry.RegisterSingleton<IMemberRepository, MemberRepository>();
        }
    }
}
