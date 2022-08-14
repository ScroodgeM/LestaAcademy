
namespace WGADemo.Pillars.Encapsulation
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
        private long score = 0;
        private float scoreMultiplier = 1.0f;
        private IScoreConfig scoreConfig;

        public ScoreManager(IScoreConfig scoreConfig)
        {
            this.scoreConfig = scoreConfig;
        }

        public long GetScore() => score;

        public void SubmitScore(ScoreSource scoreSource)
        {
            this.score += (long)(scoreConfig.GetScoreBySource(scoreSource) * scoreMultiplier);
        }
    }
}
