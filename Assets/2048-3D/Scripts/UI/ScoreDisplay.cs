using DG.Tweening;
using G2048_3D.Gameplay.Services.Score;
using TMPro;
using UnityEngine;
using Zenject;

namespace G2048_3D.UI
{
    public class ScoreDisplay : BaseDisplay
    {
        [SerializeField] protected TextMeshProUGUI _score;
        [SerializeField] protected string _format = "Score:{0}";
        [Space]
        [SerializeField] private float _moveOffsetY;

        private IScoreChangerService _scoreChanger;

        private float _startPosY;

        [Inject]
        private void Construct(IScoreChangerService scoreChanger)
        {
            _scoreChanger = scoreChanger;
        }
        protected override void OnInitialize()
        {
            UpdateText();
            ShowText();
            base.OnInitialize();

            _startPosY = _score.transform.localPosition.y;
        }

        private void ShowText()
        {
            _score.DOFade(1, 0.3f).SetEase(Ease.InSine).From(0).SetDelay(0.1f);
            _score.transform.DOScale(1, 0.2f).SetEase(Ease.OutBack).From(0.4f).SetDelay(0.1f);
        }

        protected void UpdateText()
        {
            _score.transform.DORewind();
            _score.transform.DOLocalMoveY(_startPosY + _moveOffsetY, 0.2f)
           .SetEase(Ease.OutBack)
           .OnComplete(() =>
           {
               _score.text = string.Format(_format, _scoreChanger.Score);

               _score.transform.DOLocalMoveY(_startPosY, 0.1f)
                    .SetEase(Ease.InBack);
           });

           
        }

        protected override void Subscribe() =>
            _scoreChanger.ScoreChanged += UpdateText;

        protected override void UnSubscribe() =>
            _scoreChanger.ScoreChanged -= UpdateText;

        private void HideText()
        {
            _score.transform.DOKill();
            _score.DOFade(0, 0.3f).SetEase(Ease.InSine)
                .OnComplete(() => Deactivate());
        }

        private void OnDisable()
        {
            _score.transform.DOKill();
            UnSubscribe();
        }

    }
}
