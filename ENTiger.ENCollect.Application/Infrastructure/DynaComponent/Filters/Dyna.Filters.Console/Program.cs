namespace ENCollect.Dyna.Examples;
public static class Program
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("Choose a scenario (1..10). Enter Q to quit.");
            Console.WriteLine("  1) Demo1_StaticLinqExample");
            Console.WriteLine("  2) Demo2_DynamicTwoExample");
            Console.WriteLine("  3) Demo3_DynamicThreeExample");
            Console.WriteLine("  4) Demo4_OrExample");
            Console.WriteLine("  5) Demo5_NotExample");
            Console.WriteLine("  6) Demo6_HaslistOfStringsExample");
            Console.WriteLine("  7) Demo7_HasNestedScopeExample");
            Console.WriteLine("  8) Demo8_PlaceholderExample");
            Console.WriteLine("  9) Demo9_CascadingFlowExample");
            Console.WriteLine("  10) Demo10_HasSingleObjectExample");
            Console.Write("> ");

            var choice = Console.ReadLine()?.Trim().ToUpper();
            if (choice == "Q") break;

            switch (choice)
            {
                case "1":
                    Demo1_StaticLinqExample.Run();
                    break;
                case "2":
                    Demo2_DynamicTwoExample.Run();
                    break;
                case "3":
                    Demo3_DynamicThreeExample.Run();
                    break;
                case "4":
                    Demo4_OrExample.Run();
                    break;
                case "5":
                    Demo5_NotExample.Run();
                    break;
                case "6":
                    Demo6_HasListOfStringsExample.Run();
                    break;
                case "7":
                    Demo7_HasNestedScopeExample.Run();
                    break;
                case "8":
                    Demo8_PlaceholderExample.Run();
                    break;
                case "9":
                    Demo9_CascadingFlowExample.Run();
                    break;
                case "10":
                    Demo10_HasSingleObjectExample.Run();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }

        Console.WriteLine("Exiting the Dyna.Filters console demo...");
    }
}