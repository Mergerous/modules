using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Modules.ScriptableGraph
{
    [Serializable]
    public class Element
    {
        public string id;
        public string type;
        public Child[] children;
        public Connector[] attachedConnectors;
        public Endpoint connectorStart;
        public Endpoint connectorEnd;

        public string GetName()
        {
            if (children.IsNullOrEmpty())
            {
                return default;
            }
            foreach (Child child in children)
            {
                if (child.name == "Input")
                {
                    return child.children[0].name;
                }
            }

            return default;
        }

        public string GetTag()
        {
            if (children.IsNullOrEmpty())
            {
                return default;
            }
            foreach (var child in children)
            {
                if (child.name == "Tag")
                {
                    return child.children[0].name;
                }
            }

            return default;
        }
    }

    [Serializable]
    public class Child
    {
        public string type;
        public string name;
        public Child2[] children;
    }
    
    [Serializable]
    public class Child2
    {
        public string type;
        public string name;
    }

    [Serializable]
    public class Endpoint
    {
        public string endpointNodeId;
    }

    [Serializable]
    public class Connector
    {
        public string id;
    }

    [CreateAssetMenu]
    public class FigmaGraphImporter : SerializedScriptableObject
    {
        [SerializeField] private TextAsset json;

        public List<Element> root;

        [Button]
        public void Deserialize()
        {
            root = JsonConvert.DeserializeObject<List<Element>>(json.text);
        }

        public Element[] GetOutputConnectors(Element source)
        {
            return source.attachedConnectors
                .Select(connector => FindConnector(connector.id))
                .Where(connector => connector.connectorStart.endpointNodeId == source.id)
                .ToArray();
        }

        public Element FindConnector(string id)
        {
            return root.Find(e => e.type == "CONNECTOR" && e.id == id);
        }

        public Element FindNode(string id)
        {
            return root.Find(e => e.type == "FRAME" && e.id == id);
        }
    }
 }
