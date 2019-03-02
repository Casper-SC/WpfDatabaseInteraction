using System.Diagnostics;
using System.Threading.Tasks;
using DatabaseExample.Database.Interfaces;
using DatabaseExample.Model;

namespace DatabaseExample.Services
{
    public class DatabaseService
    {
        private readonly IDatabase _database;
        private readonly IDatabaseInitializer _initializer;

        public event EventHandler<DatabaseService, DatabaseServiceUpdateEventArgs<Car>> CarUpdated;
        //public event EventHandler<DatabaseService, DatabaseServiceUpdateEventArgs<Bicycle>> BicycleUpdated;

        public DatabaseService(IDatabase database, IDatabaseInitializer initializer)
        {
            Debug.Assert(database != null);
            Debug.Assert(initializer != null);

            _database = database;
            _initializer = initializer;
        }

        public Task InitializeAsync()
        {
            return Task.Run(() =>
            {
                _database.Initialize(_initializer);
            });
        }

        public async Task AddAsync(Car car)
        {
            await Task.Run(() =>
            {
                _database.Add(car);
            });

            // Определись нужно ли ждать пока по факту добавится в БД или сгенерировать событие раньше.
            // Правда, если сгенерируешь раньше, а при добавлении произойдёт ошибка, то нужно уведомить об этом подписчиков.
            OnCarAdded(car, DatabaseServiceUpdate.Added);
        }

        public Task<Car[]> GetAllCarsAsync()
        {
            return Task.Run(() =>
            {
                return _database.GetAllCars();
            });
        }

        protected void OnCarAdded(Car item, DatabaseServiceUpdate update)
        {
            CarUpdated?.Invoke(this, new DatabaseServiceUpdateEventArgs<Car>(item, update));
        }

        //protected void OnBicycleAdded(Bicycle item, DatabaseServiceUpdate update)
        //{
        //    BicycleUpdated?.Invoke(this, new DatabaseServiceUpdateEventArgs<Bicycle>(item, update));
        //}
    }
}