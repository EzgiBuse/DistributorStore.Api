using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DistributorStore.Data.Domain
{
    [Table("Message", Schema = "dbo")]
    public class Message
    {
        //Message table in database
        public int MessageID { get; set; }

       
        public int Sender { get; set; }

       
        public int Receiver { get; set; }

       
        public string Content { get; set; }

        public DateTime Date { get; set; }
    }
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            //Configuration of Message table in database
            builder.HasKey(m => m.MessageID);
            builder.Property(m => m.Sender).IsRequired();
            builder.Property(m => m.Receiver).IsRequired();
            builder.Property(m => m.Content).IsRequired();
            builder.Property(m => m.Date);

           
            builder.Property(m => m.Content).HasMaxLength(255);

            
        }
    }
}
