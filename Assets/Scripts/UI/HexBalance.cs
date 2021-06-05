using System;
using KOS.Events;
using KOS.Scenes;
using TMPro;
using UnityEngine;


namespace KOS.UI
{
    public class HexBalance : UIWidget
    {
        [SerializeField] private TMP_Text balance;
        
        //> REGISTER FOR BALANCE CHANGE EVENT
        private void Awake() => EventManager.Active.onBalanceChanged += UpdateBalance;

        //> UPDATE CURRENT HEX BALANCE
        private void UpdateBalance(int newBalance) => balance.text = newBalance.ToString();
    }
}