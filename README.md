# TriviaGame

TriviaGame is a cross-platform quiz application built with **.NET MAUI**.
It lets one or more players compete across configurable rounds using questions from the [Open Trivia Database](https://opentdb.com/).

## Features

- Cross-platform UI (Android, iOS, Mac Catalyst, Windows)
- Multiple game modes:
  - Classic
  - Race
  - Survival
  - Streak
  - Elimination
- Per-round category and difficulty selection
- Configurable rounds, questions per round, and timer
- Local preferences for theme, font size, and audio volume
- Local leaderboard persistence

## Tech Stack

- .NET 8
- .NET MAUI
- Plugin.Maui.Audio
- Open Trivia DB API

## Project Structure

- `/MainPage.xaml(.cs)` - player setup and game mode selection
- `/GamePageSettings.xaml(.cs)` - round/category/difficulty setup and question loading
- `/GamePage.xaml(.cs)` - gameplay loop, scoring, timer, and in-game actions
- `/SettingsPage.xaml(.cs)` - app preferences
- `/ViewModel/GameSettingsViewModel.cs` - game settings state and selectable options
- `/TriviaDB/Trivia.cs` - trivia API client and response models
- `/Audio.cs` - audio controls

## Requirements

- .NET SDK 8.0+
- MAUI workloads installed

Install workloads:

```bash
dotnet workload install maui
```

## Getting Started

1. Clone the repository.
2. Restore dependencies:

```bash
dotnet restore /home/runner/work/TriviaGame/TriviaGame/TriviaGame.sln
```

3. Build the solution:

```bash
dotnet build /home/runner/work/TriviaGame/TriviaGame/TriviaGame.sln
```

4. Run on your target platform (example for Android):

```bash
dotnet build /home/runner/work/TriviaGame/TriviaGame/TriviaGame.csproj -f net8.0-android
```

## Gameplay Flow

1. Choose game mode and enter player names.
2. Configure rounds (category + difficulty).
3. Play through round questions.
4. Scores are calculated based on mode behavior and question difficulty.
5. Final standings are saved in the local leaderboard.

## Configuration

User preferences are stored locally using `Preferences`:

- Theme (`IsDarkMode`)
- Timer duration (`TimerDuration`)
- Font size (`FontSize`)
- Number of rounds (`NumberOfRounds`)
- Questions per round (`NumberOfQuestions`)
- Volume (`Volume`)
- Leaderboard (`Leaderboard`)

## Notes

- Trivia questions come from an external API, so gameplay requires network access when loading questions.
- If the API returns too few questions for selected options, players are asked to try again.

## Contributing

Please read [`CONTRIBUTING.md`](CONTRIBUTING.md) before opening changes.
