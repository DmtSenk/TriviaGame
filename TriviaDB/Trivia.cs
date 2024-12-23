﻿using System.Text.Json;

namespace TriviaGame.TriviaDB
{
    public class Trivia
    {
        private readonly HttpClient httpClient;
        private List<TriviaQuestion> questionList = new List<TriviaQuestion>();

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
        public string Category { get; set; }
        public string Type { get; set; }
        public string Difficulty { get; set; }
        public string Question { get; set; }
        public string CorrectAns { get; set; }
        public List<string> IncorrectAns { get; set; }
    }
}

