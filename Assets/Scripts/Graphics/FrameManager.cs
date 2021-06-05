
using UnityEngine;

namespace KOS.Graphics
{
    public class FrameManager : MonoBehaviour
    {
        public int targetFrameRate = 144;

        private void Start() => Application.targetFrameRate = targetFrameRate;

        //? requires testing to be removed or not
        private void Update()
        {
            if (Application.targetFrameRate != targetFrameRate) Application.targetFrameRate = targetFrameRate;
        }
    }
}
