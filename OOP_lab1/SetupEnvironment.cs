using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab1
{
    internal sealed class SetupEnvironment
    {
        LineIn lineIn;
        TaskContainer taskContainer;
        ListOfWaiters listOfWaiters;
        OrderTaker orderTaker;
        Chef chef;
        public SetupEnvironment(MainWindow window) {
            lineIn = new(window, false);
            taskContainer = new();
            listOfWaiters = new();
            orderTaker = new(window, lineIn, taskContainer, listOfWaiters);
            chef = new(window, taskContainer, listOfWaiters);
        }
        public void Run()
        {
            OrderTaker.cts = new();
            Chef.cts = new();
            lineIn.isActive = true;
        }
        public void Stop()
        {
            lineIn.isActive = false;
            OrderTaker.cts.Cancel();
            OrderTaker.cts.Dispose();
            Chef.cts.Cancel();
            Chef.cts.Dispose();
        }
    }
}
