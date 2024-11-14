using System;
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
        private LevelManager levelManager;

        private void Awake()
        {
            check1 = ConnectionFrom.AddComponent<ModuleDistanceCheck>();
            check1.Connection = this;

            check2 = ConnectionTo.AddComponent<ModuleDistanceCheck>();
            check2.Connection = this;

            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            progressbarSprite = ObjectToTurnOn.GetComponent<SpriteRenderer>();
            levelManager = GetComponent<LevelManager>();
        }

        public void CheckConnection()
        {
            // This is messed up. The else is for connection true actually
            if (check1.IsConnected() || check2.IsConnected())
            {
                progressbarSprite.sprite = gameManager.progressbarCompleted;
            }
            else 
            {
                progressbarSprite.sprite = gameManager.progressbarUncompleted;
                if (levelManager.CheckWin())
                {
                    levelManager.Win();
                    gameManager.IsWin = true;
                }
            }
        }

        public bool IsChecked() => !(check1.IsConnected() || check2.IsConnected());
    }
}