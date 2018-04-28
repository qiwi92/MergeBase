using UnityEngine;

namespace Move
{
    public interface IPositioned
    {
        Vector2Int Position { get; set; }
        Vector2Int NextPosition { get; set; }
    }
}