using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Modules.Remote.Editor
{
    public class RemoteSearchProvider : ScriptableObject, ISearchWindowProvider
    {
        public event Action<Type> OnElementSelected;
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchList = new List<SearchTreeEntry>();
            SearchTreeGroupEntry searchGroup = new SearchTreeGroupEntry(new GUIContent("Select Factory"));
            searchList.Add(searchGroup);
            
            foreach (Type type in TypeCache.GetTypesWithAttribute(typeof(AddToRemoteAttribute)))
            {
                AddToRemoteAttribute attribute = type.GetCustomAttribute<AddToRemoteAttribute>();
   
                string[] path = attribute.searchName.Split('/');
                int indent = 1;
                if (path.Length > 1)
                {
                    for (int i = 0; i < path.Length - 1; i++)
                    {
                        SearchTreeGroupEntry group = new SearchTreeGroupEntry(new GUIContent(path[i]), indent);
                        searchList.Add(group);
                        indent++;
                    }
                }

                SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(path[^1]))
                {
                    level = indent,
                    userData = type
                };
                
                searchList.Add(entry);
            }
            return searchList;
        }

        public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
        {
            if (entry.userData is Type type)
            {
                OnElementSelected?.Invoke(type);
            }
         
            return true;
        }
    }
}
