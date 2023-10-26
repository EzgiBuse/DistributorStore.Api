using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributorStore.Data.Domain
{
    //This class represents the User table in the application database

    [Table("User",Schema ="dbo")]
    public class User
    {
       
        public int UserID { get; set; }

        public string UserName { get; set; }

        
        
        public string Password { get; set; }

        
        public UserRole Role { get; set; }

    }
    //This enum represents user roles, Admin or dealer.
    public enum UserRole
    {
        Admin,
        Dealer
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //User Table Configuration
            builder.HasKey(u => u.UserID);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(20);
            builder.Property(u => u.Role).IsRequired();

            
        }
    }
}
