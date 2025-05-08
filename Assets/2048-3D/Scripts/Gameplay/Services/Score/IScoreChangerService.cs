using System;

namespace G2048_3D.Gameplay.Services.Score
{
    public interface IScoreChangerService
    {
        event Action ScoreChanged;
        int Score { get; }
        void ChangeScore(int value);
    }
}
