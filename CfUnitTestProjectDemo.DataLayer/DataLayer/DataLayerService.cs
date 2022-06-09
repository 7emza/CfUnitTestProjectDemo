using CfUnitTestProjectDemo.Common.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CfUnitTestProjectDemo.Common.Enums;
using CfUnitTestProjectDemo.Common.Models;

namespace CfUnitTestProjectDemo.DataLayer.DataLayer
{
    public class DataLayerService : IDataLayerService
    {
        protected string ACCEPTED_FILE_PATH =
            $@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}/CfUnitTestProjectDemo/AcceptedMembers.txt";

        protected string REJECTED_FILE_PATH =
            $@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}/CfUnitTestProjectDemo/RejectedMembers.txt";

        public async Task<List<Member>> ReadFromFileAsync(FilePathDestination filePath)
        {
            var path = GetFile_PATH(filePath);
            var members = new List<Member>();
            using (var fileReader = File.OpenText(path))
            {
                var text = await fileReader.ReadToEndAsync();
                var lines = text.Split('\n');

                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;
                    var lineArray = line.Split(' ');
                    var member = new Member();
                    var guid = Guid.Parse(lineArray[0]);
                    member.Id = guid;
                    member.FirstName = lineArray[1];
                    member.LastName = lineArray[2];
                    member.Email = lineArray[3];
                    members.Add(member);
                }

                fileReader.Close();
            }

            return members;
        }

        public async Task<List<Member>> UpdateMembersToFileAsync(List<Member> memberList, FilePathDestination file_PATH)
        {
            var path = GetFile_PATH(file_PATH);

            using (var updateWriter = new StreamWriter(path, append: false))
            {
                foreach (var memberUpdate in memberList)
                {
                    await updateWriter.WriteLineAsync(
                        $"{memberUpdate.Id} {memberUpdate.FullName} {memberUpdate.Email}");
                }

                updateWriter.Close();
            }

            return memberList;
        }

        public async Task WriteToFileAsync(Member member, FilePathDestination file_PATH)
        {
            var path = GetFile_PATH(file_PATH);

            using (var fileWriter = new StreamWriter(path, append: true))
            {
                await fileWriter.WriteLineAsync($"{member.Id} {member.FullName} {member.Email}");
                fileWriter.Close();
            }
        }

        private string GetFile_PATH(FilePathDestination filePath)
        {
            var path = "/";
            switch (filePath)
            {
                case FilePathDestination.Accepted:
                    path = ACCEPTED_FILE_PATH;
                    break;
                case FilePathDestination.Rejected:
                    path = REJECTED_FILE_PATH;
                    break;
            }

            return path;
        }
    }
}
