using System.Collections.Generic;
using Managers;

namespace Gameplay
{
    public class Cube : Item
    {
        public override void OnInteract(BoardCell boardCell)
        {
            List<BoardCell> cellsInCluster = MatchManager.Instance.CheckBoardForMatchingClusters(boardCell);
            if (cellsInCluster.Count > 1)
            {
                PoolingManager.Instance.AddToPool(cellsInCluster);
            }
        }

        public override void Upgrade(int level)
        {
            switch (level)
            {
                case 0:
                    SpriteRenderer.sprite = SpriteManager.Instance.GetDefaultItemImage(ItemType);
                    break;
                case 1:
                    SpriteRenderer.sprite = SpriteManager.Instance.GetItemRocketImage(ItemType);
                    break;
                case 2:
                    SpriteRenderer.sprite = SpriteManager.Instance.GetItemBombImage(ItemType);
                    break;
                case 3:
                    SpriteRenderer.sprite = SpriteManager.Instance.GetItemVacuumImage(ItemType);
                    break;
            }
        }
    }
}
