using System.Collections.Generic;
using DatabaseExample.Database.Interfaces;
using DatabaseExample.Model;

namespace DatabaseExample.Database.Fake
{
    public class DatabaseImitation : IDatabase
    {
        private readonly List<Car> _database;

        public DatabaseImitation()
        {
            _database = new List<Car>();
        }

        public void Initialize(IDatabaseInitializer initializer)
        {
            initializer.Initialize(this);
        }

        public void Add(Car car)
        {
            _database.Add(car);
        }

        public Car[] GetAllCars()
        {
            return _database.ToArray();
        }
    }
}