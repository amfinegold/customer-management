using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CustomerWidget.Models.Models;

namespace CustomerWidget.Repository.Interfaces
{
    public interface IDataSource
    {
        Task<T> GetCollectionItemAsync<T>(string collection, string key)
            where T : BaseDocument;

    }
}
