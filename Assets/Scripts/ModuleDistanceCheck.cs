using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Classes
{
    public class ModuleDistanceCheck : MonoBehaviour
    {
        [DoNotSerialize] public ModuleConnection Connection;

        private float actualDistance;
        private GameManager gameManager;
        private LineRenderer line;
        private ModuleMove moduleMove;

        private void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            
            line = gameObject.AddComponent<LineRenderer>();
            line.startWidth = 0.08f;
            line.endWidth = line.startWidth;
            line.material = gameManager.lineMaterial;
            
            moduleMove = GetComponent<ModuleMove>();
        }

        public bool IsConnected()
        {
            actualDistance = Vector2.Distance(Connection.ConnectionFrom.transform.position, Connection.ConnectionTo.transform.position);
            return ((Connection.NeedsToBeSmaller && actualDistance > Connection.RequiredDistance) ||
                   (!Connection.NeedsToBeSmaller && actualDistance < Connection.RequiredDistance));
        }

        public void Update()
        {
            if (moduleMove.isBeingDragged)
            {
                Connection.CheckConnection();
                line.enabled = true;
                line.SetPositions(new [] { Connection.ConnectionFrom.transform.position, Connection.ConnectionTo.transform.position });
                line.material.SetColor("_Color", IsConnected() ? gameManager.lineColorDisconnected : gameManager.lineColorConnected);
            }
            else
            {
                line.enabled = false;
            }
        }
    }
}