using CfUnitTestProjectDemo.Contracts.Repositories;
using CfUnitTestProjectDemo.Models;
using CfUnitTestProjectDemo.Persistances.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfUnitTestProjectDemo.Persistances
{
    public class MemberRepository : RepositoryBase<Member>, IMemberRepository
    {
        public async Task<List<Member>> ReadFromFileAsync( File_PATH_DESTINATION file_PATH)
        {
            var FILE_PATH = GetFile_PATH(file_PATH);
            List<Member> members = new List<Member>();
            using (StreamReader fileReader = File.OpenText(FILE_PATH))
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

        public async Task<List<Member>> UpdateMembersToFileAsync(List<Member> memberList, File_PATH_DESTINATION file_PATH)
        {
            var FILE_PATH = GetFile_PATH(file_PATH);

            using (StreamWriter updateWriter = new StreamWriter(FILE_PATH, append: false))
            {
                foreach (Member memberUpdate in memberList)
                {
                    await updateWriter.WriteLineAsync($"{memberUpdate.Id} {memberUpdate.FullName} {memberUpdate.Email}");
                }
                updateWriter.Close();
            }
            return memberList;
        }

        public async Task WriteToFileAsync(Member member, File_PATH_DESTINATION file_PATH)
        {
            var FILE_PATH = GetFile_PATH(file_PATH);

            using (StreamWriter fileWriter = new StreamWriter(FILE_PATH, append: true))
            {
                await fileWriter.WriteLineAsync($"{member.Id} {member.FullName} {member.Email}");
                fileWriter.Close();
            }
        }

        private string GetFile_PATH(File_PATH_DESTINATION file_PATH)
        {
            var FILE_PATH = "/";
            if (file_PATH == File_PATH_DESTINATION.Accepted)
            {
                FILE_PATH = ACCEPTED_FILE_PATH;
            }
            else if (file_PATH == File_PATH_DESTINATION.Rejected)
            {
                FILE_PATH = REJECTED_FILE_PATH;
            }
            return FILE_PATH;
        }
    }
    public enum File_PATH_DESTINATION
    {
        Accepted,
        Rejected,
    }
}
