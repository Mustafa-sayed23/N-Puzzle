using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Vertex
    {
        public int[] puzzleArr;
        public int row_index;
        public int col_index;
        public int g;   //level
        public Vertex parent;
        int size_arr_final;
        public int h;

        public Vertex(int[] state_arr, int c_index, int r_index, int level, int h, int size_arr, Vertex par_ver)
        {
            puzzleArr = new int[size_arr * size_arr];

            col_index = c_index;
            row_index = r_index;
            g = level;
            parent = par_ver;
            size_arr_final = size_arr;
            this.h = h;

            for (int i = 0; i < size_arr * size_arr; i++)
            {
                puzzleArr[i] = state_arr[i];
            }
        }

        public int hamming_function()
        {
            int count_of_wrong_positions = 0;
            for (int i = 0; i < (size_arr_final * size_arr_final); i++)
            {
                if (puzzleArr[i] != i + 1 && puzzleArr[i] != 0)
                {
                    count_of_wrong_positions++;
                }
            }

            return count_of_wrong_positions;
        }

        public int Calc_Manhhaten_Distance()
        {
            int manhatten_Distance = 0;
            int final_index;
            int current_index;
            int cur_row_index;
            int cur_col_index;
            int final_row_index;
            int final_col_index;
            for (int i = 0; i < (size_arr_final * size_arr_final); i++)
            {
                if (puzzleArr[i] == i + 1 || puzzleArr[i] == 0)
                {
                    manhatten_Distance += 0;
                }
                else
                {
                    current_index = i;
                    cur_row_index = (current_index / size_arr_final);
                    cur_col_index = current_index % size_arr_final;
                    final_index = puzzleArr[i] - 1;
                    final_row_index = (final_index / size_arr_final);
                    final_col_index = (final_index % size_arr_final);

                    manhatten_Distance += (Math.Abs((final_row_index - cur_row_index)) + Math.Abs((final_col_index - cur_col_index)));
                }
            }

            return manhatten_Distance;
        }

        public int Calc_Manhattan_for_adjacents(int man_distance, int b_index, int new_b_index, int element_val)
        {
            int final_index;
            int current_index;
            int cur_row_index;
            int cur_col_index;
            int final_row_index;
            int final_col_index;
            int current_index_after_swap;

            if (element_val == b_index + 1)
            {
                man_distance -= 1;
            }
            else if (element_val != b_index + 1)
            {
                current_index = new_b_index;
                cur_row_index = (current_index / size_arr_final);
                cur_col_index = current_index % size_arr_final;
                final_index = element_val - 1;

                final_row_index = (final_index / size_arr_final);
                final_col_index = (final_index % size_arr_final);

                man_distance -= (Math.Abs((final_row_index - cur_row_index)) + Math.Abs((final_col_index - cur_col_index)));

                current_index_after_swap = b_index;
                cur_row_index = (current_index_after_swap / size_arr_final);
                cur_col_index = (current_index_after_swap % size_arr_final);

                man_distance += (Math.Abs((final_row_index - cur_row_index)) + Math.Abs((final_col_index - cur_col_index)));
            }

            return man_distance;
        }


        public int Update_Hamming_adjacent(int hamm, int b_index, int new_b_index, int element_val)
        {
            if (element_val == b_index + 1) //from false pos to true pos
            {
                hamm--;
            }
            else if (element_val != b_index + 1)
            {
                if (element_val == new_b_index + 1)
                {
                    hamm++;
                }
                else
                {
                    hamm += 0;
                }
            }

            return hamm;
        }
    }
}
