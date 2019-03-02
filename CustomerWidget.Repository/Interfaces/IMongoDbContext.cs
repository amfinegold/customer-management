using System;
using System.Collections.Generic;
using System.Text;
using CustomerWidget.Models.Models;
using MongoDB.Driver;

namespace CustomerWidget.Repository.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<Agent> Agents { get; }
        IMongoCollection<Customer> Customers { get; }
    }
}
