using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryEvents // new animations :(
{
    public static Action<ItemInstance> OnItemPlaced;
    public static Action<ItemInstance> OnItemRemoved;
}
