using AutoMapper;
using DataLayer.Interface;
using eStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IMapper mapper, IMemberRepository memberRepository)
        {
            _logger = logger;
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult GetAll()
        {
            var list = _memberRepository.GetMembers().ToList();

            Console.WriteLine(list);

            return new JsonResult(list);
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        public JsonResult LoginUser(string Username, string Password)
        {
            string errorMessage = "";
            bool status = false;
            MemberViewModel member = null;
            try
            {
                member = _mapper.Map<MemberViewModel>(_memberRepository.Login(Username, Password));
                if(member == null)
                {
                    status = false;
                    errorMessage = "Invalid Username or Password";
                }
                else
                {
                    status = true;
                }
            } catch(Exception ex)
            {
                errorMessage = ex.Message;
                status = false;
            }

            return new JsonResult(new
            {
                status = status,
                errorMessage = errorMessage,
                member = member
            });
        }
    }
}
