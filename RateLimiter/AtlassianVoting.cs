
using System;
using System.Collections.Generic;
using System.Text;

namespace RateLimiter
{
    class AtlassianVoting
    {
        public const string NO_WINNER_CASE = "No winner present in the list";
        public const int MAX_VOTE_ALLOWED = 3;
        public const int PRIMARY_VOTE_POINT = 3;
        public const int SECOND_VOTE_POINT = 2;
        public const int THIRD_VOTE_POINT = 1;


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var person1 = new List<string>();
            person1.Add("John");
            person1.Add("Mary");

            var person2 = new List<string>();
            person2.Add("Mary");
            person2.Add("John");


            var allVotes = new List<List<string>>();
            allVotes.Add(person1);
            allVotes.Add(person2);

            var winner = findWinnerList(allVotes);
            Console.WriteLine("Winner is : " + winner);
            Console.ReadKey();
        }


        //solution 1
        public static String findWinner(List<String> votes)
        {
            var result = string.Empty;
            int max = 0;

            if (votes == null || votes.Count == 0)
            {
                return NO_WINNER_CASE;
            }

            var totalVoteCount = votes.Count;
            var dict = new Dictionary<string, int>();

            foreach (var vote in votes)
            {
                if (dict.ContainsKey(vote))
                {
                    dict[vote] = dict[vote] + 1;
                }
                else
                {
                    dict.Add(vote, 1);
                }

                if (dict[vote] >= max)
                {
                    result = vote;
                    max = dict[vote];

                    if (dict[vote] > totalVoteCount / 2)
                    {
                        return result;
                    }
                }
            }


            return result;
        }


        /// <summary>
        /// john, mary
        /// mary, Raj, john
        /// max 3 people vote
        /// </summary>
        /// <param name="votes"></param>
        /// <returns></returns>
        public static String findWinnerList(List<List<String>> votes)
        {
            var result = string.Empty;
            int maxPoints = 0;

            if (votes == null || votes.Count == 0)
            {
                return NO_WINNER_CASE;
            }

            var totalVoteCount = votes.Count;
            var dict = new Dictionary<string, int>();

            foreach (var voteList in votes)
            {
                for (int i = 0; i < voteList.Count; i++)
                {
                    var vote = voteList[i];
                    var point = GetPoints(i);

                    if (dict.ContainsKey(vote))
                    {
                        dict[vote] = dict[vote] + point;
                    }
                    else
                    {
                        dict.Add(vote, point);
                    }

                    if (dict[vote] >= maxPoints)
                    {
                        result = vote;
                        maxPoints = dict[vote];
                    }

                }
            }


            return result;
        }

        public static int GetPoints(int index)
        {
            if (index == 0)
            {
                return PRIMARY_VOTE_POINT;
            }
            if (index == 1)
            {
                return SECOND_VOTE_POINT;
            }
            if (index == 2)
            {
                return THIRD_VOTE_POINT;
            }

            return 0;
        }

    }
}
