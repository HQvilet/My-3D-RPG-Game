using System;
using System.Collections.Generic;

public class OnEventOccured : IEvent
{
    public OnEventOccured(string name)
    {
        this.eventName = name;
    }
    public string eventName;
}

public class OnCollectItemEvent : IEvent
{
    public OnCollectItemEvent(int ID, int amount)
    {
        this.itemID = ID;
        this.amount = amount;
    }
    public int itemID;
    public int amount;
}

public class OnInteractedNPC : IEvent
{
    public OnInteractedNPC(int ID)
    {
        this.itemID = ID;
    }
    public int itemID;
}

public class OnKillEnemyEvent : IEvent
{
    public OnKillEnemyEvent(string name)
    {
        this.type = name;
    }
    public string type;
}