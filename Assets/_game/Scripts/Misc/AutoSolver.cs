using Paired.Scripts.Card;
using Paired.Scripts.Order;
using UnityEngine;
using Zenject;

public class AutoSolver : ITickable
{
    const float delay = 0.05f;

    private int _count;
    private IOrderHolder _orderHolder;
    private CardCommands.SelectCommand _selectCommand;
    
    private int _matchId;
    private int _nextCardId = -1;
    private float _elapsed = -1f;

    public AutoSolver([Inject(Id = "CardCount")] int count, IOrderHolder orderHolder, CardCommands.SelectCommand selectCommand)
    {
        _count = count;
        _orderHolder = orderHolder;
        _selectCommand = selectCommand;
    }

    public void Tick()
    {
        if (_elapsed < delay)
        {
            _elapsed += Time.deltaTime;
            return;
        }

        if (_nextCardId == -1)
        {
            bool firstCardSelected = false;

            for (int i = 0; i < _orderHolder.Order.Length; i++)
            {
                if (_orderHolder.Order[i] == _matchId)
                {
                    if (firstCardSelected)
                    {
                        _nextCardId = i;
                        _matchId++;
                        break;
                    }
                    else
                    {
                        _selectCommand.Execute(i);
                        firstCardSelected = true;
                    }
                }
            }
        }
        else
        {
            _selectCommand.Execute(_nextCardId);
            _nextCardId = -1;
        }

        _elapsed = 0;
    }
}
