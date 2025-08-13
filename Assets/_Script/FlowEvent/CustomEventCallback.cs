using System.Collections;
using System.Collections.Generic;
using DialogueGraph;
using QuestSystem;
using UnityEngine;
using UnityEngine.Profiling.Memory.Experimental;


public class IContentPackageRequest : IEvent
{
    public string path;
    public IContentPackageRequest(string packageID)
    {
        this.path = packageID;
    }
}

