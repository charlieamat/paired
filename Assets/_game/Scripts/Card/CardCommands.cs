using Zenject;

namespace Paired.Scripts.Card
{
    public class CardCommands
    {
        public class RevealCommand : Command
        {
        }

        public class HideCommand : Command
        {
        }

        public class SelectCommand : Command<int>
        {
        }
    }
}
