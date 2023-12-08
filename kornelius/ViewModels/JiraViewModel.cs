using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using kornelius.Models;
using System.Collections.ObjectModel;
using System.Linq;
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
        private bool isStarted;
        public JiraViewModel()
        {
            Issues = new ObservableCollection<Issue>();
            Sprints = new ObservableCollection<string>();
            IsStarted = false;
        }

        [RelayCommand]
        public async Task LoadIssuesAsync()
        {
            var issuesFromDatabase = await Jira.GetIssuesAsync("KASPER", SelectedSprint); // Make sure this method is async
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
        public void Start()
        {
            IsStarted = true;
        }

        [RelayCommand]
        public void Stop()
        {
            IsStarted = false;
        }

        [RelayCommand]
        public void Cancel()
        {
            IsStarted = false;
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
