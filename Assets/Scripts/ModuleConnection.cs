using System;
using System.Collections.Generic;
using UnityEngine;

namespace Classes
{
    [Serializable]
    public class ModuleConnection : MonoBehaviour
    {
        public GameObject ConnectionFrom;
        public GameObject ConnectionTo;
        public GameObject ObjectToTurnOn;
        public float RequiredDistance;
        public bool NeedsToBeSmaller;

        private GameManager gameManager;
        private ModuleDistanceCheck check1;
        private ModuleDistanceCheck check2;
        private SpriteRenderer progressbarSprite;

        private void Awake()
        {
            check1 = ConnectionFrom.AddComponent<ModuleDistanceCheck>();
            check1.Connection = this;

            check2 = ConnectionTo.AddComponent<ModuleDistanceCheck>();
            check2.Connection = this;

            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            progressbarSprite = ObjectToTurnOn.GetComponent<SpriteRenderer>();
        }
        
        public void CheckConnection()
        {
            if (check1.IsConnected() || check2.IsConnected())
            {
                progressbarSprite.sprite = gameManager.progressbarCompleted;
            }
            else
            {
                progressbarSprite.sprite = gameManager.progressbarUncompleted;
            }
        }
    }
}