using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOS.Points
{
    abstract public class Point : MonoBehaviour
    {
        virtual public Vector3 position => transform.position;
        virtual public Vector3 forward => transform.forward;
        virtual public Vector3 right => transform.right;
        virtual public Vector3 up => transform.up;
    }
}

