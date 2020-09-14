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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int skip =0, [FromQuery] int take = 20)
        {
            if (skip < 0)
                return BadRequest(new { error = "skip must be a positive number or 0" });

            if (take <= 0)
                return BadRequest(new {error = "take must be a positive number"});

            var users = await _userService.GetAllAsync(skip, take);
            var model = _mapper.Map<IList<UserModel>>(users);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            var model = _mapper.Map<UserModel>(user);
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserModel model)
        {
            if(model.MonthlySalary <=0)
                ModelState.AddModelError("MonthlySalary", "MonthlySalary must be a positive number");

            if (model.MonthlyExpenses <= 0)
                ModelState.AddModelError("MonthlyExpenses", "MonthlyExpenses must be a positive number");

            var userByEmail = await _userService.GetUserByEmailAsync(model.Email);
            if (userByEmail != null)
                ModelState.AddModelError("Email", "User with such email already exists");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(model);
            await _userService.Create(user);

            var userModel = _mapper.Map<UserModel>(user);
            return Ok(userModel);
        }
    }
}
