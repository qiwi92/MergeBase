using System.Collections.Generic;
using System.Linq;
using Move;
using UnityEngine;

namespace Grid
{
    public class GridModel
    {
        private List<CellModel> _cells;

        public GridModel(List<CellModel> cells)
        {
            _cells = cells;
        }

        public Vector2Int HandleCollision(Vector2Int oldPos, Vector2Int newPos)
        {
            foreach (var cell in _cells)
            {
                if (newPos == cell.Position && !cell.IsOcupied)
                {
                    cell.IsOcupied = true;

                    var oldCell = _cells.Single(e => e.Position == oldPos);
                    oldCell.IsOcupied = false;

                    return newPos;
                }
            }

            return oldPos;
        }

        public void Sync(Vector2Int position)
        {
            foreach (var cell in _cells)
            {
                if (position != cell.Position)
                {
                    cell.IsOcupied = false;
                }
            }
        }

        public bool IsNextPosInGrid(IPositioned positionedObject)
        {
            return _cells.Any(x => x.Position == positionedObject.NextPosition);
        }
    }
}