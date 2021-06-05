using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace KOS.HexMap
{
    public class SaveLoadItem : MonoBehaviour
    {
        public SaveLoadMenu menu;
        private string mapName;
        
        public string MapName
        {
            get => MapName;
            
            set
            {
                mapName = value;
                transform.GetChild(0).GetComponent<Text>().text = value;
            }
        }
        
        
        public void Select() => menu.SelectItem(mapName);
    }
}
