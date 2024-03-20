# N-Puzzle
## A program to solve any solvable puzzle.

#### At first it checks if the puzzle is solvable or not, Then if it is solvable the program finds the shortest path to solve to the puzzle based on A* searching algorithm and print a STEP by STEP movements occur in the A* algorithms till you reach the final solvable board.
#### By applying of two priority function for a state (Heuristic value):

#### 1.	Hamming priority function: The number of blocks in the wrong position, plus the number of moves made so far to get to the state. Intuitively, a state with a small number of blocks in the wrong position is close to the goal state, and we prefer a state that has been reached using a small number of moves.

#### 2.	Manhattan priority function: The sum of the distances (sum of the vertical and horizontal distance) from the blocks to their goal positions, plus the number of moves made so far to get to the state.

#### For example, the Hamming and Manhattan priorities of the initial state:
![image](https://github.com/Mustafa-sayed23/N-Puzzle/assets/162192046/10da1502-19fe-4a1b-b816-8211252b9f67)

#### Total complexity = O(E log(V)) where E is the total number of moves and V is the number of states till reaching to the solution.
