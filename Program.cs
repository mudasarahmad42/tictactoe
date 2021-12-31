using System;
using System.Threading;

namespace TicTacToe
{
    class Program
    {
        //Store the current condition of the game
        int gameResult = 0;

        //We need the print specific characters and have to keep
        // record of them in code
        enum players { _, X, O };

        //Intilising 3x3 Array for 9 cell tic tac toe board
        int[,] gridState = new int[3, 3]
        {
            {0, 0, 0},
            {0, 0, 0},
            {0, 0, 0}
        };

        static void Main(string[] args)
        {
            //Creating objects
            Program obj = new Program();
            Random rand = new Random();

            string chooseAI = "";
            string testHuman = "human";
            string testAI = "ai";
            string weakAI = "weak";
            string strongAI = "strong";

            int turnComplete = 0;


            //Printing introduction
            string str = "Welcome!";
            string str2 = "I Have Developed this tic tac toe game using Minimax Algorithm\n" +
                          "with alpha beta pruning.That lets you play with other people or against the computer \nLets Start!\n\n\n";
            string textToEnter = "| Artificially Intelligent Tic Tac Toe |";
            string dashes = "________________________________________";

            //To perfectly center the heading on console
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter.Length / 2)) + "}", dashes));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter.Length / 2)) + "}", textToEnter));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter.Length / 2)) + "}", dashes));

            //Using for each loop to print string char by char
            foreach (char i in str)
            {
                Console.Write(i);
                Thread.Sleep(10);
            }
            Console.WriteLine("");
            Thread.Sleep(200);

            foreach (char z in str2)
            {
                Console.Write(z);
                Thread.Sleep(10);
                //Reads the string char by char when it finds a dot ' . '
                //it moves to the next line
                if (z == 46)
                {
                    Console.WriteLine();
                }
            }

        //************************Setting Preferences*********************
        takeInputAgain:
            Console.WriteLine("Do you want to play against another Human or AI (Human / AI)");
            string choosePlayer = Console.ReadLine();
            choosePlayer.ToLower();
            Console.WriteLine("");

            if (!choosePlayer.Equals("human") && !choosePlayer.Equals("ai"))
            {
                Console.WriteLine("Please Choose from the given Options\n~Human\n~AI");
                goto takeInputAgain;
            }

            //takeAIagain:
            if (choosePlayer.Equals("ai"))
            {
                Console.WriteLine("Do you want to play against Weak AI or Strong AI (Weak / Strong)");
                chooseAI = Console.ReadLine();
                chooseAI.ToLower();
                Console.WriteLine("");
            }

            /*This check worked when i first wrote and compiled it but after some time it stopped working
             i have spent quite time on it but couldn,t figure out the error. It is probably because of GOTO statement because if you
             do not choose ai it goes into infinite loop*/

            //if (!chooseAI.Equals("weak") && !chooseAI.Equals("strong"))
            //{
            //    Console.WriteLine("Please choose from the given options\n~Weak\n~Strong");
            //    goto takeAIagain;
            //}




            /*This is the main WHILE loop of the game*/
            while (obj.gameResult == 0)
            {
                //resetting the turn variable
                turnComplete = 0;
            //obj.Draw();

            again:
                //no turn has taken so its time for make a move
                while (turnComplete == 0)
                {
                    if (string.Equals(choosePlayer, testHuman))
                    {
                        Console.WriteLine("Its your Turn Player 1");
                    }
                    else
                    {
                        Console.WriteLine("Its your turn Human!!!");
                    }


                    //Storing an integer number entered by user
                    //subtracting 1 from it as array index starts from 0
                    //but a layman dont know this
                    //So its better if take [0][0] index in my grid as row: 1 and column: 1
                    Console.Write("Row: ");
                    int row = Convert.ToInt32(Console.ReadLine()) - 1;

                    //Same for the Column
                    Console.Write("Column: ");
                    int column = Convert.ToInt32(Console.ReadLine()) - 1;

                    //if the entered cell is available
                    if (obj.gridState[row, column] == 0)
                    {
                        //write 1 there as enum at 1 index is
                        // X, so human takes X.
                        obj.gridState[row, column] = 1;
                        //Made a move turn completed
                        turnComplete = 1;
                    }
                    else
                    {
                        Console.WriteLine("Not Available");
                        //If required cell not available take input again
                        Console.WriteLine("\n*****Choose Again*****");
                        goto again;
                    }

                    Console.WriteLine("You Took your turn\n\n");

                    //Print the board on screen
                    obj.Draw();

                    //Checks game condition for player
                    obj.winCondition();
                    obj.drawCondition();
                }



                //After making our move we check if the game has
                //ended or not
                if (obj.gameResult == 0)
                {
                    //*****************TEST**********************
                    if (string.Equals(choosePlayer, testHuman))
                    {
                        Console.WriteLine("Its your turn Player2!!!");

                        //Storing an integer number entered by user
                        //subtracting 1 from it as array index starts from 0
                        //but a layman dont know this
                        Console.Write("Row: ");
                        int row = Convert.ToInt32(Console.ReadLine()) - 1;

                        //Same for the Column
                        Console.Write("Column: ");
                        int column = Convert.ToInt32(Console.ReadLine()) - 1;

                        //if the entered cell is available
                        if (obj.gridState[row, column] == 0)
                        {
                            //write 1 there as enum at 1 index is
                            // X, so human takes X.
                            obj.gridState[row, column] = 2;
                            //Made a move turn completed
                            turnComplete = 1;
                        }
                        else
                        {
                            //Keep taking input until found valid one
                            Console.WriteLine("Not Available");
                            Console.WriteLine("\n*****Choose Again*****");
                            goto again;
                        }

                        Console.WriteLine("You Took your turn\n\n");

                        //Print the board on screen
                        obj.Draw();

                        //Checks game condition for player
                        obj.winCondition();
                        obj.drawCondition();
                    }

                    else if (string.Equals(choosePlayer, testAI))
                    {
                        Console.WriteLine("AI's turn...");

                        if (string.Equals(chooseAI, weakAI))
                        {
                        againAI:
                            //creating random number less than 3
                            //0 - 2
                            int row_ai = rand.Next(3);
                            int column_ai = rand.Next(3);

                            //if the random row and column are available make move
                            if (obj.gridState[row_ai, column_ai] == 0)
                            {
                                obj.gridState[row_ai, column_ai] = 2;
                                turnComplete = 1;
                            }
                            //if not available again generate random row and
                            //column until computer make a move
                            else
                            {
                                goto againAI;
                            }
                        }
                        else if (string.Equals(chooseAI, strongAI))
                        {
                            obj.aiOpponent(obj.gridState);
                        }


                        obj.Draw();

                        //check if after making move
                        //ai won or not
                        obj.winCondition();
                        obj.drawCondition();

                    }

                }

                //After both players made move check
                //who won by checking value of gameResult variable
                if (obj.gameResult == 1)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Final Board");
                    Console.WriteLine("_____________");
                    obj.Draw();

                    if (string.Equals(choosePlayer, testHuman))
                    {
                        Console.WriteLine("Player 1 Wins!");
                    }
                    else
                    {
                        Console.WriteLine("Human Wins!");
                    }
                }
                //Player 2
                else if (obj.gameResult == 2)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Final Board");
                    Console.WriteLine("_____________");
                    obj.Draw();

                    if (string.Equals(choosePlayer, testHuman))
                    {
                        Console.WriteLine("Player 2 Wins!");
                    }
                    Console.WriteLine("Computer Wins!");
                }
                else if (obj.gameResult == 3)
                {
                    Console.WriteLine("Draw!");
                }
                else if (obj.gameResult == 0)
                {
                    Console.WriteLine("Game is still going on!!");
                }
                else
                {
                    Console.WriteLine("BLUE SCREEN ERROR... not really, just restart the console");
                }
            }

            Console.ReadKey();
        }

        //Checking who won
        public int winCondition()
        {
            //Win Condition for human
            if (gridState[0, 0] == 1 && gridState[1, 1] == 1 && gridState[2, 2] == 1)
            {
                gameResult = 1;
            }

            if (gridState[0, 2] == 1 && gridState[1, 1] == 1 && gridState[2, 0] == 1)
            {
                gameResult = 1;
            }

            for (int i = 0; i < 3; i++)
            {

                // checking the rows
                if (gridState[i, 0] == 1 && gridState[i, 1] == 1 && gridState[i, 2] == 1)
                {
                    gameResult = 1;
                }

                // checking the columns
                if (gridState[0, i] == 1 && gridState[1, i] == 1 && gridState[2, i] == 1)
                {
                    gameResult = 1;
                }
            }


            //Win condition for AI
            if (gridState[0, 0] == 2 && gridState[1, 1] == 2 && gridState[2, 2] == 2)
            {
                gameResult = 2;
            }

            if (gridState[0, 2] == 2 && gridState[1, 1] == 2 && gridState[2, 0] == 2)
            {
                gameResult = 2;
            }

            for (int i = 0; i < 3; i++)
            {

                // checking the rows
                if (gridState[i, 0] == 2 && gridState[i, 1] == 2 && gridState[i, 2] == 2)
                {
                    gameResult = 2;
                }

                // checking the columns
                if (gridState[0, i] == 2 && gridState[1, i] == 2 && gridState[2, i] == 2)
                {
                    gameResult = 2;
                }
            }

            return 0;
        }


        int drawCondition()
        {
            int emptyspaces = new int();
            //Find empty spaces on the board
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gridState[i, j] == 0)
                    {
                        emptyspaces += 1;
                    }
                }
            }
            //If the whole board is full and
            //there is no empty space it means nobody won
            //so its a draw
            if (emptyspaces == 0)
            {
                gameResult = 3;
            }
            return emptyspaces;
        }

        //Traverse the 3x3 array
        //and print the desired symbols
        //|1 0 2|   |X _ O|
        //|2 1 2| = |O X O|
        //|0 2 1|   |_ O X|
        void Draw()
        {
            for (int i = 0; i < gridState.GetLength(0); i++)
            {
                for (int j = 0; j < gridState.GetLength(1); j++)
                {
                    //cast the enum on value of desired cell
                    players grid = (players)gridState[i, j];
                    Console.Write("{0} ", grid);
                }
                Console.Write("\n\n");
            }
        }
        //Assign value to the current board
        public int evaluationFunction(int[,] g)
        {

            //Win Condition for human
            //Diagnols
            if (g[0, 0] == 1 && g[1, 1] == 1 && g[2, 2] == 1)
            {
                return -1;
            }

            if (g[0, 2] == 1 && g[1, 1] == 1 && g[2, 0] == 1)
            {
                return -1;
            }

            for (int i = 0; i < 3; i++)
            {

                // checking the rows
                if (g[i, 0] == 1 && g[i, 1] == 1 && g[i, 2] == 1)
                {
                    return -1;
                }

                // checking the columns
                if (g[0, i] == 1 && g[1, i] == 1 && g[2, i] == 1)
                {
                    return -1;
                }
            }

            // For AI
            //diagnols
            if (g[0, 0] == 2 && g[1, 1] == 2 && g[2, 2] == 2)
            {
                return 1;
            }

            if (g[0, 2] == 2 && g[1, 1] == 2 && g[2, 0] == 2)
            {
                return 1;
            }

            for (int i = 0; i < 3; i++)
            {

                // checking the rows
                if (g[i, 0] == 2 && g[i, 1] == 2 && g[i, 2] == 2)
                {
                    return 1;
                }

                // checking the columns
                if (g[0, i] == 2 && g[1, i] == 2 && g[2, i] == 2)
                {
                    return 1;
                }
            }

            return 0;
        }


        //checks for empty grid-spaces
        public Boolean isTurns(int[,] g)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (g[i, j] == 0)
                        return true;
            return false;
        }

        //minimax method with alpha beta pruning
        public int minimax(int[,] g, int depth, int alpha, int beta, Boolean isMax)
        {
            //Stores the score as per current state of board
            int currVal = evaluationFunction(g);


            //if score is not zero | either +ve or -ve
            if (currVal != 0)
            {
                return currVal;
            }

            //if we have turns left
            if (!isTurns(g))
                return 0;

            //Maximizing Player Turn
            if (isMax)
            {
                int best = -1000;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {

                        if (g[i, j] == 0)
                        {
                            //setting grid-space
                            g[i, j] = 2;


                            best = Math.Max(best, minimax(g, depth + 1, alpha, beta, false));
                            alpha = Math.Max(alpha, best);
                            //undoing move
                            g[i, j] = 0;

                            if (beta <= alpha)
                                break;

                        }
                    }
                }
                return best;
            }



            //Minimizing Player Turn
            else
            {
                int best = 1000;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {

                        if (g[i, j] == 0)
                        {

                            g[i, j] = 1;

                            best = Math.Min(best, minimax(g, depth + 1, alpha, beta, true));
                            beta = Math.Min(best, beta);

                            g[i, j] = 0;

                            if (beta <= alpha)
                                break;

                        }
                    }
                }
                return best;
            }
        }

        //Make move for AI
        public void aiOpponent(int[,] g)
        {
            int best = -1000, m = -1, n = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    if (g[i, j] == 0)
                    {

                        g[i, j] = 2;

                        int Currmove = minimax(g, 0, -1000, 1000, false);

                        g[i, j] = 0;

                        //Store best row and column index
                        if (Currmove > best)
                        {
                            m = i;
                            n = j;
                            best = Currmove;
                        }
                    }
                }//Traverse the whole available board
            }
            g[m, n] = 2; //After Traversing, move to the best cell we got
        }
    }
}