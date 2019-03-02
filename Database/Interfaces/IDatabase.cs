using DatabaseExample.Model;

namespace DatabaseExample.Database.Interfaces
{
    public interface IDatabase
    {
        void Initialize(IDatabaseInitializer initializer);
        void Add(Car Car);
        Car[] GetAllCars();
    }
}