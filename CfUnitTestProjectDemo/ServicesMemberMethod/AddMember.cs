using CfUnitTestProjectDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfUnitTestProjectDemo.ServicesMemberMethod
{
    public class AddMember
    {
        /// <summary>
        /// Method that creates a new member.
        /// This method is using WriteToFileAsync method to write it to file.
        /// </summary>
        /// <param name="firstname">Firstname of Member</param>
        /// <param name="lastname">Lastname of Member</param>
        /// <param name="email">Email of Member</param>
        /// <param name="filepath">Filepath for textfile</param>
        /// <returns>The new Member</returns>
        public async Task<Member> AddMemberAsync(string firstname, string lastname, string email, string filepath)
        {
            Member member = new Member
            {
                Id = Guid.NewGuid(),
                FirstName = firstname,
                LastName = lastname,
                Email = email
            };
            WriteAsync write = new WriteAsync();
            await write.WriteToFileAsync(member, filepath);
            return member;
        }
    }
}
