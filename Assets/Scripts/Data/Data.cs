using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOS.ItemData
{
    abstract public class Data : ScriptableObject
    {
        [Header("Basic Information")]
        new public string name;
        public string description;
        public Sprite image;
    }
}

