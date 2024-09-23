using System.Collections.Concurrent;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace OOP_lab1
{
    internal sealed class LineIn
    {
        BlockingCollection<Customer> customers;
        MainWindow window;
        bool _isActive;
        public LineIn(MainWindow window, bool isActive) => (this.customers, this.isActive, this.window) = (new(), isActive, window);
        async void StartAdding()
        {
            while (_isActive)
            {
                Customer _customer = new() { id = Guid.NewGuid(), name = $"Customer {++MainWindow.CustomersCount}" };

                Label label1 = (Label)window.FindName("label1");
                customers.Add(_customer);
                window.Enqueue(0, _customer.name);
                OnCreating(new CustomerAddedEventArgs());
                await System.Threading.Tasks.Task.Delay(1000);
            }
        }
        public bool isActive 
        {
            get => _isActive;
            set 
            {  
                _isActive = value;
                if (_isActive) StartAdding();
            }
        }
        public event EventHandler<CustomerAddedEventArgs> CustomerAdded;
        void OnCreating(CustomerAddedEventArgs e) => CustomerAdded?.Invoke(this, e);
        public void Enqueue(Customer customer) {customers.Add(customer); customers.CompleteAdding();}
        public Customer Dequeue() => customers.Take();
    }
}
