using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMS_Auth.Application.Features.AuthFeatures.Command.Commands;
using SMS_Auth.Domain.Dtos;
using SMS_Auth.Domain.Entities;

namespace SMS_Auth.Api.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Props
        private readonly IMediator _mediator;
        protected ApiResponse _response;
        #endregion
        #region Constructor
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
            _response = new();
        }
        #endregion
        #region Methods
        [HttpPost("/login")]
        public async Task<IActionResult> login([FromForm] LoginRequestCommand request )
        {
            LoginResponseDto res = await _mediator.Send(request);
            if (res != null)
            {
                _response.HttpStatus = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = res;
                return Ok(_response);
            }
            _response.HttpStatus = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Invalid information");
            return BadRequest(_response);
        }
        [HttpPost("/Register")]
        public async Task<IActionResult> register([FromForm] RegisterRequestCommand request)
        {
            IdentityResult res = await _mediator.Send(request);
            if (res.Succeeded)
            {
                _response.HttpStatus = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = new { SuccessMessage = "User Created Successfully" };
                return Ok(_response);
            }
            _response.HttpStatus = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add($"Error during registration : {res.Errors.Select(e => e.Description)}");
            _response.Result = null;
            return BadRequest(_response);
        }
        #endregion
    }
}
