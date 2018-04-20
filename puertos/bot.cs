using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace puertos
{
    class bot
    {

        private Server server;

        public bot(Server server)
        {
            this.server = server;
            server.headingNumber = 1;
        }

        //the running system

        //EN ESTAS FUNCIONES HAY QUE CHECAR QUE SE ESTEN REVISANDO LAS CASILLAS CORRECTAS
        //SI VAS A MOVER ALGO, PRIMERO REVISA QUE ESTAS FUNCIONES ESTEN BIEN, ES POR ESTO QUE AHORITA FALLA A VECES EL CODIGO Y SE CRASHEA EL SERVER
        public bool frontIsClear(char heading)
        {
            if(heading == 'u')
            return((server.myHero.pos.y != 0) && server.Board[server.myHero.pos.x, server.myHero.pos.y - 1] != '#');
            else if(heading == 'd')
                return ((server.myHero.pos.y != 0) && server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] != '#');
            else if(heading == 'r')
                return ((server.myHero.pos.y != 0) && server.Board[server.myHero.pos.x+1, server.myHero.pos.y] != '#');
            else if(heading == 'l')
                return ((server.myHero.pos.y != 0) && server.Board[server.myHero.pos.x-1, server.myHero.pos.y ] != '#');
            else
                return false;
            
        }

        public bool frontIsBlocked(char heading)
        {
            if (heading == 'u')
                return ((server.myHero.pos.y == 0) || server.Board[server.myHero.pos.x, server.myHero.pos.y - 1] == '#');
            else if (heading == 'd')
                return ((server.myHero.pos.y == 0) || server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] == '#');
            else if (heading == 'r')
                return ((server.myHero.pos.y == 0) || server.Board[server.myHero.pos.x + 1, server.myHero.pos.y] == '#');
            else if (heading == 'l')
                return ((server.myHero.pos.y == 0) || server.Board[server.myHero.pos.x - 1, server.myHero.pos.y] == '#');
            else
                return false;
        }

        public bool rightIsClear(char heading)
        {
            if (heading == 'u')
                return ((server.myHero.pos.x != server.Board.Length) && server.Board[server.myHero.pos.x + 1, server.myHero.pos.y] != '#');
            else if (heading == 'd')
                return ((server.myHero.pos.x != server.Board.Length) && server.Board[server.myHero.pos.x - 1, server.myHero.pos.y] != '#');
            else if (heading == 'r')
                return ((server.myHero.pos.x != server.Board.Length) && server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] != '#');
            else if (heading == 'l')
                return ((server.myHero.pos.x != server.Board.Length) && server.Board[server.myHero.pos.x, server.myHero.pos.y - 1] != '#');
            else
                return false;
        }

        public bool rightIsBlocked(char heading)
        {
            if (heading == 'u')
                return ((server.myHero.pos.x == server.Board.Length) || server.Board[server.myHero.pos.x + 1, server.myHero.pos.y] == '#');
            else if (heading == 'd')
                return ((server.myHero.pos.x == server.Board.Length) || server.Board[server.myHero.pos.x - 1, server.myHero.pos.y] == '#');
            else if (heading == 'r')
                return ((server.myHero.pos.x == server.Board.Length) || server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] == '#');
            else if (heading == 'l')
                return ((server.myHero.pos.x == server.Board.Length) || server.Board[server.myHero.pos.x, server.myHero.pos.y - 1] == '#');
            else
                return false;
        }

        public bool leftIsClear(char heading)
        {
            if (heading == 'u')
                return ((server.myHero.pos.x != 0) && server.Board[server.myHero.pos.x - 1, server.myHero.pos.y] != '#');
            else if (heading == 'd')
                return ((server.myHero.pos.x != 0) && server.Board[server.myHero.pos.x + 1, server.myHero.pos.y] != '#');
            else if (heading == 'r')
                return ((server.myHero.pos.x != 0) && server.Board[server.myHero.pos.x, server.myHero.pos.y - 1] != '#');
            else if (heading == 'l')
                return ((server.myHero.pos.x != 0) && server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] != '#');
            else
                return false;
        }

        public bool leftIsBlocked(char heading)
        {
            if (heading == 'u')
                return ((server.myHero.pos.x == 0) || server.Board[server.myHero.pos.x - 1, server.myHero.pos.y] == '#');
            else if (heading == 'd')
                return ((server.myHero.pos.x == 0) || server.Board[server.myHero.pos.x + 1, server.myHero.pos.y] == '#');
            else if (heading == 'r')
                return ((server.myHero.pos.x == 0) || server.Board[server.myHero.pos.x, server.myHero.pos.y - 1] == '#');
            else if (heading == 'l')
                return ((server.myHero.pos.x == 0) || server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] == '#');
            else
                return false;
        }

        public bool nextToABeer(){
            if (server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] != '[' || server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] != ']')
                return true;
            else if (server.Board[server.myHero.pos.x, server.myHero.pos.y - 1] != '[' || server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] != ']')
                return true;
            else if (server.Board[server.myHero.pos.x + 1, server.myHero.pos.y] != '[' || server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] != ']')
                return true;
            else if (server.Board[server.myHero.pos.x - 1, server.myHero.pos.y] != '[' || server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] != ']')
                return true;
            else
                return false;
        }

        public char nextToABeer(bool t)
        {
            if (t)
            {
                if (server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] != '[' || server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] != ']')
                    return 'd';
                if (server.Board[server.myHero.pos.x, server.myHero.pos.y - 1] != '[' || server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] != ']')
                    return 'u';
                if (server.Board[server.myHero.pos.x + 1, server.myHero.pos.y] != '[' || server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] != ']')
                    return 'r';
                if (server.Board[server.myHero.pos.x - 1, server.myHero.pos.y] != '[' || server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] != ']')
                    return 'l';
            }

                return 'n';
            
        }

        public bool nextToGold()
        {
            if (server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] != '$')
                return true;
            else if (server.Board[server.myHero.pos.x, server.myHero.pos.y - 1] != '$')
                return true;
            else if (server.Board[server.myHero.pos.x + 1, server.myHero.pos.y] != '$')
                return true;
            else if (server.Board[server.myHero.pos.x - 1, server.myHero.pos.y] != '$')
                return true;
            else
                return false;
        }

        public char nextToGold(bool t)
        {
            if (t)
            {
                if (server.Board[server.myHero.pos.x, server.myHero.pos.y + 1] != '$')
                    return 'u';
                else if (server.Board[server.myHero.pos.x, server.myHero.pos.y - 1] != '$')
                    return 'd';
                else if (server.Board[server.myHero.pos.x + 1, server.myHero.pos.y] != '$')
                    return 'r';
                else if (server.Board[server.myHero.pos.x - 1, server.myHero.pos.y] != '$')
                    return 'l';
            }
                return 'n';

        }

        public void calculateNextMove(int[] actualPos, int[] goalPos, int numeracionInicial){
           
            int[,] numeracion = new int[server.boardSize,server.boardSize];
            int numeracionActual = numeracionInicial;
            numeracion[goalPos[0], goalPos[1]] = numeracionInicial;
            int[] modPos = new int[2]{goalPos[0]-1, goalPos[1]-1};
            if (actualPos[0] != goalPos[0] && actualPos[1] != goalPos[1])
            {
                recursiveNextNumber(numeracion, ref numeracionActual, actualPos, goalPos, modPos);
            }else{
                for (int i = 0; i < server.recorrido.Length; i++){
                    for (int j = 0; j < server.recorrido.Length; j++){
                        server.numeracion[i, j] = numeracion[i,j];
                    }
                }
            }

        }

        public void recursiveNextNumber(int[,] numeracion, ref int numeracionActual, int[] actualPos, int[] goalPos, int[] modPos){
            numeracionActual++;
            int[] aPos = new int[2] { actualPos[0], actualPos[1] };

            if (actualPos[0] != 0 && actualPos[1] != 0 && actualPos[0] != server.boardSize - 1 && actualPos[1] != server.boardSize - 1)
            {
                if (server.Board[modPos[0] - 1, modPos[1] - 1] != '#')
                {
                    aPos[0] = modPos[0] - 1;
                    aPos[1] = modPos[1] - 1;
                    calculateNextMove(aPos, goalPos, numeracionActual);
                }

                if (server.Board[modPos[0], modPos[1] - 1] != '#')
                {
                    aPos[0] = modPos[0];
                    aPos[1] = modPos[1] - 1;
                    calculateNextMove(aPos, goalPos, numeracionActual);
                }

                if (server.Board[modPos[0] + 1, modPos[1] - 1] != '#')
                {
                    aPos[0] = modPos[0] + 1;
                    aPos[1] = modPos[1] - 1;
                    calculateNextMove(aPos, goalPos, numeracionActual);
                }

                if (server.Board[modPos[0] - 1, modPos[1]] != '#')
                {
                    aPos[0] = modPos[0] - 1;
                    aPos[1] = modPos[1];
                    calculateNextMove(aPos, goalPos, numeracionActual);
                }

                if (server.Board[modPos[0] + 1, modPos[1]] != '#')
                {
                    aPos[0] = modPos[0] + 1;
                    aPos[1] = modPos[1];
                    calculateNextMove(aPos, goalPos, numeracionActual);
                }

                if (server.Board[modPos[0] - 1, modPos[1] + 1] != '#')
                {
                    aPos[0] = modPos[0] - 1;
                    aPos[1] = modPos[1] + 1;
                    calculateNextMove(aPos, goalPos, numeracionActual);
                }

                if (server.Board[modPos[0], modPos[1] + 1] != '#')
                {
                    aPos[0] = modPos[0];
                    aPos[1] = modPos[1] + 1;
                    calculateNextMove(aPos, goalPos, numeracionActual);
                }

                if (server.Board[modPos[0] + 1, modPos[1] + 1] != '#')
                {
                    aPos[0] = modPos[0] + 1;
                    aPos[1] = modPos[1] + 1;
                    calculateNextMove(aPos, goalPos, numeracionActual);
                }
            }
            else if (/*izquierda*/actualPos[1] == 0 && actualPos[0] != 0)
            {
                 

            }
            else if (/*arriba*/actualPos[1] != 0 && actualPos[0] == 0)
            {

            }
            else if (/**/actualPos[1] == 0 && actualPos[0] == 0)
            {

            }
            else if (actualPos[1] !=0 && actualPos[0] !=0){
                
            }

        }

        public void moveTo(char heading){
            if (heading == 'u')
                server.moveHero(Direction.North);
            if (heading == 'd')
                server.moveHero(Direction.South);
            if (heading == 'r')
                server.moveHero(Direction.East);
            if (heading == 'l')
                server.moveHero(Direction.West);
            if (heading == 's')
                server.moveHero(Direction.Stay);
        }

        public void start()
        {
            Console.Out.WriteLine("Bot is running");
            server.newGame();
            if(server.error)
            {
                new Thread(delegate ()
                {
                    System.Diagnostics.Process.Start(server.viewURL);
                }).Start();
            }
            Random random = new Random();
            while (server.finished == false && server.errored == false)
            {
                //posiciones actuales de myheroe

                //int numeracionLength = server.boardSize;

                int goalX;
                int goalY;
                int[] goals = new int[2];
                int nextX;
                int nextY;
                int[] nextMove = new int[2];

                int actualX = server.myHero.pos.x;
                int actualY = server.myHero.pos.y;

                int[] actualPos = new int[2] { actualX, actualY };
                int spawnX = server.myHero.spawnPos.x;
                int spawnY = server.myHero.spawnPos.y;

                int[] spawnPos = new int[2] { spawnX, spawnY };
                int[] goal = new int[2] { server.boardSize-1, server.boardSize-1 };
                char heading = 'u';
                //Console.WriteLine("x: " + actualX + " y: " + actualY);
                //calculateNextMove(actualPos,goal,1);
                /*
                for (int i = 0; i < server.boardSize; i++){
                    for (int j = 0; j < server.boardSize; j++){
                        Console.Write(server.numeracion[i,j]);
                        Console.Write(" ");
                    }
                    Console.WriteLine("");
                }
                */

                /*

                if (server.Board[actualX+1, actualY] == '$')
                    server.moveHero(Direction.West);
                else if (server.Board[actualX -1, actualY] == '$' && actualX > 0)
                    server.moveHero(Direction.East);
                else if (server.Board[actualX , actualY+1] == '$')
                    server.moveHero(Direction.South);
                else if (server.Board[actualX , actualY-1] == '$' && actualY > 0)
                    server.moveHero(Direction.North);
                
                else
                {


                    switch (random.Next(0, 6))
                    {
                        case 0:
                            server.moveHero(Direction.East);
                            break;
                        case 1:
                            server.moveHero(Direction.North);
                            break;
                        case 2:
                            server.moveHero(Direction.South);
                            break;
                        case 3:
                            server.moveHero(Direction.Stay);
                            break;
                        case 4:
                            server.moveHero(Direction.West);
                            break;
                    }


                }
*/
                char[] headings = new char[5] { 'u', 'r', 'l', 'd', 's'};

                int hn = server.headingNumber;
                heading = headings[hn];

                    if (frontIsClear(heading))
                    {
                        moveTo(heading);
                    }

                    if (frontIsBlocked(heading))
                    {
                    server.headingNumber++;
                    heading = headings[hn % 5];//se recorre de manera circular el arreglo
                    }




                /* RANDOM BOT
               
                }*/

                Console.Out.WriteLine("completed turn " + server.currentTurn);
            }

            if (server.errored)
            {
                Console.Out.WriteLine("error: " + server.errorText);
            }

            Console.Out.WriteLine("random bot finished");
        }



    }


    }

