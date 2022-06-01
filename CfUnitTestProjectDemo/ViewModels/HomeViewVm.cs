using CfUnitTestProjectDemo.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfUnitTestProjectDemo.ViewModels
{
    public class HomeViewVm : BindableBase
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        public HomeViewVm()
        {
            SeeAllMemberCommand = new DelegateCommand(SeeAllMember);
            AddMemberCommand = new DelegateCommand(AddMember);
            CancelMemberCommand = new DelegateCommand(CancelMember);
            GenerateCodeCommand = new DelegateCommand(GenerateCode);
        }

        public DelegateCommand AddMemberCommand { get; set; }
        public DelegateCommand CancelMemberCommand { get; set; }
        public DelegateCommand GenerateCodeCommand { get; set; }
        public DelegateCommand SeeAllMemberCommand { get; set; }


        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                SetPropertyValue(ref _lastName, value);
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                SetPropertyValue(ref _email, value);
            }
        }

        public void AddMember()
        {

        }
        private void GenerateCode()
        {
            
        }

        private void CancelMember()
        {
            
        }

        private void SeeAllMember()
        {
            
        }
    }
}
