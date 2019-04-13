using BillsPaymentSystem.Models;

namespace BillsPaymentSystem.Initial
{
    public class UserInitializer
    {
        public static User[] GetUsers()
        {
            User[] users = new User[]
            {
                new User("aaaaaa", "aaaaaa", "aaaa@aaaa", "aaaaaaa"),
                new User("aaaaba", "aaaaba", "aaab@aaaa", "aaabaaa"),
                new User("aaabaa", "aaabaa", "aaba@aaaa", "abaaaaa"),
                new User("aabaaa", "aabaaa", "abaa@aaaa", "baaaaaa"),
                new User("abaaaa", "abaaaa", "baaa@aaaa", "aaaaaba"),
                new User("baabaa", "baabaa", "aaaa@baaa", "aaaabaa"),
                new User("aabaab", "aabaab", "aaaa@abaa", "aaaaaab")
            };

            return users;
        }
    }
}
