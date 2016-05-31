namespace EmotionApp.Models
{
    public class EmotionScore
    {
        public string Name { get; set; }

        public double Score { get; set; }

        public EmotionScore()
        {
            Name = "Unknown";
            Score = 0.0;
        }

        public EmotionScore(string name, double score)
        {
            Name = name;
            Score = score;
        }
    }
}
