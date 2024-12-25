
public class User
{
    private string _username;
    private string _password;

    //Constructor to initialize a user
    public User(string Username, string Password)
    {
        _username = Username;
        _password = Password;
    }

    //public properties to access private fields
    public string Username
    {
        get
        {
            return _username;
        }
        set
        {
            _username = value;
        }
    }
    public string Password
    {
        get
        {
            return _password;
        }
        set
        {
            _password = value;
        }
    }
}

//Abstract class defining an authentication mechanism
public abstract class Authenticator
{

    //Abstract method for user registration
    public abstract void Register(string Username, string Password);
    // abstract method for user login
    public abstract void Login(string Username, string Password);

}


public class Authentication : Authenticator
{
        //List of registered users
    List<User> users = new List<User>();
    public override void Login(string username, string password)
    {
        // throw new NotImplementedException();
        foreach (var user in users)
        {
            if (user.Username == username && user.Password == password)
            {
                System.Console.WriteLine("User succesfully logged in");

            }
            System.Console.WriteLine("Wrong username or password");
        }

    }

    public override void Register(string Password, string Username)
    {
        // throw new NotImplementedException();
        if (string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(Password))
        {
            System.Console.WriteLine("Succesfully registered");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Authentication authentication1 = new Authentication();
        while (true)
        {
            System.Console.WriteLine("1. Please Register");
            System.Console.WriteLine("2. Please Login");
            System.Console.WriteLine("3. Exit");

            int choice = int.Parse(System.Console.ReadLine());
            switch (choice)
            {
                case 1:
                    
                    // System.Console.WriteLine("Register");
                    break;
                case 2:

                    // System.Console.WriteLine("Login");
                    break;
                case 3:

                    // System.Console.WriteLine("Exit");
                    break;
                default:
                    System.Console.WriteLine("Kindly confirm with the provided options");
                    break;
            }

        }
    }
}