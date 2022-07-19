using AutoMapper;
using DataLayer.Interface;
using DataLayerDB.DataBaseScaffold;
using eStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;

        public AdminController(IMapper mapper, IMemberRepository memberRepository)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
        }

        public IActionResult ViewMemberList()
        {
            string role = HttpContext.Session.GetString("Role");

            if(role != null)
            {
                if (role.Equals("Admin"))
                {
                    return View("MemberList");
                }
                else
                {
                    return View("AccessDenied");
                }
            }
            else
            {
                return View("AccessDenied");
            }
        }

        public JsonResult GetMemberList()
        {
            var memberList = _mapper.Map<List<MemberViewModel>>(_memberRepository.GetMembers().ToList());
            return new JsonResult(memberList);
        }

        public JsonResult SaveChanges(int MemberID, string Password, string Email, string CompanyName, string City, string Country)
        {
            var status = false;
            var errorMessage = "";

            MemberViewModel member = new MemberViewModel();
            member.MemberId = MemberID;
            member.Email = Email;
            member.CompanyName = CompanyName;
            member.City = City;
            member.Country = Country;
            member.Password = Password;

            if(Password == null)
            {
                try
                {
                    var result = _memberRepository.UpdateMember(_mapper.Map<Member>(member));
                    status = true;
                }
                catch (Exception ex)
                {
                    status = false;
                    errorMessage = ex.Message;
                }
            }
            else
            {
                try
                {
                    var result = _memberRepository.AddMember(_mapper.Map<Member>(member));
                    if(result)
                    {
                        status = true;
                    }else
                    {
                        status = false;
                        errorMessage = "Add failed somehow";
                    }
                }catch(Exception ex)
                {
                    errorMessage = ex.Message;
                    status = false;
                }
            }
           
            return new JsonResult(new
            {
                status = status,
                errorMessage = errorMessage
            });
        }

        public JsonResult DeleteMember(int MemberID)
        {
            string errorMessage = "";
            bool status = false;

            try
            {
                var result = _memberRepository.DeleteMember(MemberID);
                status = true;
            }catch(Exception ex)
            {
                errorMessage = ex.Message;
            }

            return new JsonResult(new
            {
                status = status,
                errorMessage = errorMessage
            });
        }
    }
}
