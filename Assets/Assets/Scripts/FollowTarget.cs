using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;

      


        private void Update()
        {
            if (target.GetComponent<ThrowBall>().Thrown)
            {
                if (target.position.z < 4)
                {
                    transform.position = target.position + offset;
                }
            }
        }
    }
}
