using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab1
{
    public sealed class Task
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public Task() => this.id = Guid.NewGuid();
    }
}
