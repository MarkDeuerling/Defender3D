namespace Directives
{
    public struct Timer
    {
        private float timeSum;

        public void HotStart()
        {
            timeSum = float.MaxValue;
        }

        public void Reset()
        {
            timeSum = 0;
        }
    
        public bool IsTimeUp(float dt, float matchTime)
        {
            if (timeSum > matchTime)
                return true;
            timeSum += dt;
            return false;
        }
    }
}