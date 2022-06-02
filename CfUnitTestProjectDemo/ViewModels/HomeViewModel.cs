using CfUnitTestProjectDemo.Contracts.Repositories;
using CfUnitTestProjectDemo.Models;
using CfUnitTestProjectDemo.Persistances;
using CfUnitTestProjectDemo.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CfUnitTestProjectDemo.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private readonly IMemberRepository _memberRepository;
        private string _firstName;
        private string _lastName;
        private string _email;
        private Member _selectedAcceptedMemberItem;
        private Member _selectedRejectedMemberItem;
        private string _outputGenerateCode;
        private string _addCounter;

        public HomeViewModel(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
            Initialize();
        }

        private void Initialize()
        {
            SeeAllMemberCommand = new DelegateCommand(SeeAllMember);
            AddMemberCommand = new DelegateCommand(AddMember);
            CancelMemberCommand = new DelegateCommand(CancelMember);
            RemoveMemberCommand = new DelegateCommand(RemoveMember);
            GenerateCodeCommand = new DelegateCommand(GenerateCode);

            LoadData();
            Reset();
        }

        private void LoadData()
        {
            SeeAllMember();
        }

        public DelegateCommand AddMemberCommand { get; set; }
        public DelegateCommand CancelMemberCommand { get; set; }
        public DelegateCommand RemoveMemberCommand { get; set; }
        public DelegateCommand GenerateCodeCommand { get; set; }
        public DelegateCommand SeeAllMemberCommand { get; set; }

        public ObservableCollection<Member> ListAcceptedMembers { get; set; } = new ObservableCollection<Member>();
        public ObservableCollection<Member> ListRejectedMembers { get; set; } = new ObservableCollection<Member>();
        public List<Member> MembersFromFileList { get; set; } = new List<Member>();

        public Member SelectedAcceptedMemberItem
        {
            get => _selectedAcceptedMemberItem;
            set => SetPropertyValue(ref _selectedAcceptedMemberItem, value);
        }
        public Member SelectedRejectedMemberItem
        {
            get => _selectedRejectedMemberItem;
            set => SetPropertyValue(ref _selectedRejectedMemberItem, value);
        }


        public string FirstName
        {
            get => _firstName;
            set { SetPropertyValue(ref _firstName, value); }
        }

        public string LastName
        {
            get => _lastName;
            set { SetPropertyValue(ref _lastName, value);  }
        }
        public string Email
        {
            get => _email;
            set => SetPropertyValue(ref _email, value);
        }

        public string OutputGenerateCode
        {
            get => _outputGenerateCode;
            set => SetPropertyValue(ref _outputGenerateCode, value);
        }
        public int Counter=0;

        public string AddCounter
        {
            get => _addCounter;

            set => SetPropertyValue(ref _addCounter, value);
        }
 
        private async void SeeAllMember()
        {
            await AcceptedMembers();
            await RejectedMembers();
            Reset();
        }

        private async Task RejectedMembers()
        {
            MembersFromFileList = await _memberRepository.ReadFromFileAsync(File_PATH_DESTINATION.Rejected);
            ListRejectedMembers.Clear();
            foreach (Member member in MembersFromFileList)
            {
                ListRejectedMembers.Add(member);
            }
        }
        private async Task AcceptedMembers()
        {
            MembersFromFileList = await _memberRepository.ReadFromFileAsync(File_PATH_DESTINATION.Accepted);
            ListAcceptedMembers.Clear();
            foreach (Member member in MembersFromFileList)
            {
                ListAcceptedMembers.Add(member);
            }
        }

        public async void AddMember()
        {
            if (IsFieldsValid() == false)
            {
                return;
            }
            await _memberRepository.WriteToFileAsync(new Member
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
            }, File_PATH_DESTINATION.Accepted);

            Counter++;
            AddCounter = $"Add Member { Counter}";
            ResetFields();
        }

        private bool IsFieldsValid()
        {
            if (FirstName == null || LastName == null || Email == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void RemoveMember()
        {

        }
        private void GenerateCode()
        {
            Guid code = Guid.NewGuid();
            OutputGenerateCode = $"VIP-{code.ToString()}";
        }
     
        private async void CancelMember()
        {
            if (SelectedAcceptedMemberItem==null)
            {
                return;
            }
            if (ListAcceptedMembers.Count > 0)
            {
                ListRejectedMembers.Add(SelectedAcceptedMemberItem);
                if (ListRejectedMembers.Count > 0)
                {
                    foreach (Member member in ListRejectedMembers)
                    {
                        if (MembersFromFileList.Contains(member) == true)
                        {
                            MembersFromFileList.Remove(member);
                        }
                    }
                }

                AddToRejected(SelectedAcceptedMemberItem);

                ListAcceptedMembers.Remove(SelectedAcceptedMemberItem);
                MembersFromFileList = await _memberRepository.UpdateMembersToFileAsync(MembersFromFileList, File_PATH_DESTINATION.Accepted);
            }
        }
        private async void AddToRejected(Member rejectedMember)
        {
            if (rejectedMember!=null)
            {
                await _memberRepository.WriteToFileAsync(new Member
                {
                    FirstName = rejectedMember.FirstName,
                    LastName = rejectedMember.LastName,
                    Email = rejectedMember.Email,
                }, File_PATH_DESTINATION.Rejected);
            }
        }

        private void ResetFields()
        {
            this.FirstName = null;
            this.LastName = null;
            this.Email = null;
        }
        private void ResetAdd()
        {
            Counter=0;
            AddCounter = "Add Member";
        }
        private void Reset()
        {
            ResetFields();
            ResetAdd();
        }
    }
}
