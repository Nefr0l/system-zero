using System;
using UnityEngine;

namespace Classes
{
    [Serializable]
    public class Connection
    {
        public GameObject ConnectionFrom;
        public GameObject ConnectionTo;
        public float RequiredDistance;
        public bool MustBeSmaller;
        
        public GameObject LineObject;
        private LineRenderer Line;
        public bool StateChanged;
    
        public Connection(GameObject connectionFrom, GameObject connectionTo, float requiredDistance, bool mustBeSmaller)
        {
            ConnectionFrom = connectionFrom;
            ConnectionTo = connectionTo;
            RequiredDistance = requiredDistance;
            MustBeSmaller = mustBeSmaller;
        }

        public float GetDistance()
        {
            return Vector2.Distance(ConnectionFrom.transform.position, ConnectionTo.transform.position);
        }

        public void PlayConnectionSound()
        {
            AudioSource source = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>();

            if (source.isPlaying == false && !GameManager.IsWin) 
                GameManager.PlaySound
                    (GameObject.FindGameObjectWithTag("GameController").
                    GetComponent<GameManager>().ModuleConnectedSound);
        }

        public bool IsConnected()
        {
            float distance = Vector2.Distance(ConnectionFrom.transform.position, 
                ConnectionTo.transform.position);
            
            return (MustBeSmaller && distance < RequiredDistance) ||
                   (MustBeSmaller == false && distance > RequiredDistance);
        }

        public void UpdateLine(Vector2 from, Vector2 to)
        {
            Line.SetPositions(new []{ new Vector3(from.x, from.y, -10), new Vector3(to.x, to.y, -10)});
        }
    }
    
}