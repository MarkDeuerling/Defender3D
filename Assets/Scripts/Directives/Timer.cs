namespace Directives
{
    public struct Timer
    {
        private float timeSum;
        private bool hotStart;

        public void HotStart()
        {
            hotStart = true;
        }

        public void Reset()
        {
            timeSum = 0;
        }
    
        public bool IsTimeUp(float dt, float matchTime)
        {
            if (hotStart)
            {
                hotStart = false;
                timeSum = matchTime;
            }
            timeSum += dt;
            return timeSum > matchTime;
        }
    }
}