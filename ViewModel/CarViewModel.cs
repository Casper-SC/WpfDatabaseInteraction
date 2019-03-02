using DatabaseExample.Model;
using GalaSoft.MvvmLight;

namespace DatabaseExample.ViewModel
{
    public class CarViewModel : ViewModelBase
    {
        private readonly Car _car;

        public CarViewModel(Car car)
        {
            _car = car;
        }

        public string Model
        {
            get { return _car.Model; }
            set
            {
                if (value == _car.Model)
                    return;
                _car.Model = value;
                RaisePropertyChanged();
            }
        }

        public string Color
        {
            get { return _car.Color; }
            set
            {
                if (value == _car.Color)
                    return;
                _car.Color = value;
                RaisePropertyChanged();
            }
        }

        public Car GetModel()
        {
            return new Car
            {
                Model = Model,
                Color = Color
            };
        }
    }
}