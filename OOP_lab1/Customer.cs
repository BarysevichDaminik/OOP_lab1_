using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lab1
{
    internal sealed class Customer
    {
        public required Guid id { get; init; }
        public string name { get; set; }
        public Task task { get; set; }
    }
}
