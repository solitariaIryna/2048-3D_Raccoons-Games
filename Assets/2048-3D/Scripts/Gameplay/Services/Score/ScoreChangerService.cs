using System;
namespace G2048_3D.Gameplay.Services.Score
{
    public class ScoreChangerService : IScoreChangerService
    {
        public event Action ScoreChanged;
        public int Score { get; private set; }
        public void ChangeScore(int value)
        {
            Score += value;
            ScoreChanged?.Invoke();
        }
    }
}
