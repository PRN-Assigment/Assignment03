﻿
using DataLayer.Interface;
using DataLayerDB.DataBaseScaffold;
using Microsoft.EntityFrameworkCore;


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

        public Member Login(string username, string password)
        {
            var user = _dbSet.FirstOrDefault(x => x.Email.Equals(username) && x.Password.Equals(password));

            return user;
        }
    }
}
