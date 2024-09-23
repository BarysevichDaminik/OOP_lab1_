namespace OOP_lab1
{
    internal sealed class OrderTaker
    {
        TaskContainer container;
        LineIn lineIn;
        ListOfWaiters ListOfWaiters;
        MainWindow window;
        readonly int numberOfThreads;
        public readonly System.Threading.Tasks.Task[] threadTasks;

        internal static CancellationTokenSource cts = new CancellationTokenSource();
        internal static CancellationToken token = cts.Token;

        readonly AutoResetEvent customerAddedEvent;
        public OrderTaker(MainWindow window, LineIn lineIn, TaskContainer container, ListOfWaiters listOfWaiters)
        {
            (this.container, this.lineIn, this.ListOfWaiters, numberOfThreads, customerAddedEvent, this.window) =
                (container, lineIn, listOfWaiters, 4, new(false), window);
            lineIn.CustomerAdded += this.CustomerAdded;
            threadTasks = new System.Threading.Tasks.Task[numberOfThreads];
            for (int i = 0; i < numberOfThreads; i++)
            {
                threadTasks[i] = System.Threading.Tasks.Task.Run(() => ProcessCustomers(token), token);
            }
        }
        void CustomerAdded(object sender, CustomerAddedEventArgs e) => customerAddedEvent.Set();
        async System.Threading.Tasks.Task ProcessCustomers(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                customerAddedEvent.WaitOne();
                await System.Threading.Tasks.Task.Delay(4000);
                Customer customer = lineIn.Dequeue();
                window.Dequeue(0);
                customer.task = new();
                customer.task.name = $"Task {++MainWindow.TasksCount}";
                container.Enqueue(customer.task);
                ListOfWaiters.Add(customer);
                window.Enqueue(1, customer.task.name);
                window.Enqueue(2, customer.name);
                customerAddedEvent.Reset();
            }
        }
    }
}
