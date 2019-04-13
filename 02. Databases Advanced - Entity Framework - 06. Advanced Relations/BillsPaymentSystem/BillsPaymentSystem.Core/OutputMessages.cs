using System.Text;

namespace BillsPaymentSystem.Core
{
    public class OutputMessages
    {
        public static string EnterId => "Enter userId: ";
        public static string Loading => "Loading...";
        public static string InvalidCommand => "Invalid Command! Try again";
        public static string EnterIdAndAmount => "Enter userId and bills amount separated by white space: ";
        public static string WelcomeString() => "Welcome to our BillPaymentSystem. Please choose one of the following options from the menu!";
        public static string MenuOptions()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("Press 1 to enter user's info Menu");
            sb.AppendLine("Press 2 to enter PayBill's Menu");
            sb.AppendLine("Press 9 to Exit");

            return sb.ToString();
        }
    }
}
