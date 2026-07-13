using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryEvents 
{
    public static Action<ItemInstance> OnItemPlaced;
    public static Action<ItemInstance> OnItemRemoved;
}
