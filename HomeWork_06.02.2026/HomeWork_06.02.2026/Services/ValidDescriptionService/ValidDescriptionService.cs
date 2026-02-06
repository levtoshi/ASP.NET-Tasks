using System.Text.Json;

namespace HomeWork_06._02._2026.Services.ValidDescriptionService
{
    public class ValidDescriptionService : IValidDescriptionService
    {
        private readonly string[] _bannedWords; 

        public ValidDescriptionService()
        {
            var path = "Resourses/bannedWords.json";
            var json = File.ReadAllText(path);
            _bannedWords = JsonSerializer.Deserialize<string[]>(json) ?? ["none"];
        }

        public bool IsValidDescription(string description)
        {
            foreach (string word in _bannedWords)
            {
                if (description.Split(" ").Contains(word))
                {
                    return false;
                }
            }
            return true;
        }
    }
}