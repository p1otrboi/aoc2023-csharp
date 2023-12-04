// Entry point for the application

using aoc2023_csharp.src;

if (args.Length > 0)
{
    switch (args[0])
    {
        case "1":
            Day_01.Run();
            break;
        case "2":
            Day_02.Run();
            break;
        case "3":
            Day_03.Run();
            break;
        default:
            Console.WriteLine("Something went wrong. Usage: dotnet run [day]");
            break;
    }
}
else
{
    Console.WriteLine("Please provide a day. Usage: dotnet run [day]");
}