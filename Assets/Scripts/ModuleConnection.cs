using System;
using UnityEngine;

namespace Classes
{
    [Serializable]
    public class ModuleConnection : MonoBehaviour
    {
        public GameObject ConnectionTo;
        public GameObject ObjectToTurnOn;
        public float RequiredDistance;
        public bool NeedsToBeSmaller;
        public bool IsActive;

        private float actualDistance;
        
        public bool IsConnected()
        {
            actualDistance = Vector2.Distance(gameObject.transform.position, ConnectionTo.transform.position);
            return (NeedsToBeSmaller && actualDistance < RequiredDistance) ||
                    (!NeedsToBeSmaller && actualDistance > RequiredDistance);
        }

        public void Update()
        {
            if (IsActive)
            {
                Debug.DrawLine(transform.position, ConnectionTo.transform.position,
                    IsConnected() ? Color.green : Color.red);
            }
        }
    }
}