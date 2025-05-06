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
                numberText.text = number.ToString();
            
        }
    }
}
