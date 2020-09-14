using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestProject.WebAPI.Contracts;
using TestProject.WebAPI.Data.Entities;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountsController(IAccountService accountService, IMapper mapper, IUserService userService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int skip=0, int take =20)
        {
            var users = await _accountService.GetAllAsync(skip, take);
            var model = _mapper.Map<IList<AccountModel>>(users);
            return Ok(model);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(long userId)
        {
            var account = await _accountService.GetByUserIdAsync(userId);
            if (account == null)
                return NotFound();
            var model = _mapper.Map<AccountModel>(account);
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountModel model)
        {
            var user = await _userService.GetByIdAsync(model.UserId);
            if (user == null)
            {
                ModelState.AddModelError("UserId", "User does not exist");
                return BadRequest(ModelState);
            }

            if (user.MonthlySalary - user.MonthlyExpenses < 1000)
                ModelState.AddModelError("UserId", "User does not meet account requirements");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = new Account {UserId = user.Id};
            await _accountService.CreateAccount(account);
            return Ok();
        }
    }
}