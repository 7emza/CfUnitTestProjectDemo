using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CfUnitTestProjectDemo.Common.Contracts;
using CfUnitTestProjectDemo.Common.Models;
using CfUnitTestProjectDemoUI.ViewModels;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace CfUintTestProjectDemoUI.ClientSideTest.ViewModelsTest
{
    public class MemberViewModelTest
    {

        private HomeViewModel _homeViewModel;
        public Member _fakeMember;

        public MemberViewModelTest()
        {
            //Arrange
            _homeViewModel = new HomeViewModel(A.Fake<IDataLayerService>());
            _fakeMember = new Member
            {
                Id = new Guid(),
                FirstName = "fakeFirstName",
                LastName = "fakeLastName",
                Email = null
            };
        }

   

        [Fact]
        public void MemoryLeak_Test()
        {
            var weakRef = new List<WeakReference>
            {
                new WeakReference(_homeViewModel.FirstName),
                new WeakReference(_homeViewModel.LastName),
                new WeakReference(_homeViewModel.Email),
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
            _homeViewModel.FirstName = _fakeMember.FirstName;
            _homeViewModel.LastName = _fakeMember.LastName;
            _homeViewModel.Email = _fakeMember.Email;

            //Assert
            _homeViewModel.IsFieldsValid().Should().BeTrue("This method should return true");
        }

        [Fact]
        public void Email_Should_Contains()
        {
            //Act
            _homeViewModel.Email = _fakeMember.Email;

            //Assert
            _homeViewModel.Email.Should().Contain("@cairful.com").And.HaveLength(9);
        }


        [Fact]
        public void AddMember_Test()
        {
            //Act
            _homeViewModel.FirstName = _fakeMember.FirstName;
            _homeViewModel.LastName = _fakeMember.LastName;
            _homeViewModel.Email = _fakeMember.Email;

            //Assert
            _homeViewModel.AddMemberCommand.Execute();
        }

        //[Fact]
        //public void IsFieldsValid_Test()
        //{
        //    var _fakeMember = A.Fake<Member>();

        //    fakeMember.Id = new Guid();
        //    fakeMember.FirstName = "fakeFirstName";
        //    fakeMember.LastName = "fakeLastName";
        //    fakeMember.Email = "fakeEmail";

        //    //Act
        //    _homeViewModel.FirstName = fakeMember.FirstName;
        //    _homeViewModel.LastName = fakeMember.LastName;
        //    _homeViewModel.Email = fakeMember.Email;

        //    //Assert
        //    //Assert.NotNull(_homeViewModel.FirstName);
        //    //Assert.NotNull(_homeViewModel.LastName);
        //    //Assert.NotNull(_homeViewModel.Email);
        //    Assert.True(_homeViewModel.IsFieldsValid());
        //    //await _homeViewModel.AddMemberCommand.ExecuteAsync(); // Test by AddMember Command
        //    // Test by Add Member Method
        //    //if ()
        //    //{
        //    //    await _homeViewModel.AddMember(); // Test in HomeView Model
        //    //                              //await methods.WriteToFileAsync(fakeMember, File_PATH_DESTINATION.Accepted); // Test in MemberRepository
        //    //}
        //}
    }
}
