using CfUnitTestProjectDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfUnitTestProjectDemo.ServicesMemberMethod
{
    public class ReadAsync
    {
        /// <summary>
        /// Method that reads a textfile to end and then splits it into rows.
        /// Recreating the members from file and putting them in a list.
        /// </summary>
        /// <param name="filepath">Filepath for textfile</param>
        /// <returns>List of the recreated Members</returns>
        public async Task<List<Member>> ReadFromFileAsync(string filepath)
        {
            List<Member> members = new List<Member>();
            using (StreamReader fileReader = File.OpenText(filepath))
            {
                string text = await fileReader.ReadToEndAsync();
                string[] lines = text.Split('\n');

                foreach (string line in lines)
                {
                    if (line != null && line.Length != 0)
                    {
                        string[] lineArray = line.Split(' ');
                        Member member = new Member();
                        Guid guid = Guid.Parse(lineArray[0]);
                        member.Id = guid;
                        member.FirstName = lineArray[1];
                        member.LastName = lineArray[2];
                        member.Email = lineArray[3];
                        members.Add(member);
                    }
                }
                fileReader.Close();
            }
            return members;
        }

    }
}
