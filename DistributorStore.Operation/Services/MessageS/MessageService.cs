using DistributorStore.Base.Response;
using DistributorStore.Data.Domain;
using DistributorStore.Data.Uow;
using DistributorStore.Schema;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorStore.Operation.Services.MessageS
{
    public class MessageService
    {
        private readonly IUnitOfWork unitofwork;
        public MessageService(IUnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }

        public ApiResponse SendMessageAdmin(Message message)
        {
           unitofwork.MessageRepository.Insert(message);
            unitofwork.MessageRepository.Save();
            return new ApiResponse();
        }
        public ApiResponse SendMessagetoAllAdmin(string message)
        {
            var alldealers = unitofwork.DealerRepository.GetAll();
            foreach(var dealer in alldealers)
            {
                var newMessage = new Message
                {
                    Sender = 11, // Admin ID in database
                    Receiver = dealer.DealerID, // receiver id
                    Content = message,
                    Date = DateTime.Now 
                };
                unitofwork.MessageRepository.Insert(newMessage);
            }
            
            unitofwork.MessageRepository.Save();
            return new ApiResponse();
        }

        public ApiResponse SendMessageDealer(MessageRequestDealer messagerequest)
        {
            if(messagerequest.Dealerid ==null || messagerequest.message == null)
            {
                return new ApiResponse("incomplete data");
            }

            var newMessage = new Message
            {
                Sender = messagerequest.Dealerid, 
                Receiver = 11, //Admin ID in database
                Content = messagerequest.message,
                Date = DateTime.Now
            };
            unitofwork.MessageRepository.Insert(newMessage);
            unitofwork.MessageRepository.Save();
            return new ApiResponse();
        }

        public ApiResponse<List<Message>> GetMessages(int receiverid)
        {
            if (receiverid == null)
            {
                return new ApiResponse<List<Message>>("receiver id is incoplete");
            }

            
            List<Message> messages = unitofwork.MessageRepository.Where(m =>m.Receiver == receiverid).ToList();
            if (messages.IsNullOrEmpty())
            {
                return new ApiResponse<List<Message>>("Messagebox is empty");
            }


            return new ApiResponse<List<Message>>(messages);
        }
    }
}
