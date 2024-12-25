using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TriviaGame.ViewModel
{
    public class PlayersSett
    {
        public string Name { get; set; }
        public string SelectedCategory { get; set; }
        public string SelectedDifficulty { get; set; }
    }
    public class GameSettingsViewModel : INotifyPropertyChanged
    {
        private string selectedCategory;
        private string selectedDifficulty;
        private string selectedGameMode;


        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> Categories { get; set; }
        public ObservableCollection<string> Difficulties { get; set; }
        public ObservableCollection<string> GameModes { get; set; }
        public ObservableCollection<PlayersSett> Players { get; set; }


        public string SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                if (selectedCategory != value)
                {
                    selectedCategory = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedDifficulty
        {
            get
            {
                return selectedDifficulty;
            }
            set
            {
                if (selectedDifficulty != value)
                {
                    selectedDifficulty = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedGameMode
        {
            get
            {
                return selectedGameMode;
            }
            set
            {
                if (selectedGameMode != value)
                {
                    selectedGameMode = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public GameSettingsViewModel()
        {
            Categories = new ObservableCollection<string>
            {
                "General Knowledge",
                "Film",
                "Music",
                "Video Games",
                "History",
                "Mythology"
            };

            Difficulties = new ObservableCollection<string>
            {
                "Easy",
                "Medium",
                "Hard"
            };

            GameModes = new ObservableCollection<string>
            {
                "Classic",
                "Race",
                "Survival",
                "Streak",
                "Elimination"
            };

            
            SelectedCategory = Categories.FirstOrDefault("General Knowledge");
            SelectedDifficulty = Difficulties.FirstOrDefault("Easy");
            SelectedGameMode = GameModes.FirstOrDefault("Classic");


            Players = new ObservableCollection<PlayersSett>();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
