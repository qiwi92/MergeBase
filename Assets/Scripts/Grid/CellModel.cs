using UnityEngine;

namespace Grid
{
    public class CellModel
    {
        public CellModel(bool isOcupied, Vector2Int position)
        {
            IsOcupied = isOcupied;
            Position = position;
        }

        public bool IsOcupied { get; set; }

        public Vector2Int Position { get; private set; }
    }
}