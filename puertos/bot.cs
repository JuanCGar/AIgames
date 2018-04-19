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
                int xo = server.myHero.pos.y +1;
                int yo = server.myHero.pos.x +1;

                Console.WriteLine("x: " + xo + " y: " + yo);

                int xi = server.myHero.spawnPos.x;
                int yi = server.myHero.spawnPos.y;

                
                if (server.Board[xo+1, yo] == '$')
                    server.moveHero(Direction.West);
                else if (server.Board[xo -1, yo] == '$' && xo > 0)
                    server.moveHero(Direction.East);
                else if (server.Board[xo , yo+1] == '$')
                    server.moveHero(Direction.South);
                else if (server.Board[xo , yo-1] == '$' && yo > 0)
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

