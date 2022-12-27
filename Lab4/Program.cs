using UsersClass;

internal class Program
{
    private static void Main(string[] args)
    {
        User usr = new User("Testing", "Jim", "Jommia-Sale", "test@mail.ru", User.StringToDate("15-11-2000"));
        Console.WriteLine(usr.ToString());
        string str = "Testing, Jima, Jommia-Sous,a@yandex.ru , 11-11-2024";
        usr.FillFromString(str);
        Console.WriteLine(usr.ToString());
    }
}