using UnityEngine;

namespace Paired.Scripts.Game
{
    public class GameResults : MonoBehaviour
    {
        public GameResult Result { get; private set; }

        public void Construct(GameResult result)
        {
            Result = result;
        }
    }

    public enum GameResult
    {
        Won,
        Lost
    }
}
