﻿namespace G2048_3D.Infrastructure.StateMachine
{
    public interface IExitableState
    {
        void Exit();
    }

    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayloadState<T> : IExitableState
    {
        void Enter(T data);
    }
}