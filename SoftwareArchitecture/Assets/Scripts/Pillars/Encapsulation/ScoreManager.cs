//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.Pillars.Encapsulation
{
    public enum ScoreSource
    {
        CoinCollected,
        EnemyKilled,
        BossKilled,
    }

    public interface IScoreConfig
    {
        long GetScoreBySource(ScoreSource scoreSource);
    }

    public interface IScoreManager
    {
        long GetScore();
        void SubmitScore(ScoreSource scoreSource);
    }

    public class ScoreManager : IScoreManager
    {
        private readonly float scoreMultiplier = 1.0f;
        private readonly IScoreConfig scoreConfig;

        private long score = 0;

        internal ScoreManager(IScoreConfig scoreConfig, float scoreMultiplier)
        {
            this.scoreConfig = scoreConfig;
            this.scoreMultiplier = scoreMultiplier;
        }

        public long GetScore() => score;

        public void SubmitScore(ScoreSource scoreSource)
        {
            this.score += (long)(scoreConfig.GetScoreBySource(scoreSource) * scoreMultiplier);
        }
    }
}
