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
        private ObservableCollection<string> sprints;
        [ObservableProperty]
        private string selectedSprint;
        [ObservableProperty]
        private string startStopButtonText = "START";
        [ObservableProperty]
        private bool isStarted;

        public JiraViewModel()
        {
            Issues = new ObservableCollection<Issue>();
            Sprints = new ObservableCollection<string>();
        }

        [RelayCommand]
        public async Task LoadIssuesAsync()
        {
            var issuesFromDatabase = await IssueService.GetIssuesForSprintByAssignee(42, "KASPER"); // Make sure this method is async
            Issues.Clear();
            foreach (var issue in issuesFromDatabase)
            {
                Issues.Add(issue);
            }
            SelectedIssue = Issues.FirstOrDefault();
        }

        [RelayCommand]
        public async Task LoadSprintsAsync()
        {
            var sprints = await Jira.GetSprintsAsync();
            Sprints.Clear();
            foreach (var sprint in sprints)
            {
                Sprints.Add(sprint);
            }
            SelectedSprint = Sprints.FirstOrDefault();
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

        partial void OnIsStartedChanged(bool isStarted)
        {
            CancelCommand.NotifyCanExecuteChanged();
        }

        partial void OnSelectedSprintChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                LoadIssuesAsync();
            }
        }
    }
}
