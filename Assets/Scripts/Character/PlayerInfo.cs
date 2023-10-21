using System.Collections.Generic;

namespace Character
{
    public class PlayerInfo
    {
        private Dictionary<int, int> _dicTileCollection = new();
        private float _fScore;


        public void CollectTile(int tileIdx)
        {
            if (_dicTileCollection.ContainsKey(tileIdx))
            {
                _dicTileCollection[tileIdx] += 1;
            }
            else
            {
                _dicTileCollection[tileIdx] = 1;
            }
        }


        public void OnResetPlayerInfo()
        {
            _dicTileCollection = new Dictionary<int, int>();
            _fScore = 0;
        }
    }
}