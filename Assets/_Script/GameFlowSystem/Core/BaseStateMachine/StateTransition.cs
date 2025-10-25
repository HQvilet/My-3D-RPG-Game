using System;

namespace AdvanceFSM
{
    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }

    public class Transition : ITransition
    {
        public IState To { get; }

        public IPredicate Condition { get; }
        public Transition(IState toState, IPredicate predicate)
        {
            To = toState;
            Condition = predicate; 
        }
    }
}