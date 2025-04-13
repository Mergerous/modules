using System;
using System.Collections.Generic;
using Modules.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.UI.Views
{
    [Serializable]
    public sealed class ListElement : Element
    {
        [Serializable]
        private class Template
        {
            public string key;
            public View prefab;
        }

        [SerializeField] private List<View> instances;
        [SerializeField] private List<Template> templates;
        [SerializeField] private Transform content;
        
        public View this[int i] => instances[i];

        public View CreateInstance(string key)
        {
            Template template = templates.Find(template => string.Equals(template.key, key, StringComparison.InvariantCulture));
            View instance = Object.Instantiate(template.prefab, content);
            instance.gameObject.SetActive(true);
            instances.Add(instance);
            return instance;
        }

        public void RemoveInstance(View instance)
        {
            if (instances.Remove(instance))
            {
                Object.Destroy(instance.gameObject);
            }
        }

        public void Clear()
        {
            foreach (var instance in instances)
            {
                Object.Destroy(instance.gameObject);
            }
            
            instances.Clear();
        }

        public override void Initialize()
        {
            
        }

        public override void Dispose()
        {
            
        }
    }
}
