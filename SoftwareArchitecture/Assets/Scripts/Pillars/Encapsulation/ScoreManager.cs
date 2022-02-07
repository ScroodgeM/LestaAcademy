
namespace WGADemo.Pillars.Encapsulation
{
    public interface IScoreConfig
    {
        long GetScoreBySource(ScoreSource scoreSource);
    }

    public enum ScoreSource
    {
        CoinCollected,
        EnemyKilled,
        BossKilled,
    }

    public class ScoreManager
    {
        private long score = 0;
        private IScoreConfig scoreConfig;

        public ScoreManager(IScoreConfig scoreConfig)
        {
            this.scoreConfig = scoreConfig;
        }

        public long GetScore() => score;

        public void SubmitScore(ScoreSource scoreSource)
        {
            this.score += scoreConfig.GetScoreBySource(scoreSource);
        }
    }
}
