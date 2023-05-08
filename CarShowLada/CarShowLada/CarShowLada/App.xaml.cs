using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CarShowLada
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entities.CarShowLadaEntities Context
        { get; } = new Entities.CarShowLadaEntities();

        public static Entities.User CurrentUser = null;
    }
}
