using System.Collections.Generic;
using Merge;
using Move;
using UnityEngine;

namespace Grid
{
    public class GridCollisions
    {
        private List<BlockView> _blocks;
        private GridModel _gridModel;

        public GridCollisions(List<BlockView> blocks, GridModel gridModel)
        {
            _blocks = blocks;
            _gridModel = gridModel;
        }

        public void HandleCollision(BlockView currentBlock)
        {
            if (!_gridModel.IsNextPosInGrid(currentBlock))
            {
                return;
            }

            foreach (var block in _blocks)
            {
                if (block != currentBlock)
                {
                    if (currentBlock.NextPosition == block.Position)
                    {
                        if (currentBlock.Id == block.Id)
                        {
                            currentBlock.Merge(block);
                            
                            currentBlock.Position = currentBlock.NextPosition;
                        }
                        else
                        {
                            currentBlock.NextPosition = currentBlock.Position;
                        }
                    }
                    else
                    {
                        currentBlock.Position = currentBlock.NextPosition;
                    }
                }
                
            }

            

        }
    }

    public interface ICollidible : IMergable, IPositioned, IMoveable
    {
        bool Destroy { get; set; }
    }
}