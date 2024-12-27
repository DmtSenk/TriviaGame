using System.Diagnostics;
using System.Text.Json;

namespace TriviaGame.TriviaDB
{
    public class Trivia
    {
        private readonly HttpClient httpClient;

        public Trivia()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<TriviaQuestion>> GetQuestions(int amount, int categoryId, string difficulty)
        {
            var url = $"https://opentdb.com/api.php?amount={amount}&category={categoryId}&difficulty={difficulty.ToLower()}&type=multiple";
            
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new List<TriviaQuestion>();
            }

            var content = await response.Content.ReadAsStringAsync();

            var triviaApiResponse = JsonSerializer.Deserialize<TriviaResponse>(content);

            var questionList = new List<TriviaQuestion>();
            if (triviaApiResponse != null && triviaApiResponse.Results != null)
            {
                questionList = triviaApiResponse.Results;
            }

            return questionList;
        }
    }
    public class TriviaResponse
    {
        public int ResponseCode { get; set; }
        public List<TriviaQuestion> Results { get; set; }
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

