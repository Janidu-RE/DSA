using System;
using System.Collections.Generic;

namespace FlightRes
{
    public class Graph
    {
        // Adds an edge with a weight (flight price)
        public static void AddEdge(int[,] mat, int i, int j, int weight)
        {
            mat[i, j] = weight;
        }

        // Displays the adjacency matrix
        public static void DisplayGraph(int[,] mat)
        {
            int n = mat.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{mat[i, j]}    ");
                }
                Console.WriteLine();
            }
        }

        // Finds and displays all paths from start to end using DFS
        public static void FindAllPaths(int[,] mat, int start, int end, string[] countries)
        {
            var visited = new bool[mat.GetLength(0)];
            var path = new List<int>();
            var allPaths = new List<List<int>>();

            DFS(mat, start, end, visited, path, allPaths);

            if (allPaths.Count == 0)
            {
                Console.WriteLine("No paths found.");
                return;
            }

            Console.WriteLine("\nAvailable Paths:");
            foreach (var p in allPaths)
            {
                Console.Write("Path: ");
                for (int i = 0; i < p.Count; i++)
                {
                    Console.Write(countries[p[i]]);
                    if (i < p.Count - 1)
                    {
                        Console.Write(" -> ");
                    }
                }
                Console.WriteLine($", Total Cost: {CalculatePathCost(mat, p)}");
            }
        }

        private static void DFS(int[,] mat, int current, int end, bool[] visited, List<int> path, List<List<int>> allPaths)
        {
            visited[current] = true;
            path.Add(current);

            if (current == end)
            {
                allPaths.Add(new List<int>(path));
            }
            else
            {
                for (int i = 0; i < mat.GetLength(0); i++)
                {
                    if (mat[current, i] > 0 && !visited[i])
                    {
                        DFS(mat, i, end, visited, path, allPaths);
                    }
                }
            }

            path.RemoveAt(path.Count - 1);
            visited[current] = false;
        }

        private static int CalculatePathCost(int[,] mat, List<int> path)
        {
            int totalCost = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                totalCost += mat[path[i], path[i + 1]];
            }
            return totalCost;
        }

        // Displays the list of available countries
        public static void DisplayCountries(string[] countries)
        {
            Console.WriteLine("\nAvailable Countries:");
            for (int i = 0; i < countries.Length; i++)
            {
                Console.WriteLine($"{i}. {countries[i]}");
            }
        }

        // Prompts the user to select a country by number
        public static int SelectCountry(string[] countries, string prompt)
        {
            int choice;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice >= 0 && choice < countries.Length)
                    {
                        return choice;
                    }
                }
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
}
