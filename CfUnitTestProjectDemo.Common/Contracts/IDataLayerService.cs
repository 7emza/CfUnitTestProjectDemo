using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CfUnitTestProjectDemo.Common.Enums;
using CfUnitTestProjectDemo.Common.Models;

namespace CfUnitTestProjectDemo.Common.Contracts
{
    public interface IDataLayerService
    {
        Task<List<Member>> ReadFromFileAsync(FilePathDestination filePath);
        Task WriteToFileAsync(Member member, FilePathDestination filePath);
        Task<List<Member>> UpdateMembersToFileAsync(List<Member> memberList, FilePathDestination filePath);
    }
}
