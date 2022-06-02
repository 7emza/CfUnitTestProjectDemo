using CfUnitTestProjectDemo.Models;
using CfUnitTestProjectDemo.Models.Base;
using CfUnitTestProjectDemo.Persistances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfUnitTestProjectDemo.Contracts.Repositories
{
    public interface IMemberRepository
    {
        Task<List<Member>> UpdateMembersToFileAsync(List<Member> memberList, File_PATH_DESTINATION file_PATH);
        Task<List<Member>> ReadFromFileAsync(File_PATH_DESTINATION file_PATH);
        Task WriteToFileAsync(Member member, File_PATH_DESTINATION file_PATH);
    }
}
