using System.ComponentModel;
using UnityEngine;
using VolumetricLines;
using Directives;

namespace Enemy.Boss
{
    [RequireComponent(typeof(VolumetricLineBehavior))]
    [RequireComponent(typeof(BoxCollider))]
    public class BossGreatLaser : MonoBehaviour
    {
        public float MaxSize;
        public float StartSizeUp;
        public float SizeRate;
        public float StayTime;

        private VolumetricLineBehavior line;
        private Collider hitbox;
        private Timer startSizeUpTimer = new Timer();
        private Timer stayTimer = new Timer();
        private int state;
        
        private void Start()
        {
            line = GetComponent<VolumetricLineBehavior>();
            hitbox = GetComponent<Collider>();
            hitbox.enabled = false;
        }

        private void Update()
        {
            var dt = Time.deltaTime;
            switch (state)
            {
                case 0:
                    PredictionLine(dt);
                    break;
                case 1:
                    Expand(dt);
                    break;
                case 2:
                    Stay(dt);
                    break;
                case 3:
                    Shrink(dt);
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        private void PredictionLine(float dt)
        {
            if (startSizeUpTimer.IsTimeUp(dt, StartSizeUp))
                state = 1;
        }

        private void Expand(float dt)
        {
            if (line.LineWidth <= MaxSize)
                line.LineWidth += SizeRate * dt;
            else
            {
                state = 2;
                hitbox.enabled = true;
            }
        }

        private void Stay(float dt)
        {
            if (!stayTimer.IsTimeUp(dt, StayTime)) 
                return;
            state = 3;
            hitbox.enabled = false;
        }

        private void Shrink(float dt)
        {
            if (line.LineWidth >= 1)
                line.LineWidth -= SizeRate * dt;
            else
            {
                Game.Execute(Game.BossLaser, gameObject);
                Destroy(gameObject);
            }
        }
    }
}
