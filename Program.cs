using AuthenticationApp;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

Welcome();

void Welcome()
{
    Console.WriteLine("Welcome To My Authentication App");

    while (true)
    {

        Console.WriteLine("[1] Register");
        Console.WriteLine("[2] Login");
        Console.WriteLine("[3] Exit");
        Console.WriteLine("Please select an option");

        var input = Console.ReadLine();
        
        switch (input)
        {
            case "1":
                Register();
                break;
            case "2":
                Login();
                break;
            case "3":
                Exit();
                return;
            default:
                Console.WriteLine("Invalid option. Please try again");
                break;
        }
    }
}

string HashPassword(string password)
{
    SHA256 hash = SHA256.Create();
    var pwdBytes = Encoding.Default.GetBytes(password);
    var hashedPassword = hash.ComputeHash(pwdBytes);
    return Convert.ToHexString(hashedPassword);
}

void Register()
{
    Console.Clear();
    Console.WriteLine("---------- Register ----------");
    Console.Write("Username: ");
    var username = Console.ReadLine();
    Console.Write("Password: ");
    var password = Console.ReadLine();

    using (var context = new Context())
    {
        try
        {
            var userExists = context.Users.Any(u => u.Username == username);

            if (userExists)
            {
                Console.WriteLine("User already exists.");
                return;
            }

            var HashedPassword = HashPassword($"{password}"); 

            context.Users.Add(new User() { Username = username, Password = HashedPassword });
            context.SaveChanges();
            Console.WriteLine("You have successfully registered.");
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine("An error occurred while saving the entity changes.");
            Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
        }
    }
}


void Login()
{
    Console.Clear();
    Console.WriteLine("---------- Login ----------");
    Console.Write("Username: ");
    var username = Console.ReadLine();
    Console.Write("Password: ");
    var password = Console.ReadLine();

    using (var context = new Context())
    {
        var userFound = context.Users.FirstOrDefault(u => u.Username == username);

        if (userFound != null)
        {
            if(HashPassword(password) == userFound.Password)
            {
                Console.WriteLine("You have successfully logged in.");
            }
            else
            {
                Console.WriteLine("Incorrect password. Please try again.");
            }
        }
        else
        {
            Console.WriteLine("User not found. Please try again.");
        }
    }
}
void Exit()
{
    Console.Clear();
    Console.WriteLine("Exiting the program...");
}




