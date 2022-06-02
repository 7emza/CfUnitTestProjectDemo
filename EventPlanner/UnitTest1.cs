using CfUnitTestProjectDemo.Contracts.Repositories;
using CfUnitTestProjectDemo.Models;
using CfUnitTestProjectDemo.Persistances;
using CfUnitTestProjectDemo.ServicesMemberMethod;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace EventPlanner
{
    public class UnitTest1
    {

        protected string ACCEPTED_FILE_PATH = $@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}/CfUnitTestProjectDemo/AcceptedMembers.txt";
        protected string REJECTED_FILE_PATH = $@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}/CfUnitTestProjectDemo/RejectedMembers.txt";

        private readonly IMemberRepository _memberRepository;


        [Fact]
        public async void AddMember_Test()
        {
            //await _memberRepository.WriteToFileAsync(new Member
            //{
            //    FirstName = "hamza test",
            //    LastName = "last name test",
            //    Email = "email test",
            //}, File_PATH_DESTINATION.Accepted);

            //Arrange
            AddMember sut = new AddMember();

            //Act
            Member member = await sut.AddMemberAsync("Aleksandar", "Todorovic", "a@domain.com", ACCEPTED_FILE_PATH);

            //Assert
            Assert.NotNull(member);
            Assert.True(member.Id.ToString().Length > 0);
        }
    }
}
