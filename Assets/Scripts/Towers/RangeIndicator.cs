using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOS.Towers
{
    public class RangeIndicator : MonoBehaviour
    {
        [SerializeField] private Material rangeIndication;

        public float scale
        {
            set => transform.localScale = Vector3.one * value;
            get
            {
                float scale = 0f;
                scale += transform.localScale.x;
                scale += transform.localScale.y;
                scale += transform.localScale.z;
                return scale / 3;
            }
        }

        public bool active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

    }
}
