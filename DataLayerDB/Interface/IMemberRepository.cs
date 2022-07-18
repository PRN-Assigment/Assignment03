using DataLayerDB.DataBaseScaffold;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface
{
    public interface IMemberRepository
    {
        IQueryable<Member> GetMembers();
        Member Login(string username, string password);
    }
}
