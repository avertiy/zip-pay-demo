using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZipPayDemo.Application.Users.Command.CreateUser;
using ZipPayDemo.Application.Users.Query.GetUser;
using ZipPayDemo.Application.Users.Query.GetUsers;

namespace ZipPayDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetUsersResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Index([FromQuery] GetUsersQuery query)
        {
            var result = await _mediator.Send(query);
            if (result?.Users == null || !result.Users.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromQuery] GetUserQuery query)
        {
            var result = await _mediator.Send(query);
            if (result?.User == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            var result = new ObjectResult(response);
            if (response?.Success == true)
            {
                result.StatusCode = StatusCodes.Status201Created;
            }

            return result;
        }
    }
}
