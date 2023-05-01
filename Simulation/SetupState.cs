namespace Simulation
{
    public static class SetupState
    {
        public static bool Update(ref ECell[][] field)
        {
            string? inputStr = Console.ReadLine();

            if (inputStr == null) return false;

            string[] command = inputStr.Split(' ');

            if (command.Length > 0)
            {
                switch (command[0])
                {
                    case "done":
                        return true;
                    
                    case "put":
                        if (command.Length == 3)
                        {
                            int x, y;

                            if (!int.TryParse(command[1], out x) || !int.TryParse(command[2], out y))
                            {
                                Console.WriteLine("Wrong format");
                                break;
                            }

                            if (x >= field.Length || y >= field.Length)
                            {
                                Console.WriteLine("Position wrong");
                                break;
                            }

                            field[x][y] = field[x][y] == ECell.Alive ? ECell.Empty : ECell.Alive;
                        }
                        else 
                        {
                            Console.WriteLine("Wrong format");
                        }
                        break;
                }
            }

            return false;
        }
    }
}