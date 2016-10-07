using UnityEngine.Networking;
using Zenject;
using Paired.Scripts.Order;

namespace Paired.Scripts.Matching
{
    public class Matcher : NetworkBehaviour
    {
        private int _selectedId = -1;

        private IOrderHolder _orderHolder;
        private MatchCommand _matchCommand;
        private MismatchCommand _mismatchCommand;

        [Inject]
        public void Construct(IOrderHolder orderHolder, MatchCommand matchCommand, MismatchCommand mismatchCommand)
        {
            _orderHolder = orderHolder;
            _matchCommand = matchCommand;
            _mismatchCommand = mismatchCommand;
        }

        public void SelectCard(int id)
        {
            if (_selectedId == -1)
            {
                _selectedId = id;
            }
            else
            {
                if (_orderHolder.Order[_selectedId] == _orderHolder.Order[id])
                {
                    _matchCommand.Execute(); 
                }
                else
                {
                    _mismatchCommand.Execute(_selectedId, id);
                }

                _selectedId = -1;
            }
        }
    }
}