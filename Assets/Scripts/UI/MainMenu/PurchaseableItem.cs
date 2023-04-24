using UnityEngine;

public abstract class PurchaseableItem : ScriptableObject
{
    public abstract string ItemName { get; }
    public abstract int Cost { get; }
}
