using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootable : MonoBehaviour
{
    public List<LootPossibility> possibleLoot;
    public List<LootRecieved> finalLoot;

    public bool wasLootCalculated;

}

[System.Serializable]
public class LootPossibility
{
    public GameObject item;
    public int ammountMin;
    public int ammountMax;

}

[System.Serializable]
public class LootRecieved
{   
    public GameObject item;
    public int ammount;

}
