using System.Collections.Generic;

namespace Character
{
    public class PlayerInfo
    {
        private Dictionary<int, int> _dicTileCollection = new();
        private Dictionary<int, int> _requiredTileCollection = new();
        private float _fScore = 0;


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


        // TODO implement this function to set the desire tile
        private Dictionary<int, int> GetRequireTileCollection()
        {
            return new Dictionary<int, int>();
        }


        // TODO decide how to calculate the score based on the tile collection
        public string GetScore()
        {
            return _fScore + "";
        }

        public void OnResetPlayerInfo()
        {
            _requiredTileCollection = GetRequireTileCollection();
            _dicTileCollection = new Dictionary<int, int>();
            _fScore = 0;
        }
    }
}