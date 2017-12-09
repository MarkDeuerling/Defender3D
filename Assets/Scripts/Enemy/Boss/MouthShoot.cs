using System;
using System.Collections;
using Statics;
using UnityEngine;
using VolumetricLines;

namespace Enemy.Boss
{
    [Serializable]
    public class MouthShoot
    {
        public GameObject Laser;
        public Vector3 Offset;
        public float Period;
        public float MaxSize;
        public float SizeUp;
        public float SizeUpRate;

        private VolumetricLineBehavior line;

        public void Shoot(GameObject entity, MonoBehaviour mb)
        {
            var laser = GameObject.Instantiate(Laser);
            line = laser.GetComponent<VolumetricLineBehavior>();
            var offset = Offset - Vector3.right * line.EndPos.z;
            laser.SetPosition(entity.GetPosition() + offset);
            mb.StartCoroutine(LaserSizeUp(laser));
        }

        private IEnumerator LaserSizeUp(GameObject entity)
        {
            yield return new WaitForSeconds(1f);
            var collider = Laser.gameObject.GetComponent<Collider>();
            collider.gameObject.SetActive(true);
            while (line.LineWidth <= MaxSize)
            {
                line.LineWidth += SizeUp;
                yield return new WaitForSeconds(SizeUpRate);    
            }
            yield return new WaitForSeconds(Period);
            while (line.LineWidth > 0)
            {
                line.LineWidth -= SizeUp;
                yield return new WaitForSeconds(SizeUpRate);    
            }
            GameObject.Destroy(entity);
        }
    }
}