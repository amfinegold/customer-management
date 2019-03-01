using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CustomerWidget.Models.Models;

namespace CustomerWidget.Repository.Interfaces
{
    public interface IDataSource
    {
        Task<IEnumerable<T>> GetCollectionAsync<T>(string collection)
            where T : BaseDocument;
        Task<T> GetCollectionItemAsync<T>(string collection, int key)
            where T : BaseDocument;
        Task<T> AddCollectionItemAsync<T>(string collection, T item)
            where T : BaseDocument;
        Task UpdateCollectionItemAsync<T>(string collection, T item)
            where T : BaseDocument;
        Task DeleteCollectionItemAsync<T>(string collection, int id)
            where T : BaseDocument;
    }
}
