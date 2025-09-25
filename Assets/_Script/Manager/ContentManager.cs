using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ContentManager : Singleton<ContentManager>
{

    // Dictionary<string, ICotent

    protected override void Awake()
    {
        base.Awake();
        Bus<IContentPackageRequest>.AddRegister(OnContentRequested);
        // Path = path;
    }

    private void OnContentRequested(IContentPackageRequest request)
    {
        // ICoResources.Load("asfasf");
        IContentPackage package = Resources.Load(request.path) as IContentPackage;
        if (package == null)
        {
            Debug.Log("Package request failed to load.");
            return;
        }
        package.AddPackageToScene();
        Debug.Log(package);

    }

    public string path;

    [ContextMenu("Package/Load package")]
    public void LoadPackage()
    {
        Bus<IContentPackageRequest>.Raise(new IContentPackageRequest(path));
    }
}
