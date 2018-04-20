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
        }

        //the running system

        public void calculateNextMove(int[] actualPos, int[] goalPos, int numeracionInicial){
           
            int[,] numeracion = new int[server.recorrido.Length,server.recorrido.Length];
            int numeracionActual = numeracionInicial;
            numeracion[goalPos[0], goalPos[1]] = numeracionInicial;
            int[] modPos = new int[2]{goalPos[0], goalPos[1]};
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

                int numeracionLength = server.boardSize;

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
                int[] goal = new int[2] { numeracionLength, numeracionLength };
                //Console.WriteLine("x: " + actualX + " y: " + actualY);
                calculateNextMove(actualPos,goal,1);
                for (int i = 0; i < numeracionLength; i++){
                    for (int j = 0; j < numeracionLength; j++){
                        Console.Write(server.numeracion[i,j]);
                        Console.Write(" ");
                    }
                    Console.WriteLine("");
                }
               

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

