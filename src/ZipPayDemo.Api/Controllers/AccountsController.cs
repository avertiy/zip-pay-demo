using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZipPayDemo.Application.Accounts.Command.CreateAccount;
using ZipPayDemo.Application.Accounts.Query.GetAccount;
using ZipPayDemo.Application.Accounts.Query.GetAccounts;

namespace ZipPayDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetAccountsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Index([FromQuery] GetAccountsQuery query)
        {
            var result = await _mediator.Send(query);
            if (result?.Accounts == null || !result.Accounts.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(GetAccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromQuery] GetAccountQuery query)
        {
            var result = await _mediator.Send(query);
            if (result?.Account == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateAccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateAccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
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