using DatabaseExample.Database.Fake;
using DatabaseExample.Database.Interfaces;
using DatabaseExample.Services;
using GalaSoft.MvvmLight.Ioc;

namespace DatabaseExample
{
    public static class Ioc
    {
        public static DatabaseService DatabaseService => SimpleIoc.Default.GetInstance<DatabaseService>();

        public static void Initialize(bool fake)
        {
            SimpleIoc.Default.Register<IDatabase, DatabaseImitation>();
            if (fake)
            {
                SimpleIoc.Default.Register<IDatabaseInitializer, DatabaseInitializerDebug>();
            }
            else
            {
                SimpleIoc.Default.Register<IDatabaseInitializer, DatabaseInitializerDefault>();
            }
            SimpleIoc.Default.Register<DatabaseService>();
        }
    }
}
