// See https://aka.ms/new-console-template for more information

using System;
using System.Net.NetworkInformation;
using System.Collections;

class DC
{
    // static string, never changes.
    static string cheatsheet = @"
    Command	Operation
    q       Exit the program.

    p   	Prints the value on the top of the stack and ends the statement with a newline.
    n   	Prints the value on the top of the stack and ends the line with a null statement.
    f   	Prints the entire stack, without any alteration.
    P   	Pops the value from the top of the stack.
    c   	Clear the stack.
    d   	Duplicates the top value and push it into the main stack.
    r   	Reverses the order of top two elements in the stack.
    Z   	Pops the value from the stack, calculate the number of digits in it and pushes that number.
    X   	Pops the value from the stack, calculate the number of fraction digits in it and pushes that number.
    z   	Pushes the stack length into the stack.
    i   	Pops the value from the stack and uses it as input radix.
    o   	Pops the value from the stack and uses it as output radix.
    k   	Pops the values from the stack and uses it to set precision.
    I   	Pushes the value of input radix into the stack.
    O   	Pushes the value of output radix into the stack
    K   	Pushes the precision value into the stack.
    ";

    public static void WriteCheatSheet()
    {
        Console.WriteLine(cheatsheet);
    }

    public static void Main()
    {
        Stack rpn = new Stack();
        WriteCheatSheet();

        DC.Repl(rpn);
    }

    public static void Repl(Stack rpn)
    {
        bool warning = false;

        while (true)
        {
            // Prompt reveals the last thing in the stack.
            if (rpn.Count > 0)
            {
                Console.Write($"{rpn.Peek()}> ");
            }
            else
            {
                Console.Write(" > ");
            }

            string input = Console.ReadLine();

            // Things which are parseable as numbers are stored as numbers.
            if (int.TryParse(input, out int number))
            {
                rpn.Push(number);
                continue;
            }

            // Things which are parseable as +, -, x or / are stored as operators.
            if (input == "+" || input == "-" || input == "x" || input == "/")
            {
                if (rpn.Count < 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Not enough items on the stack to perform operation.");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    int a = Convert.ToInt32(rpn.Pop());
                    int b = Convert.ToInt32(rpn.Pop());
                    if (input == "+")
                    {
                        rpn.Push(a + b);
                    }
                    else if (input == "-")
                    {
                        rpn.Push(a - b);
                    }
                    else if (input == "x")
                    {
                        rpn.Push(a * b);
                    }
                    else if (input == "/")
                    {
                        rpn.Push(a / b);
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(rpn.Peek());
                    Console.ResetColor();

                    continue;
                }
            }

            if (input == "q")
            {
                // Exit the program.
                System.Environment.Exit(0);
            }

            if (input == "p")
            {
                Console.WriteLine(rpn.Peek());
            }
            else if (input == "n")
            {
                Console.Write(rpn.Peek());
            }
            else if (input == "f")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                foreach (var item in rpn)
                {
                    Console.WriteLine(item);
                }

                Console.ResetColor();
            }
            else if (input == "c")
            {
                rpn.Clear();
            }
            else if (input == "P")
            {
                rpn.Pop();
            }
            else if (input == "d")
            {
                rpn.Push(rpn.Peek());
            }
            else if (input == "r")
            {
                object a = rpn.Pop();
                object b = rpn.Pop();
                rpn.Push(a);
                rpn.Push(b);
            }
            else if (input == "Z")
            {
                rpn.Push(rpn.Pop().ToString().Length);
            }
            else if (input == "X")
            {
                rpn.Push(rpn.Pop().ToString().Split('.')[1].Length);
            }
            else if (input == "z")
            {
                rpn.Push(rpn.Count);
            }
            else if (input == "i")
            {
                int inputRadix = Convert.ToInt32(rpn.Pop());
                Console.WriteLine("Input radix set to " + inputRadix);
            }
            else if (input == "o")
            {
                int outputRadix = Convert.ToInt32(rpn.Pop());
                Console.WriteLine("Output radix set to " + outputRadix);
            }
            else if (input == "k")
            {
                int precision = Convert.ToInt32(rpn.Pop());
                Console.WriteLine("Precision set to " + precision);
            }
            else if (input == "I")
            {
                rpn.Push(10);
            }
            else if (input == "O")
            {
                rpn.Push(10);
            }
            else if (input == "K")
            {
                rpn.Push(10);
            }
            else
            {
                if (warning == false)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Unhandled input - silently dropping. Use 'q' to exit.");
                    warning = true;
                    Console.ResetColor();
                }

                continue;
            }
        }
    }
}