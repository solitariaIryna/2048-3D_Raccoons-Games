using System.Linq;
using UnityEngine;

namespace G2048_3D.Configs.Cube
{
    [CreateAssetMenu(fileName = nameof(CubeConfig), menuName = "Configs/" + nameof(CubeConfig))]
    public class CubeConfig : ScriptableObject
    {
        [SerializeField] private GenericDictionary<int, Color> _colorsForNumbers;

        public Color GetColorForNumber(int number)
        {
            if (_colorsForNumbers.TryGetValue(number, out Color color))
                return color;

            Debug.LogError($"Color for number: {number} is not exist");
            return _colorsForNumbers.Last().Value;
        }
    }
}
