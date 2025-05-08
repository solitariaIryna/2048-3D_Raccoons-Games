using System.Linq;
using UnityEngine;

namespace G2048_3D.Configs.Cube
{
    [CreateAssetMenu(fileName = nameof(CubeConfig), menuName = "Configs/" + nameof(CubeConfig))]
    public class CubeConfig : ScriptableObject
    {
        [SerializeField] private CubeNumberChance[] _numbersChance;
        [SerializeField] private GenericDictionary<int, Color> _colorsForNumbers;

        public int GetNumber()
        {
            float totalChance = _numbersChance.Sum(n => n.Chance);
            float randomValue = Random.Range(0, totalChance);

            float cumulative = 0f;
            foreach (var numberChance in _numbersChance)
            {
                cumulative += numberChance.Chance;
                if (randomValue <= cumulative)
                {
                    return numberChance.Number;
                }
            }
            Debug.LogWarning("No valid number selected, check chances setup");
            return _numbersChance.Length > 0 ? _numbersChance[0].Number : 0;
        }
        public Color GetColorForNumber(int number)
        {
            if (_colorsForNumbers.TryGetValue(number, out Color color))
                return color;

            Debug.LogError($"Color for number: {number} is not exist");
            return _colorsForNumbers.Last().Value;
        }
    }
}
