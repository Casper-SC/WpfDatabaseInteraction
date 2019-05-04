using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DatabaseExample.Model;
using DatabaseExample.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NLog;

namespace DatabaseExample.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private DatabaseService _databaseService;
        private bool _isInitialized;
        private bool _isCarAdditionInProgress;

        public ObservableCollection<CarViewModel> Cars { get; }

        public RelayCommand AddCarCommand => new RelayCommand(Add);

        public CarViewModel CurrentCar { get; }

        public bool IsInitialized
        {
            get { return _isInitialized; }
            set { Set(ref _isInitialized, value); }
        }

        public bool IsCarAdditionInProgress
        {
            get { return _isCarAdditionInProgress; }
            set { Set(ref _isCarAdditionInProgress, value); }
        }

        public MainViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _databaseService.CarUpdated += OnDatabaseServiceCarUpdated;

            Cars = new ObservableCollection<CarViewModel>();

            CurrentCar = new CarViewModel(new Car() { Color = "Цвет", Model = "Модель" });
        }

        private void OnDatabaseServiceCarUpdated(DatabaseService sender, DatabaseServiceUpdateEventArgs<Car> args)
        {
            _logger.Trace($"{nameof(OnDatabaseServiceCarUpdated)}. {nameof(args.UpdateInfo)} = {args.UpdateInfo.ToString()}");

            switch (args.UpdateInfo)
            {
                case DatabaseServiceUpdate.Added:
                    foreach (var car in args.Data)
                    {
                        var viewModel = new CarViewModel(car);
                        Cars.Add(viewModel);

                        _logger.Debug($"{nameof(Add)}. Car added: {viewModel.Model}, {viewModel.Model}.");
                    }
                    break;

                case DatabaseServiceUpdate.Deleted:
                    // Cars.Remove(...)
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(args) + "." + nameof(args.UpdateInfo));
            }
        }

        private async void Add()
        {
            _logger.Trace($"{nameof(Add)}.");

            IsCarAdditionInProgress = true;

            try
            { 
                // Ждём, пока якобы добавляются данные из БД.
                await Task.Delay(1000);
                // Если хочется, чтобы в UI данные появлялись сразу, нужно это реализовать.
                await _databaseService.AddAsync(CurrentCar.GetModel());
            }
            catch (Exception ex)
            {
                _logger.Error($"{nameof(Add)}.", ex.ToString());
            }
            finally
            {
                IsCarAdditionInProgress = false;
            }
        }

        public async Task InitializeAsync()
        {
            _logger.Trace($"{nameof(InitializeAsync)}.");

            await _databaseService.InitializeAsync();

            await Task.Delay(2000); // Ждём, пока якобы грузятся данные из БД.

            var cars = await _databaseService.GetAllCarsAsync();
            foreach (var car in cars)
            {
                Cars.Add(new CarViewModel(car));
            }

            IsInitialized = true;
        }
    }
}