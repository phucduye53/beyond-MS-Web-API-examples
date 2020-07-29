using System;
using System.Threading.Tasks;
using DemoApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace DemoApi.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        private readonly TodoContext _context;

        public ApiControllerBase(TodoContext context)
        {
            _context = context;
        }
        [NonAction] //need decorator because Swagger is trying to build this method and falling into error
        public new async Task<IActionResult> Response(IBaseCommandResult baseCommandResult)
        {
            if (baseCommandResult.Success)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        Success = true,
                        Message = baseCommandResult.Message,
                        ResponseDataObj = baseCommandResult.ResponseDataObj
                    });
                }
                catch (Exception err)
                {
                    _context.Database.RollbackTransaction();
                    return BadRequest(new
                    {
                        Success = false,
                        Message = baseCommandResult.Message,
                        ResponseDataObj = new[] { "A Internal-Server error occured: " + err.Message.ToString() }
                    });
                }
            }
            else
            {
                return Ok(new
                {
                    Success = false,
                    Message = baseCommandResult.Message,
                    ResponseDataObj = baseCommandResult.ResponseDataObj
                });
            }
        }
    }
}