using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab1
{
    internal sealed class Chef
    {
        TaskContainer container;
        ListOfWaiters listOfWaiters;
        MainWindow window;
        readonly int numberOfThreads = 4;
        public readonly System.Threading.Tasks.Task[] threadTasks;
        readonly AutoResetEvent taskAddedEvent;

        internal static CancellationTokenSource cts = new CancellationTokenSource();
        internal static CancellationToken token = cts.Token;

        Random rand;
        public Chef(MainWindow window, TaskContainer container, ListOfWaiters listOfWaiters) {
            (this.container, this.listOfWaiters, this.window, taskAddedEvent, rand) =
                (container, listOfWaiters, window, new(false), new());
            container.TaskAdded += TaskAdded;
            threadTasks = new System.Threading.Tasks.Task[numberOfThreads];

            for (int i = 0; i < numberOfThreads; i++)
            {
                threadTasks[i] = System.Threading.Tasks.Task.Run(() => ProcessTasks(token), token);
            }
        }
        void TaskAdded(object sender, TaskAddedEventArgs e) => taskAddedEvent.Set();
        async void ProcessTasks(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                taskAddedEvent.WaitOne();
                await DoTask();
            }
        }
        async System.Threading.Tasks.Task DoTask()
        {
            Task _task = ExcludeTask();
            await System.Threading.Tasks.Task.Delay(rand.Next(5000));
            window.Dequeue(1, _task.name);
            Customer customer = listOfWaiters.Find(_task);
            window.Dequeue(2, customer.name);
            ExcludeCustomer(customer);
            taskAddedEvent.Reset();
        }
        void ExcludeCustomer(Customer customer) => listOfWaiters.Remove(customer);
        Task ExcludeTask() => container.Dequeue();
    }
}
