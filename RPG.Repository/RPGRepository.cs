using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPG.Domain.Entities;
using RPG.Domain.Util;

namespace RPG.Repository
{
    public class RPGRepository : IRPGRepository
    {
        private RPGContext _context { get; set; }
        public RPGRepository(RPGContext ctx)
        {
            _context = ctx;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public int Count<T>() where T : class
        {
            return _context.Characters.Count();
        }   

        public async Task<Character[]> GetAllCharactersAsync(Pagination pagination)
        {
            IQueryable<Character> query = _context.Characters.Include(i =>i.Moves).Include(i =>i.Weapons);
            query = query.OrderBy(o => o.Name)
                         .Skip((pagination.pageNumber - 1) * pagination.pageSize)
                         .Take(pagination.pageSize).AsQueryable<Character>();
            return await query.ToArrayAsync();
        }

        public async Task<Character> GetCharacterById(int CharacterId)
        {
            IQueryable<Character> query = _context.Characters.Include(i =>i.Moves).Include(i =>i.Weapons);
            query = query.Where(w=> w.Id == CharacterId).OrderByDescending(o => o.Name);
            return await query.FirstOrDefaultAsync();
        }
    }
}