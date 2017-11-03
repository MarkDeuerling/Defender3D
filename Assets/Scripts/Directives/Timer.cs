namespace Directives
{
    public struct Timer
    {
        private float timeSum;

        public void Reset()
        {
            timeSum = 0;
        }
    
        public bool IsTimeUp(float dt, float matchTime)
        {
            timeSum += dt;
            return timeSum > matchTime;
        }
    }
}