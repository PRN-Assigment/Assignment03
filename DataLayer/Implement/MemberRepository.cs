using DataLayer.Interface;
using DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using DataLayer;

namespace DataLayer.Implement
{
    public class MemberRepository : IMemberRepository
    {
        private eStoreContext _dbContext;
        DbSet<Member> _dbSet { get; set; }

        public MemberRepository(eStoreContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Member>();
        }

        public IQueryable<Member> GetMembers()
        {
            var result = _dbContext.Members;
            return result;
        }
    }
}
