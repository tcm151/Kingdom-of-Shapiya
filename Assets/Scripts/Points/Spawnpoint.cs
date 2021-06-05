using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOS.Points
{
    public class Spawnpoint : Checkpoint
    {
        override protected void OnValidate()
        {
            transform.localScale = new Vector3(radius, radius, radius);
        }
    }
}
