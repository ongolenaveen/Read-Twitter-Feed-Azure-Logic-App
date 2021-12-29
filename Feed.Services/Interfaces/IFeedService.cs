using System.Collections.Generic;
using System.Threading.Tasks;
using Template.Domain.Models;

namespace Template.Services.Interfaces
{
    public interface IFeedService
    {
        Task<List<Feed>> Get(string hashTag);
    }
}
