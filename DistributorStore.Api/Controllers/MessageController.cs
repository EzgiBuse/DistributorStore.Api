using DistributorStore.Base.Response;
using DistributorStore.Data.Domain;
using DistributorStore.Operation.Services.MessageS;
using DistributorStore.Operation.Services.OrderS;
using DistributorStore.Schema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DistributorStore.Api.Controllers
{
    [Authorize(Roles = "Admin, Dealer")]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService service;
        public MessageController(IMessageService service)
        {
            this.service = service;

        }
        [Authorize(Roles = "Admin")]
        [HttpPost("SendMessageAdmin")]
        public ApiResponse SendMessageAdmin([FromBody] Message message)
        {
            var res = service.SendMessageAdmin(message);
            return res;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("SendMessagetoAllAdmin")]
        public ApiResponse SendMessagetoAllAdmin(string message)
        {
            var res = service.SendMessagetoAllAdmin(message);
            return res;
        }
        [Authorize(Roles = "Dealer")]
        [HttpPost("SendMessageDealer")]
        public ApiResponse SendMessageDealer(MessageRequestDealer message)
        {
            var res = service.SendMessageDealer(message);
            return res;
        }
        [Authorize(Roles = "Admin, Dealer")]
        [HttpPost("SendMessageDealer")]
        public ApiResponse<List<Message>> GetMessages(int receiverid)
        {
            var res = service.GetMessages(receiverid);
            return res;
        }
    }
}
