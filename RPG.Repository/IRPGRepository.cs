using System.Threading.Tasks;
using RPG.Domain.Entities;
using RPG.Domain.Util;

namespace RPG.Repository
{
    public interface IRPGRepository
    {
        void Add<T>(T entity) where T: class;
        void Update<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        int Count<T>() where T: class;
        Task<bool> SaveChangesAsync();
        Task<Character[]> GetAllCharactersAsync(Pagination pagination);
        Task<Character> GetCharacterById(int RickId);
    }
}