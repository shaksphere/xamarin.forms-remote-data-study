using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GitHubRepos.Models;

namespace GitHubRepos.ViewModels
{
    public class ReposViewModel : INotifyPropertyChanged
    {
        private readonly RepoRepository _repository;

        public event PropertyChangedEventHandler? PropertyChanged;
        public List<RepoViewModel> Repos { get; private set; } = new List<RepoViewModel>();

        public ReposViewModel(RepoRepository repository)
        {
            _repository = repository;

            repository.PropertyChanged += RepositoryOnPropertyChanged;
        }

        private void RepositoryOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_repository.Repos))
            {
                // Repopulate list of repo view models
                Repos = _repository.Repos.Select(repo => new RepoViewModel(repo)).ToList();
            }
        }

        public void RefreshRepos() => _repository.RefreshRepos();
    }
}