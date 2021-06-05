using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KOS.ItemData;
using KOS.Containers;

namespace KOS.Player
{
    public class Inventory : MonoBehaviour, IContainer
    {
        public List<Data> testItems;
        private Dictionary<Data, int> inventory;

        private void Awake()
        {
            inventory = new Dictionary<Data, int>();

            foreach (var item in testItems)
            {
                Deposit(item);
            }

            foreach (var item in inventory)
            {
                Debug.Log(item.Key.name + ": " + item.Value);
            }
        }

        public bool Take(Data item)
        {
            if (!Contains(item)) return false;

            inventory[item]--;
            if (inventory[item] <= 0) inventory.Remove(item);
            return true;
        }

        public bool Deposit(Data item)
        {
            if (!Contains(item)) inventory.Add(item, 1);
            else inventory[item]++;
            return true;
        }

        public bool Contains(Data item) => inventory.ContainsKey(item);
    }
}
