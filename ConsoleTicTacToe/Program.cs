using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("   Welcome to  TIC - TAC - TOE\n    EXTREME CRAZY EDITION!!!\n    (no diagonals...)\n\n");
            Console.WriteLine("Please choose either 'x' or 'o' by pressing that key!");

            int[] playerMove = new int[2];
            char playerKey = Console.ReadKey(true).KeyChar;
            string coin = "";
            char player = 'n';
            char computer = 'n';
            int difficulty = 0;
            bool switchControl = false;
            int winLoseDraw = 0;
            char[,] board = new char[3, 3]
                {
                    {'1','2','3'},
                    {'4','5','6'},
                    {'7','8','9'}
                };





            while (playerKey != 'x' && playerKey != 'o')
            {

                    Console.WriteLine(playerKey + " - was selected. Please simply press 'x' or 'o'!");
                    playerKey = Console.ReadKey(true).KeyChar;

            }
            

            Console.WriteLine("\n\n>>>>>     " + playerKey + "   has been selected! Press enter to continue!    <<<<<");
            Console.ReadLine();
            Console.Clear();

            if (playerKey == 'x')
            {
                player = 'X';
                computer = 'O';
            }
            else
            {
                player = 'O';
                computer = 'X';
            }

            while (switchControl == false)
            {

                //This switch will set the difficulty of the game

                Console.WriteLine("Please enter a number 0-9 to select difficulty!\n   (0 = very easy, 9 = very hard)\n\n");

                playerKey = Console.ReadKey(true).KeyChar;

                switch (playerKey)
                {
                    case '0':
                        difficulty = 1;
                        switchControl = true;
                        break;
                    case '1':
                        difficulty = 2;
                        switchControl = true;
                        break;
                    case '2':
                        difficulty = 3;
                        switchControl = true;
                        break;
                    case '3':
                        difficulty = 4;
                        switchControl = true;
                        break;
                    case '4':
                        difficulty = 5;
                        switchControl = true;
                        break;
                    case '5':
                        difficulty = 6;
                        switchControl = true;
                        break;
                    case '6':
                        difficulty = 7;
                        switchControl = true;
                        break;
                    case '7':
                        difficulty = 8;
                        switchControl = true;
                        break;
                    case '8':
                        difficulty = 9;
                        switchControl = true;
                        break;
                    case '9':
                        difficulty = 10;
                        switchControl = true;
                        break;
                    default:
                        break;
                }

                if (switchControl == true)
                {
                    Console.WriteLine("\n\n>>>>>   Difficulty has been set to: {0}", (difficulty - 1) + "   <<<<<");
                    Console.WriteLine("\n\nPress enter to continue...");
                    Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                }
            }

            Console.Clear();

            Console.WriteLine("Press enter to flip a coin!\n\nHEADS: you go first.\nTAILS: the computer goes first.\n\n\n");
            Console.ReadLine();

            Random randomNumber = new Random();
            int coinFlip = randomNumber.Next(1,3);


            if (coinFlip == 1)
            {
                Console.WriteLine("HEADS!! You go first!!");
                coin = "first";
            }
            else
            {
                Console.WriteLine("TAILS!! You go second!!");
                coin = "second";
            }

            Console.WriteLine("\n\n\n  You will be playing as: {0}\n  You will be going: {1}" +
                                  "\n  Difficulty is set to: {2}", player, coin, playerKey);
            Console.WriteLine("\n\nPRESS ENTER TO BEGIN!!!");
            Console.ReadLine();

            Console.Clear();

            while(true)
            {
                //this is where the game actually lives---------------

                //DRAW the board state to screen
                GameBoard(board);


                //COMPUTER moves first unless player won coin flip
                int[] computerMove = new int[2];

                if (coin == "second")
                {
                    computerMove = ComputerDecision(board, difficulty, player, computer);
                    board[computerMove[0], computerMove[1]] = computer;
                     
                }
                else
                {
                    coin = "second"; //computer will get turns now (if it was skipped due to coin flip)
                }

                //DRAW the board to the screen
                GameBoard(board);

                //CHECK for game over
                winLoseDraw = GameOver(board, player, computer);
                if (winLoseDraw > 0)
                {
                    break;
                }

                //PLAYER moves
                playerMove = PlayerDecision(board);
                board[playerMove[0], playerMove[1]] = player;

                //DRAW the board to screen
                GameBoard(board);

                //check for game over
                winLoseDraw = GameOver(board, player, computer);
                if (winLoseDraw > 0)
                {
                    break;
                }

                Console.WriteLine("\n\n\nSpot marked!\nComputer is" +
                    "" +
                    " thinking, press enter to see move...");
                Console.ReadLine();
            }

            switch (winLoseDraw)
            {
                case 1:
                    Console.WriteLine("\n\nYOU WIN!!!!!!!!!");
                    Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("\n\nYOU LOSE!!!!!!!!");
                    Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("\n\nDRAW!!!!!!");
                    Console.ReadLine();
                    break;
                default:
                    break;
            }

            Console.ReadLine();


            //----------------------------
        }








        
        static void GameBoard(char[,] board)
        {
            //this will display the current state of the game    <------------------------------------

            int returnCounter = 0;

            Console.Clear();

            foreach (char spot in board)
            {
                returnCounter++;

                if (returnCounter == 4)
                {
                    Console.WriteLine("\n");
                    returnCounter = 1;
                }

                Console.Write("[{0}]  ", spot);
            }

        }







        static int[] ComputerDecision(char[,] board, int difficulty, char player, char computer)
        {
            //this will decide the computer's move                <----------------------------------------------------------------------------------------

            Random randomNumber = new Random();
            int computerRoll = randomNumber.Next(1,11); //rolls to see if computer makes a smart move or random


            // This section looks for computer wins and then player marks to try and block them...

            int[] computerMove = new int[] { 0, 0 }; //where the computer is going to move -- needed after evaluation

            int[] saver = new int[] { 0, 0 }; //saves open slot, becomes computerMove if it is needed -- needed during evaluation

            bool hasAMove = false; //if the computer has a logical move to block -- needed after evaluation

            int scanner = 0; //counter for scanning for player marks to block -- used for evaluation






            //scan for horizontal computer matches to try and win...
            if (hasAMove == false)
            {
                for (int r = 0; r <= 2; r++) //move through each row one group at a time
                {
                    scanner = 0; //reset for each group

                    for (int c = 0; c <= 2; c++) //in the row, look at each character in turn
                    {
                        char checker = board[r, c]; //save the character here

                        if (checker == computer) //check if it is a computer character
                        {
                            scanner += 2; //if this hits 2, we need to place a mark to win
                        }

                        if (checker != player && checker != computer) //this saves an empty spot if we need it
                        {
                            saver[0] = r;
                            saver[1] = c;
                            scanner++;
                        }
                        if (scanner == 5) //time to win, we will use the empty spot we saved as our move
                        {
                            hasAMove = true;
                            computerMove[0] = saver[0];
                            computerMove[1] = saver[1];

                        }


                    }
                }
            }

            //if no horizontal win, scan for vertical computer matches to try and win...
            if (hasAMove == false)
            {
                for (int c = 0; c <= 2; c++) //move through each vertical group one by one
                {
                    scanner = 0; //reset for each group

                    for (int r = 0; r <= 2; r++) //in each column work down looking for computer characters
                    {
                        char checker = board[r, c]; //save the character there

                        if (checker == computer) //check if it is a computer character
                        {
                            scanner += 2; //if this hits 2, we need to place a mark to win
                        }

                        if (checker != player && checker != computer) //saves an empty spot
                        {
                            saver[0] = r;
                            saver[1] = c;
                            scanner++;
                        }
                        if (scanner == 5) //we can win by placing our mark in the empty spot
                        {
                            hasAMove = true;
                            computerMove[0] = saver[0];
                            computerMove[1] = saver[1];

                        }
                    }
                }
            }


            //if no wins, scan for horizontal player matches to block
            if (hasAMove == false)
            {
                for (int r = 0; r <= 2; r++) //move through each row one group at a time
                {
                    scanner = 0; //reset for each group

                    for (int c = 0; c <= 2; c++) //in the row, look at each character in turn
                    {
                        char checker = board[r, c]; //save the character here

                        if (checker == player) //check if it is a player character
                        {
                            scanner += 2; //if this hits 2, we need to place a mark to stop the player from winning
                        }

                        if (checker != player && checker != computer) //this saves an empty spot if we need it
                        {
                            saver[0] = r;
                            saver[1] = c;
                            scanner++;
                        }
                        if (scanner == 5) //we need to block, we will use the empty spot we saved as our move
                        {
                            hasAMove = true;
                            computerMove[0] = saver[0];
                            computerMove[1] = saver[1];

                        }


                    }
                }
            }

            //if no horizontal matches to block, scan for vertical player matches to block
            if (hasAMove == false)
            {
                for (int c = 0; c <= 2; c++) //move through each vertical group one by one
                {
                    scanner = 0; //reset for each group

                    for (int r = 0; r <= 2; r++) //in each column work down looking for player characters
                    {
                        char checker = board[r, c]; //save the character there

                        if (checker == player) //check if it is a player character
                        {
                            scanner += 2; //if this hits 2, we need to place a mark to stop the player from winning
                        }

                        if (checker != player && checker != computer) //saves an empty spot
                        {
                            saver[0] = r;
                            saver[1] = c;
                            scanner++;
                        }
                        if (scanner == 5) //we need to block
                        {
                            hasAMove = true;
                            computerMove[0] = saver[0];
                            computerMove[1] = saver[1];

                        }
                    }
                }
            }

            /*
            //find the best non blocking move (no blocks, no way to win yet) commented until diagonals are in
            if (hasAMove == false)
            {
                if (board[1, 1] != player && board[1, 1] != computer) //check and grab middle first
                {
                    computerMove[0] = 1;
                    computerMove[1] = 1;
                }
                else if (board[0, 0] != player && board[0, 0] != computer) //check and grab top left
                {
                    computerMove[0] = 0;
                    computerMove[1] = 0;
                }
                else if (board[2, 2] != player && board[2, 2] != computer) //check and grab bottom right
                {
                    computerMove[0] = 2;
                    computerMove[1] = 2;
                }
                else if (board[0, 2] != player && board[0, 2] != computer) //check and grab top right
                {
                    computerMove[0] = 0;
                    computerMove[1] = 2;
                }
                else if (board[2, 0] != player && board[2, 0] != computer) //check and bottom left
                {
                    computerMove[0] = 2;
                    computerMove[1] = 0;
                }
            }
            */


            //take a random move instead of a 'smart' move (based on difficulty)
            if (computerRoll > difficulty || hasAMove == false)
            {

                while (true)
                {
                    Random myRandom = new Random();    //grab a random x/y coord
                    int rowCheck = myRandom.Next(0, 3);
                    int colCheck = myRandom.Next(0, 3);

                    if (board[rowCheck, colCheck] != player && board[rowCheck, colCheck] != computer) //see if it is available
                    {
                        computerMove[0] = rowCheck;
                        computerMove[1] = colCheck; //if it's available it will be our move, repeat untill it works
                        break;
                    }
                }
            }
            return computerMove;
        }






        static int[] PlayerDecision(char[,] board)
        {
            //this will grab the player's move      <----------------------------------

            char playerNumber;
            int[] playerchoice = new int[2];

            while (true)
            {
                Console.WriteLine("\n\n\nPlease enter the number of the space you would like to place your mark: ");

                playerNumber = Console.ReadKey(true).KeyChar;

                Console.WriteLine("The key pressed was: {0}", playerNumber);

                if (Char.IsDigit(playerNumber) == false || playerNumber == '0')
                {
                    Console.WriteLine("Please select a number from the grid!");
                    continue;
                }

                switch (playerNumber)
                {
                    case '1':
                        playerchoice[0] = 0;
                        playerchoice[1] = 0;
                        break;
                    case '2':
                        playerchoice[0] = 0;
                        playerchoice[1] = 1;
                        break;
                    case '3':
                        playerchoice[0] = 0;
                        playerchoice[1] = 2;
                        break;
                    case '4':
                        playerchoice[0] = 1;
                        playerchoice[1] = 0;
                        break;
                    case '5':
                        playerchoice[0] = 1;
                        playerchoice[1] = 1;
                        break;
                    case '6':
                        playerchoice[0] = 1;
                        playerchoice[1] = 2;
                        break;
                    case '7':
                        playerchoice[0] = 2;
                        playerchoice[1] = 0;
                        break;
                    case '8':
                        playerchoice[0] = 2;
                        playerchoice[1] = 1;
                        break;
                    case '9':
                        playerchoice[0] = 2;
                        playerchoice[1] = 2;
                        break;
                    default:
                        break;
                }

                if (board[playerchoice[0], playerchoice[1]] == 'X')
                {
                    Console.WriteLine("There is already an X here!");
                    continue;
                }
                else if (board[playerchoice[0], playerchoice[1]] == 'O')
                {
                    Console.WriteLine("There is already an O here!");
                    continue;
                }
                else
                {
                    break;
                }
            }
            return playerchoice;
        }





        static int GameOver(char[,] board,char player,char computer)
        {
            //this will display the appropriate game over screen               <--------------------

            int playerPoints = 0;
            int computerPoints = 0;
            int winner = 0; // 0=no winner yet, 1=player, 2=computer, 3=draw
            int counter = 0;
            bool gameOver = false;

            //look for horizontal matches
            for (int r = 0; r <= 2; r++) //move through each row one group at a time
            {
                playerPoints = 0; //reset for each group
                computerPoints = 0; //reset after each group

                for (int c = 0; c <= 2; c++) //in the row, look at each character in turn
                {
                    char checker = board[r, c]; //save the character here

                    if (checker == player) //check if it is a player character
                    {
                        playerPoints++; //if this hits 2, we need to place a mark to stop the player from winning
                    }
                    else if (checker == computer) //check if it is a computer character
                    {
                        computerPoints++;
                    }

                    if (playerPoints == 3) //we need to block, we will use the empty spot we saved as our move
                    {
                        winner = 1;
                        gameOver = true;
                    }
                    else if (computerPoints == 3)
                    {
                        winner = 2;
                        gameOver = true;
                    }
                }
            }

            //scan for vertical matches
            for (int c = 0; c <= 2; c++) //move through each vertical group one by one
            {
                playerPoints = 0; //reset for each group
                computerPoints = 0; //reset after each group

                for (int r = 0; r <= 2; r++) //in each column work down looking for player characters
                {
                    char checker = board[r, c]; //save the character there

                    if (checker == player) //check if it is a player character
                    {
                        playerPoints++; //if this hits 2, we need to place a mark to stop the player from winning
                    }
                    else if(checker == computer)
                    {
                        computerPoints++;
                    }


                    if (playerPoints == 3)
                    {
                        winner = 1;
                        gameOver = true;
                    }
                    else if (computerPoints == 3)
                    {
                        winner = 2;
                        gameOver = true;
                    }
                }
            }

            if (gameOver == false)

            {
                foreach (char mark in board) //checks for  draw          <----------------
                {
                    if (mark == 'X' || mark == 'O') //loops through board looking for player and computer marks
                    {
                        counter++;
                    }

                    if (counter == 9) //if all 9 spaces are marked, it's a draw
                    {
                        winner = 3;
                    }
                }
            }

            return winner;
        }
        

    }
}
