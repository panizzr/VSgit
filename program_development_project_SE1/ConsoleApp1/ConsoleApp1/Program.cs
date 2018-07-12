using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
   
}
enum Direction
{
    north,
    south,
    west,
    east
}

enum Actions
{
    use_machine,
    run_treadmill,
    cycle,
    ask_help,
    change_clothes,
    enter_class
}


// code from https://social.msdn.microsoft.com/Forums/vstudio/en-US/fe4d77f3-c3d2-4851-b5f4-b500e7f1716c/c-zork?forum=csharpgeneral
class Room
{
    public Dictionary<Direction, Room> options = new Dictionary<Direction, Room>();
    
    public string description;
}

class Action
{
    public Dictionary<Actions, Action> choices = new Dictionary<Actions, Action>();

    public string actName;
}
class Program
{
    static void Location(Room room)
    {
        string exitsText = string.Join(", ", room.options.Keys.ToArray());
        if (string.IsNullOrEmpty(exitsText))
        {
            exitsText = "None";
        }
        Console.WriteLine("{0}\n\n{1}\n\noptions are: {1}\n", room.description, exitsText);
    }

    static Action Sets()
    {
        Action use = new Action() { actName = "use machine"};
        Action run = new Action() { actName = "run on Treadmill" };

        use.choices.Add(Actions.use_machine,  run);
        use.choices.Add(Actions.run_treadmill, run);
        use.choices.Add(Actions.cycle, run);
        // changingRooms.options.Add(Actions.change_clothes);
        // groupClass.options.Add(Actions.enter_class);

        return use;
    }

    static Room Setup()
    {
        Room main = new Room() { description = "This is the gym main area. Lockers are on your left, classroom is right ahead and the helpdesk is on your right." };
        Room helpDesk = new Room() { description = "This is the gym's help desk." };
        Room changingRooms = new Room() {description = "This is the Locker Room. " };
        Room groupClass = new Room() {  description = "This is the Group Class Room." };
       
        main.options.Add(Direction.east, helpDesk);
        main.options.Add(Direction.west, changingRooms);
        main.options.Add(Direction.north, groupClass);
        helpDesk.options.Add(Direction.west, main);
        changingRooms.options.Add(Direction.east, main);
        groupClass.options.Add(Direction.south, main);

       

        return main;
    }
    static void Main()
    {
        Room currentRoom = Setup();
        Action currentAction = Sets();
        for (; ; )
        {
            Location(currentRoom);
            string command = Console.ReadLine();
            Direction direction;
            Actions act;
            if (Enum.TryParse<Direction>(command, out direction))       //stay in current room in case of wrong input
            {
                Room destination;
                if (currentRoom.options.TryGetValue(direction, out destination))
                {
                    currentRoom = destination;
                }
                else
                {
                    Console.WriteLine("You can't go that way.");
                }
            }

            /*else if (currentRoom = mainroom && ))     //add action option in main room
            {
                Action perform;
            }
            */
            else if (command == "exit")
            {
                return;
            }
            else
            {
                Console.WriteLine("That's not a command i recognize...");
               // Console.WriteLine("Try: {0}", string.Join(", ", Enum.GetNames(typeof(Direction))) + ", or exit");
            }
        }
    }
}
/*
Random numbergenerator = new Random ();

        int num1 = numbergenerator.Next(1,11);
        int num2 = numbergenerator.Next(1,11);
        int score = 0; // THIS IS THE SCORE

        Console.WriteLine("Whats " + num1 + " times " + num2 + "?");

        var answer = Convert.ToInt32(Console.ReadLine());

        if ( answer == num1 * num2) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Thats the correct answer!");
            Console.ResetColor();
            ++score; // Gives score
            Console.WriteLine("Your score: " + score);
        } else {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Bummer, try again!");
            Console.ResetColor();
            ++score; // Gives score
            Console.WriteLine("Your score: " + score);
        }
        goto start;
    }
}
}

