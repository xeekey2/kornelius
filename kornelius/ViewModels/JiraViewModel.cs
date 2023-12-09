using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kornelius.Models;
using kornelius.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace kornelius.ViewModels
{
    public partial class JiraViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Issue> issues;
        [ObservableProperty]
        private Issue selectedIssue;
        [ObservableProperty]
        private ObservableCollection<Sprint> sprints;
        [ObservableProperty]
        private ObservableCollection<Board> boards;
        [ObservableProperty]
        private Sprint selectedSprint;
        [ObservableProperty]
        private Board selectedBoard;
        [ObservableProperty]
        private string startStopButtonText = "START";
        [ObservableProperty]
        private bool isStarted;
        [ObservableProperty]
        private bool isBoardsCollectionNotEmpty;
        [ObservableProperty]
        private bool isSprintsCollectionNotEmpty;
        [ObservableProperty]
        public bool isIssuesCollectionNotEmpty;

        public JiraViewModel()
        {
            Boards = new ObservableCollection<Board>();
            Sprints = new ObservableCollection<Sprint>();
            Issues = new ObservableCollection<Issue>();

        }

        public async Task InitializeAsync()
        {
            await LoadBoardsAsync();
            await LoadSprintsAsync();
            await LoadIssuesAsync();
        }


        [RelayCommand]
        public async Task LoadIssuesAsync()
        {
            if (SelectedSprint == null)
            {
                var issues = await IssueService.GetIssuesForBoardAndAssignee(SelectedBoard.id, "KASPER");
                Issues.Clear();
                if(!issues.Any())
                {
                    SelectedIssue = null;
                    return;
                }

                foreach (var issue in issues)
                {
                    Issues.Add(issue);
                }
                SelectedIssue = Issues.FirstOrDefault();

            }
            else
            {
                var issues = await IssueService.GetIssuesForSprintByAssignee(SelectedSprint.id, "KASPER");
                Issues.Clear();
                foreach (var issue in issues)
                {
                    Issues.Add(issue);
                }
                SelectedIssue = Issues.FirstOrDefault();
            }
        }

        [RelayCommand]
        public async Task LoadSprintsAsync()
        {
            var sprint = await IssueService.GetSprintsByBoardName(SelectedBoard.name);
            Sprints.Clear();

            if (sprint == null)
            {
                SelectedSprint = null;
                return;
            }

            Sprints.Add(sprint);
            SelectedSprint = Sprints.FirstOrDefault();
        }

        [RelayCommand]
        public async Task LoadBoardsAsync()
        {
            var boards = await IssueService.GetBoards();
            Boards.Clear();
            foreach (var board in boards)
            {
                Boards.Add(board);
            }

            if (SelectedBoard != null)
            {
                SelectedBoard = Boards.FirstOrDefault();
            }
            else
            {
                SelectedBoard = Boards.FirstOrDefault(x => x.name.ToString() == "Scrum");
            }
        }

        [RelayCommand]
        private void ToggleStartStop()
        {
            if (IsStarted)
            {
                Stop(); // This method sets IsStarted to false and performs stop logic
            }
            else
            {
                Start(); // This method sets IsStarted to true and performs start logic
            }
        }

        private void Start()
        {
            IsStarted = true;
            StartStopButtonText = "PAUSE";

        }

        private void Stop()
        {
            IsStarted = false;
            StartStopButtonText = "START";
        }

        [RelayCommand(CanExecute = nameof(CanStopOrCancel))]
        private void Cancel()
        {
            IsStarted = false;
            Stop();
        }

        private bool CanStart() => !IsStarted;
        private bool CanStopOrCancel() => IsStarted;

        public void SetDropdownVisibility()
        {
            IsBoardsCollectionNotEmpty = Boards != null && Boards.Any();
            IsSprintsCollectionNotEmpty = Sprints != null && Sprints.Any();
            IsIssuesCollectionNotEmpty = Issues != null && Issues.Any();
        }

        partial void OnIsStartedChanged(bool isStarted)
        {
            CancelCommand.NotifyCanExecuteChanged();
        }

        partial void OnSelectedSprintChanged(Sprint value)
        {
            LoadIssuesAsync();
            SetDropdownVisibility();
        }

        partial void OnSelectedBoardChanged(Board value)
        {
            LoadSprintsAsync();
            LoadIssuesAsync();
            SetDropdownVisibility();
        }
    }
}
