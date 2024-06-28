using System;

namespace CagataySc
{
    public static class EventsHandler
    {
        #region MainEvents
   
        public static CustomEvent OnTurnEnd{ get; } = new ();
        public static CustomEvent<NewPlayer,NewAI> OnCombatStart{ get; } = new ();
        public static CustomEvent<bool> OnCombatEnd{ get; } = new ();
        
        #endregion

        #region CustomEvents

        #endregion
        
        public static void ClearAllSubscribers()
        {
            OnTurnEnd.ClearSubscribers();
            OnCombatStart.ClearSubscribers();
            OnCombatEnd.ClearSubscribers();
        }
    }
    
    public class CustomEvent
    {
        public event Action Event;

        public void Invoke()
        {
            Event?.Invoke();
        }
        
        public void ClearSubscribers()
        {
            Event = null;
        }
    }
    
    public class CustomEvent<T>
    {
        public event Action<T> Event;

        public void Invoke(T arg)
        {
            Event?.Invoke(arg);
        }
        
        public void ClearSubscribers()
        {
            Event = null;
        }
    }
    
    public class CustomEvent<T1,T2>
    {
        public event Action<T1,T2> Event;

        public void Invoke(T1 arg1,T2 arg2)
        {
            Event?.Invoke(arg1,arg2);
        }
        
        public void ClearSubscribers()
        {
            Event = null;
        }
    }
}