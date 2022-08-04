using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CfUnitTestProjectDemo.Common.Contracts;
using CfUnitTestProjectDemo.Common.Models;
using CfUnitTestProjectDemo.DataLayer.DataLayer;
using CfUnitTestProjectDemoUI.ViewModels;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace CfUintTestProjectDemoUI.ClientSideTest.ViewModelsTest
{
    public class MemberViewModelTest
    {
        private readonly HomeViewModel _homeVm;
        private readonly Member _fakeMember;

        public MemberViewModelTest()
        {
            //Arrange
            _homeVm = new HomeViewModel(A.Fake<IDataLayerService>());
            _fakeMember = new Member
            {
                Id = new Guid(),
                FirstName = "fakeFirstName",
                LastName = "fakeLastName",
                Email = "email@cairful.com"
            };
        }



        [Fact]
        public void MemoryLeak_Test()
        {
            var weakRef = new List<WeakReference>
            {
                new WeakReference(_homeVm.FirstName),
                new WeakReference(_homeVm.LastName),
                new WeakReference(_homeVm.Email),
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
            _homeVm.FirstName = _fakeMember.FirstName;
            _homeVm.LastName = _fakeMember.LastName;
            _homeVm.Email = _fakeMember.Email;

            //Assert
            _homeVm.IsFieldsValid().Should().BeTrue("This method should return true");
        }

        [Fact]
        public void Email_Should_Contains()
        {
            //Act
            _homeVm.Email = _fakeMember.Email;

            //Assert
            _homeVm.Email.Should().Contain("@cairful.com");
        }


        [Fact]
        public void AddMember_Test()
        {
            //Act
            _homeVm.FirstName = _fakeMember.FirstName;
            _homeVm.LastName = _fakeMember.LastName;
            _homeVm.Email = _fakeMember.Email;

            //Assert
            _homeVm.AddMemberCommand.Execute();
        }


    }
}
