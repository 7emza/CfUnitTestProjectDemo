using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CfUnitTestProjectDemo.Common.Contracts;
using CfUnitTestProjectDemo.Common.Models;
using CfUnitTestProjectDemo.Common.Enums;
using CfUnitTestProjectDemo.Common.Extention;
using Prism.Commands;
using Prism.Mvvm;

namespace CfUnitTestProjectDemoUI.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        #region Fields
        private readonly IDataLayerService _memberRepository;
        private string _firstName;
        private string _lastName;
        private string _email;
        private Member _selectedAcceptedMemberItem;
        private Member _selectedRejectedMemberItem;
        private string _outputGenerateCode;
        private string _addCounter;
        #endregion

        public HomeViewModel(IDataLayerService memberRepository)
        {
            _memberRepository = memberRepository;
            Initialize();
        }

        #region Delegate Commands
        public DelegateCommand SeeAllMemberCommand { get; set; }
        public DelegateCommand AddMemberCommand { get; set; }
        public DelegateCommand CancelMemberCommand { get; set; }
        public DelegateCommand RemoveMemberCommand { get; set; }
        public DelegateCommand GenerateCodeCommand { get; set; }
        #endregion

        #region Properties
        public ObservableCollection<Member> ListAcceptedMembers { get; set; } = new ObservableCollection<Member>();
        public ObservableCollection<Member> ListRejectedMembers { get; set; } = new ObservableCollection<Member>();
        public List<Member> MembersFromFileList { get; set; } = new List<Member>();

        public Member SelectedAcceptedMemberItem
        {
            get => _selectedAcceptedMemberItem;
            set => SetProperty(ref _selectedAcceptedMemberItem, value);
        }
        public Member SelectedRejectedMemberItem
        {
            get => _selectedRejectedMemberItem;
            set => SetProperty(ref _selectedRejectedMemberItem, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string OutputGenerateCode
        {
            get => _outputGenerateCode;
            set => SetProperty(ref _outputGenerateCode, value);
        }
        public int Counter;

        public string AddCounter
        {
            get => _addCounter;
            set => SetProperty(ref _addCounter, value);
        }

        #endregion

        #region Methods
        public void Initialize()
        {
            SeeAllMemberCommand = new DelegateCommand(() => SeeAllMember().FireAndForgetAsync());
            AddMemberCommand = new DelegateCommand(() => AddMember().FireAndForgetAsync());
            CancelMemberCommand = new DelegateCommand(() => CancelMember().FireAndForgetAsync());

            RemoveMemberCommand = new DelegateCommand(() => RemoveMember().FireAndForgetAsync());
            GenerateCodeCommand = new DelegateCommand(() => GenerateCode().FireAndForgetAsync());
            LoadData();
            Reset();
        }
        public void LoadData()
        {
            var seeAllMember = SeeAllMember();
            seeAllMember.ConfigureAwait(false);
        }

        public async Task SeeAllMember()
        {
            await AcceptedMembers();
            await RejectedMembers();
            Reset();
        }

        public async Task RejectedMembers()
        {
            MembersFromFileList = await _memberRepository.ReadFromFileAsync(FilePathDestination.Rejected);
            ListRejectedMembers.Clear();
            foreach (var member in MembersFromFileList)
            {
                ListRejectedMembers.Add(member);
            }
        }

        public async Task AcceptedMembers()
        {
            MembersFromFileList = await _memberRepository.ReadFromFileAsync(FilePathDestination.Accepted);
            ListAcceptedMembers.Clear();
            foreach (var member in MembersFromFileList)
            {
                ListAcceptedMembers.Add(member);
            }
        }

        public async Task AddMember()
        {
            if (IsFieldsValid() == false)
            {
                return;
            }
            await _memberRepository.WriteToFileAsync(new Member
            {
                Id = new Guid(),
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
            }, FilePathDestination.Accepted);

            Counter++;
            AddCounter = $"Add Member { Counter}";
            ResetFields();
        }

        public bool IsFieldsValid() =>
            (FirstName == null ||
             LastName == null ||
             Email == null) == false;

        private async Task RemoveMember()
        {

        }
        private async Task GenerateCode()
        {
            var code = Guid.NewGuid();
            OutputGenerateCode = $"VIP-{code}";
        }

        private async Task CancelMember()
        {
            if (SelectedAcceptedMemberItem == null)
            {
                return;
            }
            if (ListAcceptedMembers.Count > 0)
            {
                ListRejectedMembers.Add(SelectedAcceptedMemberItem);
                if (ListRejectedMembers.Count > 0)
                {
                    foreach (var member in ListRejectedMembers)
                    {
                        if (MembersFromFileList.Contains(member))
                        {
                            MembersFromFileList.Remove(member);
                        }
                    }
                }
            }
            MembersFromFileList = await _memberRepository.UpdateMembersToFileAsync(MembersFromFileList, FilePathDestination.Accepted);

            var rejected = await AddToRejected(SelectedAcceptedMemberItem);
            ListAcceptedMembers.Remove(rejected);
        }
        private async Task<Member> AddToRejected(Member rejectedMember)
        {
            if (rejectedMember != null)
            {
                await _memberRepository.WriteToFileAsync(new Member
                {
                    FirstName = rejectedMember.FirstName,
                    LastName = rejectedMember.LastName,
                    Email = rejectedMember.Email,
                }, FilePathDestination.Rejected);
            }
            return rejectedMember;
        }

        private void ResetFields()
        {
            this.FirstName = null;
            this.LastName = null;
            this.Email = null;
        }
        private void ResetAdd()
        {
            Counter = 0;
            AddCounter = "Add Member";
        }
        private void Reset()
        {
            ResetFields();
            ResetAdd();
        }

        #endregion
    }

    
}
