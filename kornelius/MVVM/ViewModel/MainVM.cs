using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kornelius.Model;
using kornelius.MVVM.Model;
using kornelius.Services;
using kornelius.Services.Jira;
using kornelius.Services.Setting;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace kornelius.ViewModel
{
    public partial class MainVM : VM
    {
        [ObservableProperty] private ObservableCollection<Issue> issues;
        [ObservableProperty] private Issue selectedIssue;
        [ObservableProperty] private ObservableCollection<Sprint> sprints;
        [ObservableProperty] private ObservableCollection<Board> boards;
        [ObservableProperty] private Sprint selectedSprint;
        [ObservableProperty] private Board selectedBoard;
        [ObservableProperty] private string startStopButtonText = "START";
        [ObservableProperty] private string logButtonText = "Log";
        [ObservableProperty] private bool isStarted;
        [ObservableProperty] private bool isBoardsCollectionNotEmpty;
        [ObservableProperty] private bool isSprintsCollectionNotEmpty;
        [ObservableProperty] public bool isIssuesCollectionNotEmpty;
        [ObservableProperty] private string elapsedTime = "00:00:00";
        [ObservableProperty] private string timerDisplayText;

        private DispatcherTimer timer;
        public TimeSpan elapsedTimeSpan;

        public MainVM()
        {
            InitializeCollections();
            InitializeAsync();
            InitializeTimer();
        }

        private void InitializeCollections()
        {
            Boards = new ObservableCollection<Board>();
            Sprints = new ObservableCollection<Sprint>();
            Issues = new ObservableCollection<Issue>();
        }


        public async Task InitializeAsync()
        {
            await LoadSettingsAsync();
            await LoadBoardsAsync();
            await LoadSprintsAsync();
            await LoadIssuesAsync();
        }

        public async Task InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Update every second
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsedTimeSpan = elapsedTimeSpan.Add(TimeSpan.FromSeconds(1));
            ElapsedTime = elapsedTimeSpan.ToString(@"hh\:mm\:ss");
            UpdateTimerDisplayText();
        }

        private void UpdateTimerDisplayText()
        {
            if (elapsedTimeSpan > TimeSpan.Zero && SelectedIssue != null)
            {
                TimerDisplayText = $"Issue: {SelectedIssue.key} - Time: {elapsedTimeSpan:hh\\:mm\\:ss}";
            }
            else
            {
                TimerDisplayText = "No active time monitoring";
            }
        }

        #region Commands

        [RelayCommand]
        public async Task LoadSettingsAsync()
        {
            // Implement your logic to load settings
        }

        [RelayCommand]
        public async Task LoadSprintsAsync()
        {
            var sprint = await SprintService.GetSprintsByBoardId(SelectedBoard.id);
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
            var boards = await BoardService.GetBoardsWithAssigneeIssues("Kasper");
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
                var issues = await IssueService.GetIssuesForBoardAndAssignee(SelectedBoard.id, "Kasper");
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
            else
            {
                var issues = await IssueService.GetIssuesForSprintAndAssignee(SelectedSprint.id, "Kasper");
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
            UpdateTimerDisplayText(); // Update display text when issues are loaded
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

        #endregion

        #region UIHelpers
        private void Start()
        {
            if (!IsStarted)
            {
                // No reset of elapsedTimeSpan here to ensure it picks up from where it left off
                timer.Start();
                IsStarted = true;
                StartStopButtonText = "PAUSE";
            }
        }

        private void Stop()
        {
            IsStarted = false;
            StartStopButtonText = "START";
            timer.Stop();
        }

        [RelayCommand(CanExecute = nameof(CanClear))]
        private void Clear()
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
            }
            elapsedTimeSpan = TimeSpan.Zero;
            ElapsedTime = "00:00:00";
            IsStarted = false;
            StartStopButtonText = "START";
            UpdateTimerDisplayText(); // Update display text when cleared
        }

        [RelayCommand(CanExecute = nameof(CanLog))]
        private async void Log()
        {
            await LogService.LogTimeOnIssue(SelectedIssue.key, "1h", "");
            elapsedTimeSpan = TimeSpan.Zero;
            ElapsedTime = "00:00:00";
            IsStarted = false;
            StartStopButtonText = "START";
            UpdateTimerDisplayText(); // Update display text when logged
        }

        private bool CanStart() => !IsStarted;
        private bool CanClear() => elapsedTimeSpan > TimeSpan.Zero;
        private bool CanLog() => elapsedTimeSpan > TimeSpan.FromMinutes(0);

        partial void OnIsStartedChanged(bool isStarted)
        {
            LogCommand.NotifyCanExecuteChanged();
            ClearCommand.NotifyCanExecuteChanged();
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
        #endregion
    }
}
