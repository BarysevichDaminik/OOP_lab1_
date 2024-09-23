namespace OOP_lab1
{
    internal sealed class ListOfWaiters
    {
        List<Customer> _customer;
        public ListOfWaiters() => _customer = new();
        public Customer this[Guid id]
        {
            #pragma warning disable CS8603 
            get => _customer?.Find(c => c.id == id);
            #pragma warning restore CS8603
            set
            {
                #pragma warning disable CS8604
                int index = _customer.IndexOf(_customer.Find(c => c.id == id));
                #pragma warning restore CS8604
                _customer[index] = value;
            }
        }
        public void Add(Customer customer) => _customer.Add(customer);
        public void Remove(Customer customer) => _customer.Remove(customer);
        #pragma warning disable CS8603
        public Customer Find(Task task) => _customer.Find(c => c.task == task);
        #pragma warning restore CS8603
    }
}
