using UnityEngine;

namespace G2048_3D.Gameplay.Entities.Cube
{
    public class CubeVisual : MonoBehaviour
    {
        [SerializeField] private CubeEntity _cube;
        [SerializeField] private CubeSidesNumbers _sideNumbers;
        [SerializeField] private MeshRenderer _renderer;

        public void SetColor(Color color)
        {
            _renderer.material.color = color;
        }

        public void SetNumber(int number)
        {
            _sideNumbers.SetNumber(number);
        }
    }
}
