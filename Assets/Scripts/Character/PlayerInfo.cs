using System.Collections.Generic;

namespace Character
{
    public class PlayerInfo
    {
        private Dictionary<int, int> _dicTileCollection = new();
        private int[] _desireTiles;
        
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


        public void SetDesireTiles(int[] tiles)
        {
            _desireTiles = tiles;
        }
        
        public string GetScore()
        {
            int score = 0;
            foreach (var tileIdx in _desireTiles)
            {
                if (_dicTileCollection.TryGetValue(tileIdx, out var value))
                {
                    score += 5;
                    score += value;
                }
            }
            return score + "";
        }

        public void OnResetPlayerInfo()
        {
            _dicTileCollection = new Dictionary<int, int>();
        }
    }
}