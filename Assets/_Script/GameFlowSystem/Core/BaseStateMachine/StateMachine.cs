using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace AdvanceFSM
{
    public abstract class StateMachine
    {
        protected StateNode current;
        protected HashSet<ITransition> anyTransitions = new();

        Dictionary<Type, StateNode> nodes = new();
        protected StateNode GetNode(IState state)
        {
            if (!nodes.ContainsKey(state.GetType()))
            {
                StateNode node = new StateNode(state);
                nodes.Add(state.GetType(), node);
            }
            return nodes[state.GetType()];   
        }

        public void SetState(IState state)
        {
            current = GetNode(state);
            current.state.Enter();
        }

        public void ChangeState(IState state)
        {
            if (current.state == state)
                return;

            current.state?.Exit();
            current = nodes[state.GetType()];
            current.state?.Enter();
        }

        ITransition GetValidTransition()
        {
            foreach (ITransition transition in anyTransitions)
                if (transition.Condition.Evaluate())
                    return transition;

            foreach (ITransition transition in current.transitions)
                if (transition.Condition.Evaluate())
                    return transition;
            return null;
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetNode(from).AddTransition(new Transition(GetNode(to).state, condition));
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            anyTransitions.Add(new Transition(GetNode(to).state, condition));
        }

        public void Update()
        {
            if (current == null)
                return;

            var transition = GetValidTransition();
            if (transition != null)
                ChangeState(transition.To);

            current.state?.Update();
        }

        public void PhysicUpdate()
        {
            current?.state.PhysicUpdate();
        }
    }
}

