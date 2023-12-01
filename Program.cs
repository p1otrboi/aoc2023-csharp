// Entry point for the application

if (args.Length > 0)
{
    switch (args[0])
    {
        case "1":
            day_01.Run();
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