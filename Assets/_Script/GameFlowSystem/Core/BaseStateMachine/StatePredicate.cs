using System;

namespace AdvanceFSM
{
    public interface IPredicate
    {
        bool Evaluate();
    }

    public class FuncPredicate : IPredicate
    {
        readonly Func<bool> func;

        public FuncPredicate(Func<bool> func)
        {
            this.func = func;
        }
        public bool Evaluate() => func.Invoke();

    }

    public class ActionPredicate : IPredicate
    {
        bool isInvoke;
        public ActionPredicate(Action action)
        {
            action += () => isInvoke = true;
        }

        public bool Evaluate()
        {
            if (isInvoke)
            {
                isInvoke = false;
                return true;
            }
            return false;
        }
    }
}