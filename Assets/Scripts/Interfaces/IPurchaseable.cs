/// <summary>
/// Interface that gets inherited by the scriptable objects 
/// that are purchasable in the main menu 
/// the reason for using an interface is for decoupling it 
/// for easy further expansion in the UI and the offer of purchaseables.
/// </summary>
public interface IPurchaseable
{
    string ItemName { get; }
    int Cost { get; }
}