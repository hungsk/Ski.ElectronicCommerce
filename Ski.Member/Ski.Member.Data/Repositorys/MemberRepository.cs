using Microsoft.EntityFrameworkCore;
using Ski.Member.Domain.Entities;

namespace Ski.Member.Data
{
    public class MemberRepository : IMemberRepository
    {
        private readonly MemberDbContext _context;

        public MemberRepository(MemberDbContext context)
        {
            _context = context;
        }

        public bool MemberExists(string id)
        {
            return _context.Member.Any(e => e.me_id == id);
        }

        public async void CreateAsync(MemberModel member)
        {
            _context.Member.Add(member);
            await _context.SaveChangesAsync();
        }

        public async void DeleteAsync(MemberModel member)
        {
            if (member == null)
            {
                throw new ArgumentNullException("查無此會員");
            }
            else
            {
                _context.Entry(member).State = EntityState.Deleted;
                await SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MemberModel>> GetAllAsync()
        {
            return await _context.Member.ToListAsync();
        }

        public async Task<MemberModel> GetAsync(int id)
        {
            return await _context.Member.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async void UpdateAsync(MemberModel member)
        {
            _context.Entry(member).State = EntityState.Modified;
            await SaveChangesAsync();
        }
    }
}