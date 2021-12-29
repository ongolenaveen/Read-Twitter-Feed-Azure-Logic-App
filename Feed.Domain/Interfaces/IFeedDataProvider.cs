using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Domain.Models;

namespace Template.Domain.Interfaces
{
    public interface IFeedDataProvider
    {
        Task<List<Feed>> GetFeed(string hashTag);
    }
}
