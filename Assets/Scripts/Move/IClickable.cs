using Grid;
using UnityEngine;

namespace Move
{
    public interface IClickable : IViewMovable
    {
        //void UpdateModel();
        void UpdateView();
        bool IsSelected { get; set; }
        bool Contains(Vector2 position);
        float Size { get; set; }
    }
}