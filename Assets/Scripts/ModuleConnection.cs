using System;
using UnityEngine;

namespace Classes
{
    public class ModuleConnection : MonoBehaviour
    {
        public GameObject ConnectionTo;
        public GameObject ObjectToTurnOn;
        public float RequiredDistance;
        public bool NeedsToBeSmaller;

        private float actualDistance;
        private GameManager gameManager;
        private LineRenderer line;
        private ModuleMove moduleMove;

        private void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            moduleMove = GetComponent<ModuleMove>();
            
            line = gameObject.AddComponent<LineRenderer>();
            line.startWidth = 0.08f;
            line.endWidth = line.startWidth;
            line.material = gameManager.lineMaterial;
        }

        public bool IsConnected()
        {
            actualDistance = Vector2.Distance(gameObject.transform.position, ConnectionTo.transform.position);
            return (NeedsToBeSmaller && actualDistance < RequiredDistance) ||
                    (!NeedsToBeSmaller && actualDistance > RequiredDistance);
        }

        public void Update()
        {
            if (moduleMove.isBeingDragged)
            {
                line.enabled = true;
                line.SetPositions(new [] { gameObject.transform.position, ConnectionTo.transform.position });
                line.material.SetColor("_Color", IsConnected() ? gameManager.lineColorConnected : gameManager.lineColorDisconnected);
            }
            else
            {
                line.enabled = false;
            }
        }
    }
}