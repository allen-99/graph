using System;


namespace graph
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Graph graph = new Graph();
            graph.Print();
            graph.PrintRight();
            graph.Search();
            graph.Topological_sort();
            graph.Output();
           
        }
    }
}
