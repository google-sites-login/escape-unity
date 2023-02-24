using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemInfo itemInfo;

    public abstract void Use();
    public abstract void StopUse();
    public abstract void Reload();
    public abstract bool IsHold();
    public abstract string GetAmmo();
}