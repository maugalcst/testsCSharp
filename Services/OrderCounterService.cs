namespace DependencyInjection.Services
{
    public class OrderCounterService
    {
        private int _counter = 0;

        public int AddOrder()
        {
            _counter ++;
            return _counter;
        }
    }
}