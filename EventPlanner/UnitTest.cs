
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using CfUnitTestProjectDemo.Common.Contracts;
using CfUnitTestProjectDemo.Common.Models;
using CfUnitTestProjectDemoUI.ViewModels;

namespace EventPlanner
{
    public class UnitTest
    {
        public UnitTest()
        {
            //Arrange
            HomeVm = new HomeViewModel(A.Fake<IDataLayerService>());
            FakeMember = new Member
            {
                Id = new Guid(),
                FirstName = "fakeFirstName",
                LastName = "fakeLastName",
                Email = null
            };
        }

        public HomeViewModel HomeVm { get; set; }
        public Member FakeMember { get; set; }

        [Fact]
        public void MemoryLeak_Test()
        {
            var weakRef = new List<WeakReference>
            {
                new(HomeVm.FirstName),
                new(HomeVm.LastName),
                new(HomeVm.Email),
            };
            // Ryn an operation with leakyObject  
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            foreach (var weakRefProp in weakRef)
            {
                Assert.False(weakRefProp.IsAlive);
            }
        }

        [Fact]
        public void IsFieldsValid_Test()
        {

            //Act
            HomeVm.FirstName = FakeMember.FirstName;
            HomeVm.LastName = FakeMember.LastName;
            HomeVm.Email = FakeMember.Email;

            //Assert
            HomeVm.IsFieldsValid().Should().BeTrue("This method should return true");
        }

        [Fact]
        public void Email_Should_Contains()
        {
            //Act
            HomeVm.Email = FakeMember.Email;

            //Assert
            HomeVm.Email.Should().Contain("@cairful.com").And.HaveLength(9);
        }


        [Fact]
        public async Task AddMember_Test()
        {
            //Act
            HomeVm.FirstName = FakeMember.FirstName;
            HomeVm.LastName = FakeMember.LastName;
            HomeVm.Email = FakeMember.Email;

            //Assert
            await HomeVm.AddMemberCommand.ExecuteAsync();
        }

        //[Fact]
        //public void IsFieldsValid_Test()
        //{
        //    var FakeMember = A.Fake<Member>();

        //    fakeMember.Id = new Guid();
        //    fakeMember.FirstName = "fakeFirstName";
        //    fakeMember.LastName = "fakeLastName";
        //    fakeMember.Email = "fakeEmail";

        //    //Act
        //    HomeVm.FirstName = fakeMember.FirstName;
        //    HomeVm.LastName = fakeMember.LastName;
        //    HomeVm.Email = fakeMember.Email;

        //    //Assert
        //    //Assert.NotNull(HomeVm.FirstName);
        //    //Assert.NotNull(HomeVm.LastName);
        //    //Assert.NotNull(HomeVm.Email);
        //    Assert.True(HomeVm.IsFieldsValid());
        //    //await HomeVm.AddMemberCommand.ExecuteAsync(); // Test by AddMember Command
        //    // Test by Add Member Method
        //    //if ()
        //    //{
        //    //    await HomeVm.AddMember(); // Test in HomeView Model
        //    //                              //await methods.WriteToFileAsync(fakeMember, File_PATH_DESTINATION.Accepted); // Test in MemberRepository
        //    //}
        //}
    }
}
