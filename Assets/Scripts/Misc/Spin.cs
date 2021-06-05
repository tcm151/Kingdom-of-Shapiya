using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOS.Powerups
{
    public class Spin : MonoBehaviour
    {
        [SerializeField] private float x = 1f;
        [SerializeField] private float y = 1f;
        [SerializeField] private float z = 1f;

        private void Update()
        {
            transform.Rotate(x, y, z);
        }
    }
}