using System.Collections.Generic;
using System.Linq;
using Grid;
using JetBrains.Annotations;
using UnityEngine;

namespace Move
{
    public class InputHandler
    {
        private List<BlockView> _blocks;
        private BlockView _currentClickable;

        private GridCollisions _gridCollisions;

        public InputHandler(List<BlockView> blocks, GridModel gridModel)
        {
            _blocks = blocks;
            _gridCollisions = new GridCollisions(blocks,gridModel);
        }


        public void HandleInput(float deltTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                foreach (var clickable in _blocks)
                {
                    if (clickable.Contains(GetMousePosition()))
                    {
                        _currentClickable = clickable;
                        _currentClickable.IsSelected = true;
                    }
                }

                if (_currentClickable != null)
                {
                    _currentClickable.UpdateView();
                }
            }
            else if (Input.GetKey(KeyCode.Mouse0) && _currentClickable != null)
            {
                if (_currentClickable.IsSelected)
                {
                    _currentClickable.Transform.position = Vector3.MoveTowards(_currentClickable.Transform.position, GetMousePosition(), deltTime * 10f * Distance());
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0) && _currentClickable != null)
            {
                if (!_currentClickable.IsSelected) return;

                _currentClickable.IsSelected = false;

                _currentClickable.NextPosition  = new Vector2Int(Mathf.RoundToInt(GetMousePosition().x), Mathf.RoundToInt(GetMousePosition().y));
                _gridCollisions.HandleCollision(_currentClickable);

                //_currentClickable.UpdateModel();
                _currentClickable.UpdateView();
                _currentClickable = null;
            }
        }

        private Vector2 GetMousePosition()
        {
            var mousePosOnScreen = Input.mousePosition;
            mousePosOnScreen.z = 10.0f;
            return Camera.main.ScreenToWorldPoint(mousePosOnScreen); ;
        }

        private float Distance()
        {
            return Vector2.Distance(GetMousePosition(), _currentClickable.Transform.position);
        }
    }
}