using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kornelius.Models;
using kornelius.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace kornelius.ViewModel
{
    public partial class MainVM : VM
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
        [ObservableProperty]
        private object currentView;
        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public MainVM(INavigationService navService)
        {
            Boards = new ObservableCollection<Board>();
            Sprints = new ObservableCollection<Sprint>();
            Issues = new ObservableCollection<Issue>();
            CurrentView = new View.MainUC();
            Navigation = navService;
            InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            await LoadBoardsAsync();
            await LoadSprintsAsync();
            await LoadIssuesAsync();
        }

        [RelayCommand]
        public async Task LoadSprintsAsync()
        {
            var sprint = await IssueService.GetSprintsByBoardId(SelectedBoard.id);
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
        public async Task LoadIssuesAsync()
        {
            if (SelectedSprint == null || Sprints.Count() == 0)
            {
                var issues = await IssueService.GetIssuesForBoardAndAssignee(SelectedBoard.id, "KASPER");
                Issues.Clear();
                if (!issues.Any())
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
        }

        [RelayCommand]
        private void ToggleStartStop()
        {
            if (IsStarted)
            {
                Stop(); 
            }
            else
            {
                Start(); 
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

        partial void OnIsStartedChanged(bool isStarted)
        {
            CancelCommand.NotifyCanExecuteChanged();
        }

        partial void OnSelectedSprintChanged(Sprint value)
        {
            LoadIssuesAsync();
        }

        partial void OnSelectedBoardChanged(Board value)
        {
            LoadSprintsAsync();
            LoadIssuesAsync();
        }
    }
}
