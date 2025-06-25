namespace Modules.States
{
    public interface IState
    {
        void Open();
        void Close();
        
        void OnReturn()
        {
            
        }

        void OnReopen()
        {
            Close();
            Open();
        }
        
        void OnLinkedStateClosed()
        {
            
        }
    }

    public interface IState<in T> : IState
    {
        T Payload { set; }
    }
}
