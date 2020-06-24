using System;
using System.Collections.Generic;
using System.Text;

namespace Atlassian
{
    public class RateLimiter
    {
        public string actionName { get; set; }
        public double maxAllowed { get; set; }
        public TimeSpan interval { get; set; }
        public string userId { get; set; }



        public Dictionary<string, List<DateTime>> dictionary { get; set; }


        public RateLimiter()
        {
            this.dictionary = new Dictionary<string, List<DateTime>>();
        }

        public bool Limit()
        {
            var userId = GetUserName();
            var invalidList = new List<DateTime>();

            var existingData = dictionary.TryGetValue(userId, out List<DateTime> timeHistory);

            if(!existingData)
            {
                //Console.WriteLine("userdata does not exist");
                var userAccessHistory = new List<DateTime>();
                if (this.maxAllowed > 0)
                {
                    userAccessHistory.Add(DateTime.Now);
                    dictionary.Add(userId, userAccessHistory);
                    return true;
                }
                else
                {
                    return false;
                    //throw new RateLimiterExceeededException("You have exceeded the Limit. Please wait");
                }
            }
            else
            {
                //Console.WriteLine("userdata exist");

                //cleansing of out of timeinterval
                foreach (var val in timeHistory)
                {
                    if(DateTime.Now - val > interval)
                    {
                        invalidList.Add(val);
                    }
                }

                foreach(var val in invalidList)
                {
                    timeHistory.Remove(val);
                }

                if(timeHistory.Count >= maxAllowed)
                {
                    return false;
                    //throw new RateLimiterExceeededException("You have exceeded the Limit. Please wait");
                }
                else
                {
                    timeHistory.Add(DateTime.Now);
                    dictionary[userId] = timeHistory;
                }

                Console.WriteLine($"actions left :  {maxAllowed- timeHistory.Count}");

            }
            return true;
        }

        public string GetUserName()
        {
            return this.userId + "_" + this.actionName;
        }
    }

    public class RateLimiterExceeededException : Exception
    {
        public RateLimiterExceeededException()
        {
        }

        public RateLimiterExceeededException(string message)
            : base(message)
        {
        }
    }
}
