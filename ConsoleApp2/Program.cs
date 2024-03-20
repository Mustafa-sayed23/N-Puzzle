using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ConsoleApp2
{
    class Program
    {
        public static void print_movement_step_by_step(Vertex U,int number_of_movements,int size)
        {
            List<Vertex> list_vertex = new List<Vertex>();
            for (int m=0;m<=number_of_movements;m++)
            {
                if(m==0)
                {
                    list_vertex.Add(U);
                }
                else
                {
                    list_vertex.Add(list_vertex[m-1].parent);    
                }
            }

            int step =0;
            Console.WriteLine("Movements step by step:\n");
            for (int k = (list_vertex.Count) - 1; k >= 0; k--)
            {
                if (k == (list_vertex.Count) - 1)
                {
                    Console.WriteLine("Intial State :");
                }
                else
                {
                    Console.WriteLine("Step:" + step ) ;
                }
                for (int j = 0; j < (size * size); j++)
                {
                    if ((j + 1) % size == 0)
                    {
                        Console.Write(list_vertex[k].puzzleArr[j]);

                        Console.WriteLine("\n");
                    }
                    else
                    {
                        Console.Write(list_vertex[k].puzzleArr[j] + "\t");
                    }
                }
                if(k==0)
                {
                    Console.Write("");
                }
                else
                {
                    Console.WriteLine("------------------");
                }
                step++;
            }
        }

        public static PriorityQueue<Vertex, int> pq = new PriorityQueue<Vertex, int>();
        public static void run_with_manhattan(int size)
        {
            while (true)
            {
                Vertex U = pq.Dequeue();

                if (U.h == 0)  //reached to goal state
                {
                    if (size == 3 || size == 4)
                    {
                        print_movement_step_by_step(U, U.g, size);
                    }
                    Console.WriteLine("Min number of moves to arrange the puzzle Equal " + U.g);
                    return;
                }

                int blank_block_index = (U.row_index * size) + U.col_index;
                for (int L = 0; L < 4; L++)  //adjacents of u
                {
                    if (L == 0)  //movement blank block to top
                    {
                        if ((blank_block_index - size) < 0)
                        {
                            continue;
                        }
                        else
                        {
                            int Newblank_block_index = blank_block_index - size;
                            int temp_value = U.puzzleArr[Newblank_block_index];
                            int blank_element = U.puzzleArr[blank_block_index];
                            int parent_banlk_index;

                            if (U.g == 0)  //if the intial state is the parent to vertex v
                            {
                                parent_banlk_index = U.row_index * size + U.col_index;
                            }
                            else
                            {
                                parent_banlk_index = U.parent.row_index * size + U.parent.col_index;
                            }
                            int r_index = (Newblank_block_index / size);
                            int c_index = (Newblank_block_index % size);
                            Vertex v = new Vertex(U.puzzleArr, c_index, r_index, ((U.g) + 1), 0, size, U);
                            v.puzzleArr[Newblank_block_index] = blank_element;
                            v.puzzleArr[blank_block_index] = temp_value;
                            v.h = v.Calc_Manhattan_for_adjacents(U.h, blank_block_index, Newblank_block_index, temp_value);

                            if (Newblank_block_index != parent_banlk_index) //ensure that the block not returnd to its location in it's parent state
                            {
                                pq.Enqueue(v, (v.h + v.g));
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }

                    else if (L == 1)   // movement blank block to bottom
                    {
                        if ((blank_block_index + size) > ((size * size) - 1))
                        {
                            continue;
                        }
                        else
                        {
                            int Newblank_block_index = blank_block_index + size;
                            int temp_value = U.puzzleArr[Newblank_block_index];
                            int blank_element = U.puzzleArr[blank_block_index];
                            int parent_banlk_index;

                            if (U.g == 0) //if the intial state is the parent to vertex v
                            {
                                parent_banlk_index = U.row_index * size + U.col_index;
                            }
                            else
                            {
                                parent_banlk_index = U.parent.row_index * size + U.parent.col_index;
                            }

                            int r_index = (Newblank_block_index / size);
                            int c_index = (Newblank_block_index % size);
                            Vertex v = new Vertex(U.puzzleArr, c_index, r_index, ((U.g) + 1), 0, size, U);
                            v.puzzleArr[Newblank_block_index] = blank_element;
                            v.puzzleArr[blank_block_index] = temp_value;
                            v.h = v.Calc_Manhattan_for_adjacents(U.h, blank_block_index, Newblank_block_index, temp_value);

                            if (Newblank_block_index != parent_banlk_index) //ensure that the block not returnd to its location in it's parent state
                            {
                                pq.Enqueue(v, (v.h + v.g));
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }

                    else if (L == 2)   //movement blank block to Right
                    {
                        if (U.col_index == (size - 1))
                        {
                            continue;
                        }
                        else
                        {
                            int Newblank_block_index = blank_block_index + 1;

                            int temp_value = U.puzzleArr[Newblank_block_index];
                            int blank_element = U.puzzleArr[blank_block_index];
                            int parent_banlk_index;

                            if (U.g == 0) //if the intial state is the parent to vertex v
                            {
                                parent_banlk_index = U.row_index * size + U.col_index;
                            }
                            else
                            {
                                parent_banlk_index = U.parent.row_index * size + U.parent.col_index;
                            }
                            int r_index = (Newblank_block_index / size);
                            int c_index = (Newblank_block_index % size);
                            Vertex v = new Vertex(U.puzzleArr, c_index, r_index, ((U.g) + 1), 0, size, U);
                            v.puzzleArr[Newblank_block_index] = blank_element;
                            v.puzzleArr[blank_block_index] = temp_value;
                            v.h = v.Calc_Manhattan_for_adjacents(U.h, blank_block_index, Newblank_block_index, temp_value);

                            if (Newblank_block_index != parent_banlk_index) //ensure that the block not returnd to its location in it's parent state
                            {
                                pq.Enqueue(v, (v.h + v.g));
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }

                    else if (L == 3)     //movement blank block to Left
                    {
                        if (U.col_index == 0)
                        {
                            continue;
                        }
                        else
                        {
                            int Newblank_block_index = blank_block_index - 1;

                            int temp_value = U.puzzleArr[Newblank_block_index];
                            int blank_element = U.puzzleArr[blank_block_index];
                            int parent_banlk_index;

                            if (U.g == 0)  //if the intial state is the parent to vertex v
                            {
                                parent_banlk_index = U.row_index * size + U.col_index;
                            }
                            else
                            {
                                parent_banlk_index = U.parent.row_index * size + U.parent.col_index;
                            }
                            int r_index = (Newblank_block_index / size);
                            int c_index = (Newblank_block_index % size);
                            Vertex v = new Vertex(U.puzzleArr, c_index, r_index, ((U.g) + 1), 0, size, U);
                            v.puzzleArr[Newblank_block_index] = blank_element;
                            v.puzzleArr[blank_block_index] = temp_value;
                            v.h = v.Calc_Manhattan_for_adjacents(U.h, blank_block_index, Newblank_block_index, temp_value);

                            if (Newblank_block_index != parent_banlk_index) //ensure that the block not returnd to its location in it's parent state
                            {
                                pq.Enqueue(v, (v.h + v.g));
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }

        public static void run_with_hamming(int size)
        {
            while (true)
            {
                Vertex U = pq.Dequeue();

                if (U.h == 0)  //reached to goal state
                {
                    if (size == 3 || size == 4)
                    {
                        print_movement_step_by_step(U, U.g, size);
                    }
                    Console.WriteLine("Min number of moves to arrange the puzzle Equal " + U.g);
                    return;
                }

                int blank_block_index = (U.row_index * size) + U.col_index;  //index of Blank_space

                for (int L = 0; L < 4; L++)  //adjacents of U
                {
                    if (L == 0)       //movement blank block to top
                    {
                        if ((blank_block_index - size) < 0)
                        {
                            continue;
                        }
                        else
                        {
                            int Newblank_block_index = blank_block_index - size;
                            int temp_value = U.puzzleArr[Newblank_block_index];
                            int blank_element = U.puzzleArr[blank_block_index];
                            int parent_banlk_index;

                            if (U.g == 0) //if the intial state is the parent to vertex v
                            {
                                parent_banlk_index = U.row_index * size + U.col_index;
                            }
                            else
                            {
                                parent_banlk_index = U.parent.row_index * size + U.parent.col_index;
                            }
                            int r_index = (Newblank_block_index / size);
                            int c_index = (Newblank_block_index % size);
                            Vertex v = new Vertex(U.puzzleArr, c_index, r_index, ((U.g) + 1), 0, size, U);
                            v.puzzleArr[Newblank_block_index] = blank_element;
                            v.puzzleArr[blank_block_index] = temp_value;
                            v.h = v.Update_Hamming_adjacent(U.h, blank_block_index, Newblank_block_index, temp_value);

                            if (Newblank_block_index != parent_banlk_index) //ensure that the block not returnd to its location in it's parent state
                            {
                                pq.Enqueue(v, (v.h + v.g));
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }

                    else if (L == 1)   // movement blank block to bottom
                    {
                        if ((blank_block_index + size) > ((size * size) - 1))
                        {
                            continue;
                        }
                        else
                        {
                            int Newblank_block_index = blank_block_index + size;
                            int temp_value = U.puzzleArr[Newblank_block_index];
                            int blank_element = U.puzzleArr[blank_block_index];
                            int parent_banlk_index;

                            if (U.g == 0) //if the intial state is the parent to vertex v
                            {
                                parent_banlk_index = U.row_index * size + U.col_index;
                            }
                            else
                            {
                                parent_banlk_index = U.parent.row_index * size + U.parent.col_index;
                            }
                            int r_index = (Newblank_block_index / size);
                            int c_index = (Newblank_block_index % size);
                            Vertex v = new Vertex(U.puzzleArr, c_index, r_index, ((U.g) + 1), 0, size, U);
                            v.puzzleArr[Newblank_block_index] = blank_element;
                            v.puzzleArr[blank_block_index] = temp_value;
                            v.h = v.Update_Hamming_adjacent(U.h, blank_block_index, Newblank_block_index, temp_value);

                            if (Newblank_block_index != parent_banlk_index) //ensure that the block not returnd to its location in it's parent state
                            {
                                pq.Enqueue(v, (v.h + v.g));
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }

                    else if (L == 2)   //movement blank block to Right
                    {
                        if (U.col_index == (size - 1))
                        {
                            continue;
                        }
                        else
                        {
                            int Newblank_block_index = blank_block_index + 1;
                            int temp_value = U.puzzleArr[Newblank_block_index];
                            int blank_element = U.puzzleArr[blank_block_index];
                            int parent_banlk_index;
                            if (U.g == 0)  //if the intial state is the parent to vertex v
                            {
                                parent_banlk_index = U.row_index * size + U.col_index;
                            }
                            else
                            {
                                parent_banlk_index = U.parent.row_index * size + U.parent.col_index;
                            }
                            int r_index = (Newblank_block_index / size);
                            int c_index = (Newblank_block_index % size);
                            Vertex v = new Vertex(U.puzzleArr, c_index, r_index, ((U.g) + 1), 0, size, U);
                            v.puzzleArr[Newblank_block_index] = blank_element;
                            v.puzzleArr[blank_block_index] = temp_value;
                            v.h = v.Update_Hamming_adjacent(U.h, blank_block_index, Newblank_block_index, temp_value);

                            if (Newblank_block_index != parent_banlk_index) //ensure that the block not returnd to its location in it's parent state
                            {
                                pq.Enqueue(v, (v.h + v.g));
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }

                    else if (L == 3)     //movement blank block to Left
                    {
                        if (U.col_index == 0)
                        {
                            continue;
                        }
                        else
                        {
                            int Newblank_block_index = blank_block_index - 1;
                            int temp_value = U.puzzleArr[Newblank_block_index];
                            int blank_element = U.puzzleArr[blank_block_index];
                            int parent_banlk_index;

                            if (U.g == 0)  //if the intial state is the parent to vertex v
                            {
                                parent_banlk_index = U.row_index * size + U.col_index;
                            }
                            else
                            {
                                parent_banlk_index = U.parent.row_index * size + U.parent.col_index;
                            }
                            int r_index = (Newblank_block_index / size);
                            int c_index = (Newblank_block_index % size);
                            Vertex v = new Vertex(U.puzzleArr, c_index, r_index, ((U.g) + 1), 0, size, U);
                            v.puzzleArr[Newblank_block_index] = blank_element;
                            v.puzzleArr[blank_block_index] = temp_value;
                            v.h = v.Update_Hamming_adjacent(U.h, blank_block_index, Newblank_block_index, temp_value);

                            if (Newblank_block_index != parent_banlk_index) //ensure that the block not returnd to its location in it's parent state
                            {
                                pq.Enqueue(v, (v.h + v.g));
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }
        static int Inversion_Count(int[] a, int size) //this function calc the inversion count
        {
            int inve = 0;
            for (int i = 0; i < size * size - 1; i++)
            {
                for (int j = i + 1; j < size * size; j++)
                {
                    if (a[i] != 0 && a[j] != 0 && (a[i] > a[j]))
                    {
                        inve += 1;
                    }
                }
            }
            return inve;
        }
        static bool solvability(int count, int size, int row_number)
        {
            if (size % 2 != 0)
            {
                if (count % 2 != 0)
                {
                    return false;   //puzzle not solvable
                }
                else
                {
                    return true;   //puzzle is solvable
                }
            }

            else
            {
                if (count % 2 != 0)  //inversion count is odd
                {
                    if (row_number % 2 == 0) // Row Number is even
                    {
                        return true;
                    }

                    else                 // Row Number is odd
                    {
                        return false;
                    }
                }
                else               //inversion count is even
                {
                    if (row_number % 2 != 0)  //Row Number is odd
                    {
                        return true;
                    }
                    else                 //Row Number is even
                    {
                        return false;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("[1]Sample test cases\n[2]Complete testing\n");
            Console.Write("Enter your choice [1-2]: ");

            int choice1 = Convert.ToInt32(Console.ReadLine());
            if (choice1 == 1)  // Sample test
            {
                var sample = Directory.EnumerateFiles(@"Testcases\Sample", "*.txt");
                foreach (string file in sample)
                {
                    string[] problem_name = file.Split('\\');
                    int row_index = 0;
                    int row_number = 0;
                    int col_index = 0;
                    int size = 0;
                    bool size_flag = true;
                    int i = 0;
                    int[] result = {};
                    String filename = File.ReadAllText(file);
                    foreach (var row in filename.Split('\n'))
                    {
                        if (row == "\r") { continue; }
                        if (size_flag)
                        {
                            size_flag = false;
                            size = Convert.ToInt32(row);
                            result = new int[size * size];
                            continue;
                        }

                        foreach (var col in row.Trim().Split(' '))
                        {
                            if (i == (size * size)) { break; }

                            result[i] = Convert.ToInt32(col);
                            if (result[i] == 0)
                            {
                                row_number = size - (i / size);    //row number non zero based from down used in inversion count
                                row_index = (i / size);            //row index zero based
                                col_index = i % size;             // column index zero based
                            }
                            i++;
                        }
                    }

                    int count = Inversion_Count(result, size);
                    if (solvability(count,size,row_number) == false)
                    {
                        Console.WriteLine(problem_name[problem_name.Length - 1] + " is UNSOLVABLE");
                        Console.WriteLine("========================================================================================================================");
                    }
                    else
                    {
                        Console.WriteLine(problem_name[problem_name.Length - 1] + " is SOLVABLE");
                        //public Vertex(int[] state_arr, int c_index, int r_index, int level, int h, int size_arr, Vertex par_ver)
                        Vertex ver = new Vertex(result, col_index, row_index,0, 0, size, null);//intial state
                        ver.h = ver.hamming_function();
                        pq.Enqueue(ver, (ver.h + ver.g));

                        Stopwatch sw1 = new Stopwatch();
                        sw1.Start();
                        run_with_hamming(size);
                        sw1.Stop();
                        Console.WriteLine("Execution Time is : "+sw1.Elapsed);
                        Console.WriteLine("========================================================================================================================");
                        pq.Clear();
                    }
                }
            }

            else if (choice1 == 2)  //Complete test
            {
                Console.WriteLine("[1]Run Hamming & Manhattan and unsolvable cases\n[2]Run Manhattan and large case\n");
                Console.Write("Enter your choice [1-2]: ");
                int choice2 = Convert.ToInt32(Console.ReadLine());

                if (choice2 == 1)    //hamming and manhattan and unsolvable test cases
                {
                    var complete1 = Directory.EnumerateFiles(@"Testcases\Complete\Manhattan & Hamming and Unsolvable Puzzles", "*.txt");
                    foreach (string file in complete1)
                    {
                        string[] problem_name = file.Split('\\');
                        int row_index = 0;
                        int row_number = 0;
                        int col_index = 0;
                        int size = 0;
                        bool size_flag = true;
                        int i = 0;
                        int[] result = {};

                        String filename = File.ReadAllText(file);
                        foreach (var row in filename.Split('\n'))
                        {
                            if (row == "\r") { continue; }
                            if (size_flag)
                            {
                                size_flag = false;
                                size = Convert.ToInt32(row);
                                result = new int[size * size];
                                continue;
                            }

                            foreach (var col in row.Trim().Split(' '))
                            {
                                if (i == (size * size)) { break; }

                                result[i] = Convert.ToInt32(col);
                                if (result[i] == 0)
                                {
                                    row_number = size - (i / size);    //row number non zero based from down used in inversion count
                                    row_index = (i / size);           //row index zero based
                                    col_index = i % size;            // column index zero based
                                }
                                i++;
                            }
                        }

                        int count = Inversion_Count(result, size);
                        if (solvability(count, size, row_number) == false)
                        {
                            Console.WriteLine(problem_name[problem_name.Length - 1]+" is UNSOLVABLE");
                            Console.WriteLine("========================================================================================================================");
                        }
                        else
                        {
                            Console.WriteLine(problem_name[problem_name.Length - 1]+" is SOLVABLE\n");
                            
                            Console.WriteLine("Running Hamming.....");
                            Vertex ver = new Vertex(result, col_index, row_index, 0,0, size, null);//intial state
                            ver.h = ver.hamming_function();
                            pq.Enqueue(ver, (ver.h + ver.g));

                            Stopwatch sw1 = new Stopwatch();
                            sw1.Start();
                            run_with_hamming(size);
                            sw1.Stop();
                            Console.WriteLine("Execution Time is : " + sw1.Elapsed+"\n");
                            pq.Clear();
                            
                            Console.WriteLine("Running Manhattan.....");
                            ver.h = ver.Calc_Manhhaten_Distance();
                            pq.Enqueue(ver, (ver.h + ver.g));

                            Stopwatch sw2 = new Stopwatch();
                            sw2.Start();
                            run_with_manhattan(size);
                            sw2.Stop();
                            Console.WriteLine("Execution Time is : " + sw2.Elapsed);
                            Console.WriteLine("========================================================================================================================");
                            pq.Clear();
                        }
                    }
                }

                else if (choice2 == 2)      //manhatten and large case
                {
                    var complete2 = Directory.EnumerateFiles(@"Testcases\Complete\Manhattan and large case", "*.txt");
                    foreach (string file in complete2)
                    {
                        string[] problem_name = file.Split('\\');
                        int row_index = 0;
                        int row_number = 0;
                        int col_index = 0;
                        int size = 0;
                        bool size_flag = true;
                        int i = 0;
                        int[] result = {};

                        String filename = File.ReadAllText(file);
                        foreach (var row in filename.Split('\n'))
                        {
                            if (row == "\r") { continue; }
                            if (size_flag)
                            {
                                size_flag = false;
                                size = Convert.ToInt32(row);
                                result = new int[size * size];
                                continue;
                            }

                            foreach (var col in row.Trim().Split(' '))
                            {
                                if (i == (size * size)) { break; }

                                result[i] = Convert.ToInt32(col);
                                if (result[i] == 0)
                                {
                                    row_number = size - (i / size);    //row number non zero based from down used in inversion count
                                    row_index = (i / size);           //row index zero based
                                    col_index = i % size;            // column index zero based
                                }
                                i++;
                            }
                        }

                        int count = Inversion_Count(result, size);
                        if (solvability(count, size, row_number) == false)
                        {
                            Console.WriteLine(problem_name[problem_name.Length - 1]+" is UNSOLVABLE");
                            Console.WriteLine("========================================================================================================================");
                        }
                        else
                        {
                            Console.WriteLine(problem_name[problem_name.Length - 1]+"  is SOLVABLE");
                            Vertex ver = new Vertex(result, col_index, row_index, 0,0, size, null);//intial state
                            ver.h = ver.Calc_Manhhaten_Distance();
                            pq.Enqueue(ver, (ver.h + ver.g));

                            Stopwatch sw = new Stopwatch();
                            sw.Start();
                            run_with_manhattan(size);
                            sw.Stop();
                            Console.WriteLine("Execution Time is : " + sw.Elapsed);
                            Console.WriteLine("========================================================================================================================");
                            pq.Clear();
                        }
                    }
                }
            }
        }
    }
}
