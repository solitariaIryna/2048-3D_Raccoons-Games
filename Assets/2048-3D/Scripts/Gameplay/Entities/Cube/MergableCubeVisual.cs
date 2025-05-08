using Cysharp.Threading.Tasks;
using DG.Tweening;
using G2048_3D.Gameplay.Services.Particle;
using System.Threading;
using UnityEngine;
using Zenject;

namespace G2048_3D.Gameplay.Entities.Cube
{
    public class MergableCubeVisual : MonoBehaviour
    {
        [SerializeField] private MergableCube _cube;
        [SerializeField] private CubeSidesNumbers _sideNumbers;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private LineRenderer _directionLine;

        private ParticleRunnerService _particleRunner;

        private CancellationTokenSource _mergeAnimationCTS;
        private Sequence _mergeSequence;

        [Inject]
        private void Construct(ParticleRunnerService particleRunner)
        {
            _particleRunner = particleRunner;
        }
        public void EnableDirectionLine(bool value) => 
            _directionLine.enabled = value;
        public void SetColor(Color color) => 
            _renderer.material.color = color;

        public void SetNumber(int number) => 
            _sideNumbers.SetNumber(number);


        public async UniTask PlayMergeAnimation(Vector3 targetPosition, Color targetColor, Vector3 targetRotation, CancellationTokenSource token)
        {
            _mergeAnimationCTS = token;

            float duration = 0.2f;

            _mergeSequence = DOTween.Sequence();

            _mergeSequence.Join(_cube.transform.DOMove(targetPosition, 0.3f).SetEase(Ease.InOutSine));

            _mergeSequence.Join(_renderer.material.DOColor(targetColor, duration).SetEase(Ease.InOutSine));
            _mergeSequence.Join(transform.DORotate(targetRotation, duration).SetEase(Ease.InOutSine));

            _mergeSequence.Join(transform.DOScale(Vector3.one * 1.2f, duration / 2)
                         .SetEase(Ease.OutQuad)
                          .OnComplete(() =>
                          {
                              transform.DOScale(Vector3.one, duration / 2).SetEase(Ease.InQuad);
                          }));

            _particleRunner.PlayParticlesAt(ParticleType.CubeCollision, transform.position, Vector3.one * 0.5f);
            _sideNumbers.FadeOut();

            await UniTask.WaitForSeconds(0.1f, cancellationToken: _mergeAnimationCTS.Token);
        }

        public void ResetData()
        {
            _mergeSequence.Kill();
            transform.localScale = Vector3.one;
            transform.eulerAngles = Vector3.zero;
        }

        public void Show()
        {
            transform.DOScale(1.2f, 0.2f).From(1).SetEase(Ease.OutSine).OnComplete(() =>
            {
                transform.DOScale(1f, 0.1f).From(1).SetEase(Ease.InSine);
            });
        }
    }
}
