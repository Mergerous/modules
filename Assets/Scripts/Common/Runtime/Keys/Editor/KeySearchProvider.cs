using System;
using System.Collections.Generic;
using Modules.Common.Structures;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Modules.Common
{
    public class KeySearchProvider : ScriptableObject, ISearchWindowProvider
    {
        public event Action<Key> OnElementSelected;
        public Key? selectedKey;

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchList = new List<SearchTreeEntry>();
            var searchGroup = new SearchTreeGroupEntry(new GUIContent(nameof(KeysSettings)));
            searchList.Add(searchGroup);
            foreach (KeysSettings keysSettings in KeySettingsPostProcessor.settings)
            {
                string path = AssetDatabase.GetAssetPath(keysSettings);
                KeysSettings asset = AssetDatabase.LoadAssetAtPath<KeysSettings>(path);
                
                SearchTreeGroupEntry group = new SearchTreeGroupEntry(new GUIContent(keysSettings.name), 1);
                searchList.Add(group);
                for (int i = 0; i < keysSettings.variants.Count; i++)
                {
                    SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(keysSettings.variants[i]))
                    {
                        level = 2,
                        userData = new Key(asset.id, i)
                    };
                    searchList.Add(entry);
                }
            }
            return searchList;
        }

        public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
        {
            if (entry.userData is Key key)
            {
                OnElementSelected?.Invoke(key);
                selectedKey = key;
            }
         
            return true;
        }
    }
}
