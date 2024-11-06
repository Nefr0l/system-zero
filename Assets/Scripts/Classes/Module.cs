using System.Collections.Generic;
using UnityEngine;

namespace Classes
{
    [System.Serializable] 
    public class Module
    {
        public GameObject Object;
        public List<ModuleConnection> ModulesToConnect;
        public Vector2 StartingPosition;
        
        private bool switchedOn;

        public Module(GameObject o, List<ModuleConnection> modulesToConnect, Vector2 startingPosition)
        {
            Object = o;
            ModulesToConnect = modulesToConnect;
            StartingPosition = startingPosition;
        }

        public void Initialize()
        {
            Object.transform.position = StartingPosition;
            GameObject.Instantiate(Object);
        }
    }

    public struct ModuleConnection
    {
        public Module moduleToConnect;
        public float requiredDistance;
        public bool SetOnMaxDistanceMode;
    }
}