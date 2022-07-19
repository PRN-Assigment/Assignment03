
using DataLayer.Interface;
using DataLayerDB.DataBaseScaffold;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Implement
{
    public class MemberRepository : IMemberRepository
    {
        private eStoreContext _dbContext;
        DbSet<Member> _dbSet { get; set; }

        public MemberRepository(eStoreContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Members;
        }

        public IQueryable<Member> GetMembers()
        {
            var admin = GetInitAdmin();
            var result = _dbSet.Where(x => !x.Email.Equals(admin.Email));
            return result;
        }
        public bool AddMember(Member member)
        {
            try
            {
                int id = GetCurrentLastID() + 1;
                member.MemberId = id;
                _dbSet.Add(member);
                _dbContext.SaveChanges();
                return true;
            } catch(Exception ex)
            {
                return false;
            }
        }

        public bool UpdateMember(Member member)
        {
            var target = _dbSet.FirstOrDefault(x => x.MemberId == member.MemberId);
            if(target != null)
            {
                target.Email = member.Email;
                target.CompanyName = member.CompanyName;
                target.City = target.City;
                target.Country = member.Country;
                if(member.Password != null)
                {
                    target.Password = member.Password;
                }
                _dbSet.Update(target);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteMember(int id)
        {
            var target = _dbSet.FirstOrDefault(x => x.MemberId == id);
            if(target != null)
            {
                _dbSet.Remove(target);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Member GetMemberByID(int id)
        {
            var member = _dbSet.FirstOrDefault(x => x.MemberId == id);
            return member;
        }

        public int GetCurrentLastID()
        {
            return _dbSet.Count();
        }

        public Member Login(string username, string password)
        {
            InitAdmin();

            var user = _dbSet.FirstOrDefault(x => x.Email.Equals(username) && x.Password.Equals(password));

            return user;
        }

        public void InitAdmin()
        {
            Member admin = GetInitAdmin();
            var member = _dbSet.FirstOrDefault(x => x.Email.Equals(admin.Email));
            if(member == null)
            {
                AddMember(admin);
            }
            else
            {
                return;
            }
        }

        public Member GetInitAdmin()
        {
            Member admin = new Member();

            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
            string email = config["DefaultUser:email"];
            string password = config["DefaultUser:password"];
            string companyName = config["DefaultUser:companyName"];
            string city = config["DefaultUser:companyName"];
            string country = config["DefaultUser:country"];

            admin.Email = email;
            admin.Password = password;
            admin.CompanyName = companyName;
            admin.City = city;
            admin.Country = country;

            return admin;
        }
    }
}
