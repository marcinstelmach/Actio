using System.Threading.Tasks;

namespace Actio.Common.MongoDb
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}
