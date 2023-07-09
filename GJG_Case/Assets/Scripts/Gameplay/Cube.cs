using Managers;

namespace Gameplay
{
    public class Cube : Item
    {
        public override void OnInteract(BoardCell boardCell)
        {
            MatchManager.Instance.CheckBoardForMatchingCluster(boardCell);
        }
    }
}
