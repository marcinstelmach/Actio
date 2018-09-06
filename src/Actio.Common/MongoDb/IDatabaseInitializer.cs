using System.Threading.Tasks;

namespace Actio.Common.MongoDb
{
    public interface IDatabaseInitializer
    {
        Task InitializeDatabaseAsync();
    }
}
