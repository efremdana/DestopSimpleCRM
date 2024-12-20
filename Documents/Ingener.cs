using System;

namespace Documents
{
    public struct Ingener
    {
        private bool answer;
        private int CountRequest;
        public bool GetAnswer()
        {
            CountRequest++;
            int RandomNumber = (new Random()).Next(CountRequest, 10);
            if (RandomNumber >= 7)
            {
                CountRequest = 0;
                answer = true;
            }
            return answer;
        }
    }
}
