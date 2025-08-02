using System;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.States
{
    public interface IState
    {
        void Open()
        {
            throw new NotImplementedException();
        }

        Task OpenAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        
        void Close();
    }
    
    public interface IResultState<T> : IState
    {
        new Task<T> OpenAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public interface IResultState<in TPayload, TResult> : IResultState<TResult>, IState<TPayload>
    {
        
    }

    public interface IState<in T> : IState
    {
        T Payload { set; }
    }
}
