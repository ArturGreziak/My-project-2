using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int fruitCounter = 0;

    public void AddFruit()
    {
        fruitCounter += 1;

        Debug.Log($"Fruit Number {fruitCounter}!");
    }

    public int GetCollectedFruits()
    {
        return fruitCounter;
    }
}
