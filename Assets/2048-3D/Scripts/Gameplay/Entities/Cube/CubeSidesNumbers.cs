using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace G2048_3D.Gameplay.Entities.Cube
{
    [Serializable]
    public class CubeSidesNumbers
    {
        [SerializeField] private TextMeshPro[] _numberTexts;

        public void SetNumber(int number)
        {
            foreach (TextMeshPro numberText in _numberTexts)
            {
                numberText.text = number.ToString();
                numberText.DOFade(1, 0.1f);
            }
        }
        public void FadeOut()
        {
            foreach (TextMeshPro numberText in _numberTexts)
                numberText.DOFade(0, 0.1f);
        }
    }
}
