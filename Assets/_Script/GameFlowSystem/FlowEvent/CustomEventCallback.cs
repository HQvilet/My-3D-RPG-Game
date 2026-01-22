

public class IContentPackageRequest : IEvent
{
    public string path;
    public IContentPackageRequest(string packageID)
    {
        this.path = packageID;
    }
}

