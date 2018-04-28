using UnityEngine;

namespace Grid
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private CellModel _cellModel;

        public void Setup(CellModel cellModel)
        {
            _cellModel = cellModel;
            transform.position = (Vector2) cellModel.Position;
        }
    }
}