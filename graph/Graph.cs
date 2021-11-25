using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace graph
{
    public class Graph
    {
        private static int size = 15;
        private int count;
        int[] nodes;
        int[,] table = new int[size, size];
        List<int> ans;
        public Graph()
        {
            ans = new List<int>();
            nodes = new int[size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    table[i, j] = 0;
                }
                nodes[i] = 0;
            }

            InputFromFile();
        }
        private void InputFromFile()
        {
            string path = @"input.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                if ((line = reader.ReadLine()) == null)
                {
                    Console.WriteLine("No data in file");
                    return;
                }
                try
                {
                    this.count = Convert.ToInt32(line);
                }
                
                catch (FormatException)
                {
                    Console.WriteLine("Format Exception");
                    return;
                }
                if (this.count < 5)
                    Environment.Exit(1);
                int it = 0;
                while (it < count)
                {

                    line = reader.ReadLine(); //считывание вершины и с ней связных
                    if (line == "") continue;

                    string[] numbers = Regex.Split(line, @"\D+");
                    bool first = true;
                    int row = 0;
                    foreach (string value in numbers)
                    {
                        if (first == true)
                        {
                            row = int.Parse(value);
                            if (row < 0 || row >= count)
                            {
                                Console.WriteLine(row + " is outside from size");
                                break;
                            }
                            first = false;
                            continue;
                        }
                        if (!string.IsNullOrEmpty(value))
                        {
                            int colum = int.Parse(value);
                            if (colum < 0 || colum >= count)
                            {
                                Console.WriteLine(colum + " is outside from size");
                                continue;
                            }
                            table[row, colum] = 1;
                        }
                    }
                    it++;


                }

            }

        }
        public void AddNewNode()
        {
            Console.WriteLine("input adjacency vetrices (from 0 to " + count + ")");
            count++;
            string newVetrices = Console.ReadLine();
            string[] numbers = Regex.Split(newVetrices, @"\D+");
            foreach (string value in numbers)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int vetrex = int.Parse(value);
                    if (vetrex > 0 && vetrex < count)
                    {
                        table[count - 1, vetrex] = 1;
                    }

                }
            }

        }
        public void Print()
        {
            string line = "  ";
            for (int i = 0; i < count; i++)
            {
                line += i + " ";
            }
            Console.WriteLine(line);
            line = "";
            for (int i = 0; i < count; i++)
            {
                line += i + ":";
                for (int j = 0; j < count; j++)
                {
                    line += table[i, j] + " ";
                }
                Console.WriteLine(line);
                line = "";
            }
            //Console.WriteLine();
        }
        public void AddNewEdge()
        {
            Console.WriteLine("input two nodes between which to draw a new edge (from 0 to " + (count -1) + ")");
            string nodes = Console.ReadLine();
            string[] numbers = Regex.Split(nodes, @"\D+");
            int first = int.Parse(numbers[0]);
            int second = int.Parse(numbers[1]);
            if (first < 0 || second < 0 || first >= count || second >=count)
            {
                Console.WriteLine("one of the nodes was incorrect");
                return;
            }
            table[first, second] = 1;
        }
        public void RemoveEdge()
        {
            Console.WriteLine("input two nodes between which to delete a edge (from 0 to " + (count - 1) + ")");
            string nodes = Console.ReadLine();
            string[] numbers = Regex.Split(nodes, @"\D+");
            int first = int.Parse(numbers[0]);
            int second = int.Parse(numbers[1]);
            if (first < 0 || second < 0 || first >= count || second >= count)
            {
                Console.WriteLine("one of the nodes was incorrect");
                return;
            }
            if (table[first,second] == 0)
            {
                Console.WriteLine("there is no edge between this nodes");
                return;
            }
            table[first, second] = 0;
        }
        public void RemoveNode()
        {
            Console.WriteLine("input number of the nide which you want to remove (from 0 to "+(count-1)+")");
            int numb = Convert.ToInt32(Console.ReadLine());
            if (numb < 0 || numb >= count)
            {
                Console.WriteLine("you enter invalid data");
                return;
            }
            for (int i = numb; i < count - 1; i++)
            {
                
                for (int j = 0; j < count; j++)
                {
                    table[i, j] = table[i + 1, j];
                    table[i, count - 1] = 0;
                }
            } 
            for (int j = numb; j < count - 1; j++)
            {
                
                for (int i = 0; i < count; i++)
                {
                    table[i, j] = table[i, j + 1];
                    table[count - 1, j] = 0;
                }
            }
            count--;
        }
        public void PrintRight()
        {
            
            for (int i = 0; i < count; i++)
            {
                int countEdgesFromINode = 0;
                string nextNodes = "";
                for (int j = 0; j < count; j++)
                {
                    if (table[i, j] == 1)
                    {
                        countEdgesFromINode++;
                        nextNodes += j + " ";
                    }
                    
                }
                Console.WriteLine(i + ": " + countEdgesFromINode + ", nodes:" + nextNodes);
            }
        }
        /*
           
         void dfs (int v) {
	used[v] = true;
	for (size_t i=0; i<g[v].size(); ++i) {
		int to = g[v][i];
		if (!used[to])
			dfs (to);
	}
	ans.push_back (v);
        */
        private void search(int st)
        {
           
            //Console.Write((st) + " ");
            nodes[st] = 1;
            for (int i = 0; i < count; ++i)
            {
                int to = table[st, i];
                if ((table[st, i] == 1))
                    search(i);
            }
            ans.Add(st);
            //Console.WriteLine();
        }
        public void Search()
        {
            search(0);
        }
        public void Topological_sort()
        {
            for (int i = 0; i < count; ++i)
                nodes[i] = 0;
            ans.Clear();
            for (int i = 0; i < count; ++i)
                if (nodes[i] == 0)
                    search(i);
            ans.Reverse();

        }
     
        public void Output()
        {
            foreach (int a in ans)
            {
                Console.Write(a + " ");
            }
        }
    }

}
