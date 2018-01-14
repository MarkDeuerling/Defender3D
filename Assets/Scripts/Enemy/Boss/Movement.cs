using System.Collections.Generic;
using Directives;
using Statics;
using UnityEngine;

namespace Enemy.Boss
{
    [System.Serializable]
    public class Movement
    {
        public List<Vector3> Waypoint;
        public float Speed;
        public float StayTime;

        private Vector3 movePosition;
        private Timer timer = new Timer();
        
        public bool CanMove { get; set; }

        public void Init()
        {
            CanMove = true;
            movePosition = GetWaypoint();
        }
        
        public void Move(float dt, Boss boss)
        {
            if (!CanMove) 
                return;
            
            if (movePosition == boss.GetPosition())
            {
                if (!timer.IsTimeUp(dt, StayTime))
                    return;
                movePosition = GetWaypoint();
                timer.Reset();
            }
                
            var newPosition = Vector3.MoveTowards(
                boss.GetPosition(), 
                movePosition, 
                Speed * dt);
            boss.SetPosition(newPosition);
        }

        private Vector3 GetWaypoint()
        {
            return Waypoint[Random.Range(0, Waypoint.Count)];
        }
    }
}