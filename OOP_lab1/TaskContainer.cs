using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab1
{
    internal sealed class TaskContainer
    {
        BlockingCollection<Task> tasks = new();
        public bool IsAny { get => tasks.Any(); }
        public void Enqueue(Task task) { tasks.Add(task); OnAdding(new TaskAddedEventArgs()); }
        public Task Dequeue() => tasks.Take();
        public event EventHandler<TaskAddedEventArgs> TaskAdded;
        void OnAdding(TaskAddedEventArgs e) => TaskAdded?.Invoke(this, e);
    }
}
