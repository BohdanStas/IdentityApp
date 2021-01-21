using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DAL.Data
{
   public class Initializer 
   {
       public static async Task InitData(DataContext context, UserManager<User> manager)
       {
           if (!manager.Users.Any())
           {
               var users = new List<User>()
               {
                   new User()
                   {
                       Email = "test@gmail.com",
                       UserName = "test",
                   },
                   new User()
                   {
                       Email = "test2@gmail.com",
                       UserName = "test2",
                   },
                   new User()
                   {
                       Email = "test3@gmail.com",
                       UserName = "test3",
                   },
               };

               foreach (var user in users)
               {
                   await manager.CreateAsync(user, "Pa$$w0rd");
               }

           }
       }
   }
}
