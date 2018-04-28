using System;
using DG.Tweening;
using Grid;
using Merge;
using UnityEngine;

namespace Move
{
    public class BlockView : MonoBehaviour, IClickable, ICollidible
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _notSelectedColor;

        public Transform Transform { get; set; }
        public float Size { get; set; }
        public bool IsSelected { get; set; }

        private BlockModel _blockModel;
        private GridModel _grid;

        public void Setup(Vector2 pos, GridModel grid, int id)
        {
            Size = 0.5f;

            transform.position = pos;
            Transform = transform;

            IsSelected = false;

            _blockModel = new BlockModel {Position = new Vector2Int((int) pos.x, (int) pos.y)};

            _renderer.color = _notSelectedColor;
            _grid = grid;

            Id = id;
            Destroy = false;
        }

        //public void UpdateModel()
        //{
        //    var oldPos = BlockModel.Position;
        //    var newPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        //    BlockModel.Position = _grid.HandleCollision(oldPos, newPos);
        //}

        public void UpdateView()
        {
            var newPos = new Vector3(_blockModel.Position.x,_blockModel.Position.y,0);
            transform.DOKill();

            if (IsSelected == false)
            {
                transform.DOMove(newPos, 0.2f);
                transform.DOScale(Vector3.one, 0.2f);
                _renderer.DOColor(_notSelectedColor, 0.2f);
            }
            else
            {
                transform.DOScale(Vector3.one*1.1f, 0.2f);
                _renderer.DOColor(_selectedColor, 0.2f);
            }

        }

        public bool Contains(Vector2 position)
        {
            var curentPos = (Vector2) transform.position;
            var x = (position-curentPos).x;
            var y = (position - curentPos).y;

            if (Mathf.Abs(x) < Size && Mathf.Abs(y) < Size)
            {
                return true;
            }

            return false;
        }

        public int Id
        {
            get { return _blockModel.Id; }
            set { _blockModel.Id = value; }
        }
        public void Merge(ICollidible targetItem)
        {
            targetItem.Destroy = true;
            Debug.Log(targetItem.Destroy);
            _blockModel.Merge(targetItem);
        }

        public Vector2Int Position
        {
            get { return _blockModel.Position; }
            set { _blockModel.Position = value; }
        }

        public Vector2Int NextPosition {
            get { return _blockModel.NextPosition; }
            set { _blockModel.NextPosition = value; }
        }
        public void Move()
        {
            _blockModel.Move();
        }

        public bool Destroy { get; set; }
    }
}