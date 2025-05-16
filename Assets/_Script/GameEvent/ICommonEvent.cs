using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class OnCollectItemEvent : IEvent
{
    public OnCollectItemEvent(int ID, int amount)
    {
        this.ID = ID;
        this.amount = amount;
    }
    public int ID;
    public int amount;
}
