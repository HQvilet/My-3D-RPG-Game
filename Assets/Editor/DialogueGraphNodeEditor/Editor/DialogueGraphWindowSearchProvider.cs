using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PlasticGui.WorkspaceWindow;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


public class SearchContextElement
{
    public object target { get; private set; }
    public string menuItem { get; private set; }

    public SearchContextElement(object target, string title)
    {
        this.target = target;
        this.menuItem = title;
    }
    
}

public class DialogueGraphWindowSearchProvider : ScriptableObject, ISearchWindowProvider
{
    public DialogueGraphView graphView;
    public VisualElement target;


    public static List<SearchContextElement> elements;

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> tree = new List<SearchTreeEntry>();

        tree.Add(new SearchTreeGroupEntry(new GUIContent("Nodes"), 0));

        elements = new List<SearchContextElement>();
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (Assembly assembly in assemblies)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.CustomAttributes.ToList() != null)
                {
                    Attribute att = type.GetCustomAttribute(typeof(NodeInfoAttribute));
                    if (att != null)
                    {
                        NodeInfoAttribute nodeInfo = (NodeInfoAttribute)att;
                        var node = Activator.CreateInstance(type);
                        if (!string.IsNullOrEmpty(nodeInfo.MenuItem))
                            elements.Add(new SearchContextElement(node, nodeInfo.MenuItem));
                    }
                }
            }
        }



        HashSet<string> groups = new HashSet<string>();
        foreach (SearchContextElement element in elements)
        {
            // Debug.Log(element.menuItem);
            string[] entryItem = element.menuItem.Split('/');
            string groupName = "";
            for (int i = 0; i < entryItem.Length - 1; ++i)
            {
                groupName += entryItem[i];
                if (!groups.Contains(groupName))
                {
                    tree.Add(new SearchTreeGroupEntry(new GUIContent(entryItem[i]), i+1));
                    groups.Add(groupName);
                }
                groupName += "/";
            }
            SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(entryItem.Last()));
            entry.level = entryItem.Length;
            entry.userData = new SearchContextElement(element.target, element.menuItem);

            tree.Add(entry);
        }

        return tree;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        Vector2 windowMousePosition = graphView.ChangeCoordinatesTo(graphView, context.screenMousePosition - graphView.window.position.position);
        Vector2 graphMousePosition = graphView.contentContainer.WorldToLocal(windowMousePosition);

        SearchContextElement element = (SearchContextElement)SearchTreeEntry.userData;
        DialogueGraphNode node = (DialogueGraphNode)element.target;
        node.Rect = new Rect(graphMousePosition, new Vector2());
        graphView.Add(node);
        return true;
    }
}
