using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace TriviaGame.TriviaDB
{
    public class Trivia
    {
        private readonly HttpClient httpClient;
        List<TriviaQuestion> triviaQuestionsList;

        public Trivia()
        {
            httpClient = new HttpClient();
            triviaQuestionsList = new();
        }

        public async Task<List<TriviaQuestion>> GetQuestions(int amount, int categoryId, string difficulty)
        {
            if (triviaQuestionsList.Count > 0)
            {
                return triviaQuestionsList;
            }

            var url = $"https://opentdb.com/api.php?amount={amount}&category={categoryId}&difficulty={difficulty.ToLower()}&type=multiple";

            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new List<TriviaQuestion>();
            }

            var content = await response.Content.ReadAsStringAsync();

            var triviaQuestions = JsonSerializer.Deserialize<TriviaApiResponse>(content);

            if (triviaQuestions != null && triviaQuestions.results != null)
            {
                triviaQuestionsList = triviaQuestions.results;
                return triviaQuestionsList;
            }

            return new List<TriviaQuestion>();
        }
    }
    public class TriviaApiResponse
    {
        public List<TriviaQuestion> results { get; set; }
    }
    public class TriviaQuestion
    {
        public string category { get; set; }
        public string type { get; set; }
        public string difficulty { get; set; }
        public string question { get; set; }
        public string correct_answer { get; set; }
        public List<string> incorrect_answers { get; set; }
    }
}

