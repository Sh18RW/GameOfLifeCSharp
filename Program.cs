using Simulation;

namespace LifeGame
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Welcome to LifeGame simulation by Sh18RW!");

            var simulation = new Simulation(fieldSize: 20);

            simulation.Play();
        }
    }
}