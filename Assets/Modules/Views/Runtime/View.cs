using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Modules.UI.Views;
using UnityEngine;

namespace Modules.Views
{
    public sealed class View : MonoBehaviour
    {
        [SerializeField] private string key;
        [SerializeField] private List<State> states;
        [SerializeField] private List<View> children;
        [SerializeReference] private List<Element> elements;
        
        public string Key => key;

        public View this[string key] => children.Find(children => children.Key == key);

       public T GetElement<T>(string key, string from = "") where T : Element
        {
            if (string.IsNullOrEmpty(from))
            {
                return elements
                    .OfType<T>()
                    .First(element => element.Key == key);
            }
             
            return this[from].GetElement<T>(key);
        }

        private void Awake()
        {
            foreach (var element in elements)
            {
                element.Initialize();
            }
        }

        private void Start()
        {
            foreach (State state in states)
            {
                if (state.IsDefault)
                {
                    SetState(state.Key);
                }
            }
        }

        public void SetState(Enum key)
        {
            bool isFlags = key.GetType().GetCustomAttribute(typeof(FlagsAttribute)) != null;
            foreach (State state in states)
            {
                if ((isFlags && key.HasFlag(state.Key)) || (!isFlags && Equals(state.Key, key)))
                {
                    foreach (StateComponent stateBehaviour in state.Components)
                    {
                        stateBehaviour.Apply();
                    }
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var element in elements)
            {
                element.Dispose();
            }
        }
    }
}
