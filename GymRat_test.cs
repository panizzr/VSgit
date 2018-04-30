using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
   
}
enum Direction
{
    North,
    South,
    West,
    East,
    Up,
    Down
}

class Room
{
    public Dictionary<Direction, Room> exits = new Dictionary<Direction, Room>();
    public string name;
    public string description;
}

class Program
{
    static void Describe(Room room)
    {
        string exitsText = string.Join(", ", room.exits.Keys.ToArray());
        if (string.IsNullOrEmpty(exitsText))
        {
            exitsText = "None";
        }
        Console.WriteLine("{0}\n\n{1}\n\nExits Are: {2}\n", room.name, room.description, exitsText);
    }

    static Room Setup()
    {
        Room main = new Room() { name = "Main", description = "This is the main room." };
        Room eastWing = new Room() { name = "East Wing", description = "This is the east wing." };
        Room closet = new Room() { name = "Closet", description = "This is a closet.  The door locked behind you." };
        Room passage = new Room() { name = "Secret passage", description = "This is a long secret passage." };
        Room vestibule = new Room() { name = "Vestibule", description = "This is a vestibule." };

        main.exits.Add(Direction.East, eastWing);
        main.exits.Add(Direction.West, closet);
        eastWing.exits.Add(Direction.West, main);
        closet.exits.Add(Direction.Down, passage);
        passage.exits.Add(Direction.Up, closet);
        passage.exits.Add(Direction.East, vestibule);
        vestibule.exits.Add(Direction.West, passage);
        vestibule.exits.Add(Direction.Up, main);

        return main;
    }
    static void Main(string[] args)
    {
        Room currentRoom = Setup();

        for (; ; )
        {
            Describe(currentRoom);
            Console.Write(">");
            string command = Console.ReadLine();
            Direction direction;
            if (Enum.TryParse<Direction>(command, out direction))
            {
                Room destination;
                if (currentRoom.exits.TryGetValue(direction, out destination))
                {
                    currentRoom = destination;
                }
                else
                {
                    Console.WriteLine("You can't go that way.");
                }
            }
            else if (command == "Quit")
            {
                return;
            }
            else
            {
                Console.WriteLine("I don't understand.");
                Console.WriteLine("Try: {0}", string.Join(", ", Enum.GetNames(typeof(Direction))) + ", or Quit");
            }
        }
    }
}