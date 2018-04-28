using System.Collections.Generic;
using Grid;
using Move;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BlockView _blockPrefab;
        [SerializeField] private CellView _cellPrefab;

        [SerializeField] private int _amount;

        private GridModel _gridModel;

        private List<BlockView> _blocks;
        private List<BlockView> _deadBlocks;
        private InputHandler _inputHandler;


        private void Start()
        {
            _blocks = new List<BlockView>();
            _deadBlocks = new List<BlockView>();



            var cells = new List<CellModel>
            {
                new CellModel(false, new Vector2Int(0,0)),
                new CellModel(false, new Vector2Int(1,0)),
                new CellModel(true, new Vector2Int(0,1)),
                new CellModel(true, new Vector2Int(1,1)),
                new CellModel(false, new Vector2Int(1,2)),
                new CellModel(false, new Vector2Int(2,2)),
                new CellModel(false, new Vector2Int(-1,2)),
                new CellModel(false, new Vector2Int(-2,2)),
            };

            _gridModel = new GridModel(cells);

            foreach (var cell in cells)
            {
                var newCell = Instantiate(_cellPrefab, transform);
                newCell.Setup(cell);

                if (cell.IsOcupied)
                {
                    var newBlock = Instantiate(_blockPrefab, transform);
                    newBlock.Setup(cell.Position, _gridModel, 1);

                    _blocks.Add(newBlock);
                }
            }

            _inputHandler = new InputHandler(_blocks,_gridModel);
        }


        private void Update()
        {
            _inputHandler.HandleInput(Time.smoothDeltaTime);

            foreach (var blockView in _blocks)
            {
                if (blockView.Destroy)
                {
                    _deadBlocks.Add(blockView);
                    
                }
            }

            if (_deadBlocks.Count > 0)
            {
                foreach (var deadBlock in _deadBlocks)
                {
                    Destroy(deadBlock.gameObject);
                    _blocks.Remove(deadBlock);
                    
                }

                _deadBlocks.Clear();
            }
        }
    }
}