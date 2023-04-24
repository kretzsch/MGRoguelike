using System.Collections.Generic;


/// <summary>
/// this is where the loadoutmanager from the main menu stores bought item data. 
/// this is done through the  TransferDataToLoadoutData() method.
/// </summary>
public static class LoadoutData
{
    public static Dictionary<string, int> selectedWeaponsAndAmmo = new Dictionary<string, int>();
}
