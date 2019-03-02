using System.Collections.Generic;
using DatabaseExample.Database.Interfaces;
using DatabaseExample.Model;

namespace DatabaseExample.Database.Fake
{
    public class DatabaseInitializerDebug : IDatabaseInitializer
    {
        public void Initialize(IDatabase database)
        {
            var cars = new List<Car>()
            {
                new Car() { Model="Жигули", Color="Жёлтый" },
                new Car() { Model="Хонда", Color="Серый" },
                new Car() { Model="Крутая Машина", Color="Чёрный" }
            };

            foreach (var car in cars)
            {
                database.Add(car);
            }
        }
    }
}
