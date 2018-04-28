using Grid;
using Merge;
using UnityEngine;

namespace Move
{
    public class BlockModel : IPositioned, IMergable, IMoveable
    {
        public Vector2Int Position { get; set; }
        public Vector2Int NextPosition { get; set; }

        public int Id { get; set; }

        public void Merge(ICollidible targetItem)
        {
            Debug.Log("Merged");
        }

        public void Move()
        {
            Position = NextPosition;
        }
    }
}