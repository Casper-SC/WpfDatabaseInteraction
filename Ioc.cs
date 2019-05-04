using System;
using DatabaseExample.Database.Fake;
using DatabaseExample.Database.Interfaces;
using DatabaseExample.Services;
using GalaSoft.MvvmLight.Ioc;

namespace DatabaseExample
{
    public static class Ioc
    {
        private static SimpleIoc _ioc;

        public static DatabaseService DatabaseService
        {
            get { return _ioc.GetInstance<DatabaseService>(); }
        }

        public static void Initialize(SimpleIoc ioc, bool fake)
        {
            _ioc = ioc ?? throw new ArgumentNullException(nameof(ioc));

            ioc.Register<IDatabase, DatabaseImitation>();
            if (fake)
            {
                ioc.Register<IDatabaseInitializer, DatabaseInitializerDebug>();
            }
            else
            {
                ioc.Register<IDatabaseInitializer, DatabaseInitializerDefault>();
            }
            ioc.Register<DatabaseService>();
        }
    }
}
