using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization;
using System.Net.Http;
using System.Web.Script;
using Newtonsoft.Json;
using Json.NET.Web;

namespace puertos
{
    class Server
    {
        private string key;
        private string mode;
        private uint turns;
        private string map;

        private string playUrl;
        public string viewURL { get; private set; }

        public hero myHero { get; set; }
        public List<hero> heroes;
        public Drink[] drink {get;set;}
        public Mine[] mine { get; set; }
        public bool error { get; set; } = false;
        

        public int currentTurn { get; private set; }
        public int maxTurns { get; private set; }
        public bool finished { get; private set; }
        public bool errored { get; private set; }
        public string errorText { get; private set; }
        private string serverURL;
        public char[,] Board { get;set; }

        public Server(string url, string key, string mode = "training", uint turns = 0, string map = null)
        {
            serverURL = url;
            this.key = key;
            this.mode = mode;
            if (mode == "training")
            {
                this.map = map;
                this.turns = turns;
            }


        }


        public void newGame()
        {

            string Uri = serverURL;
            if (mode == "training")
                Uri += "/api/training";
            else
                Uri += "/api/arena";
            
                // Crea un request
                WebRequest request = WebRequest.Create(Uri);
                //seleccion del post
                request.Method = "POST";
                // se convierte a Bytes
                string postData = "key=";
                postData += key;
               if (mode == "training" && turns > 0)
                    postData += "&turns=" + turns;
                if (map != null)
                    postData += "&map=" + map;
                Console.WriteLine(postData);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // cONTENTTYPE
                request.ContentType = "application/x-www-form-urlencoded";
                //Content lenght
                request.ContentLength = byteArray.Length;
                // Se pide el stream,
                Stream dataStream = request.GetRequestStream();
                // Se escribe lo que se va amndar como request
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Se cierra stream
                dataStream.Close();

                // Recibe la respuesta
                WebResponse response = request.GetResponse();

            // Despliega status
            string status = ((HttpWebResponse)response).StatusDescription;
            if(status != "OK" || status != "Ok")
            {
                error = true;
            }
            // Se consigue la respuesta del servidor
            dataStream = response.GetResponseStream();
            // Se abre el stream
            StreamReader reader = new StreamReader(dataStream);
            // se lee el contenido
            string Cont = reader.ReadToEnd();
            var jCont = reader.ReadToEnd();


            deserialize(Cont);
            reader.Close();
            dataStream.Close();
            response.Close();

        }

        private void deserialize(string json)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            MemoryStream stream = new MemoryStream(byteArray);

            gameResponse gameResponse = JsonConvert.DeserializeObject<gameResponse>(json);


            playUrl = gameResponse.playUrl;
            viewURL = gameResponse.viewUrl;

            myHero = gameResponse.hero;
            heroes = gameResponse.game.heroes;

            errored = gameResponse.hero.crash;
            currentTurn = gameResponse.game.turns;
            maxTurns = gameResponse.game.maxTurns;
            finished = gameResponse.game.finished;
            CrearTablero(gameResponse.game.board.size, gameResponse.game.board.tiles, Board);



        }

        public void moveHero(string direction)
        {
            WebRequest request = WebRequest.Create(playUrl);
            string parameters = "key=" + key + "&dir=" + direction;
            Console.WriteLine(parameters);
            byte[] byteArray = Encoding.UTF8.GetBytes(parameters);
            request.Method = "POST";
            // cONTENTTYPE
            request.ContentType = "application/x-www-form-urlencoded";
            //Content lenght
            request.ContentLength = byteArray.Length;
            // Se pide el stream,
            Stream dataStream = request.GetRequestStream();
            // Se escribe lo que se va amndar como request
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Se cierra stream
            dataStream.Close();
           
            WebResponse response = request.GetResponse();
            // Despliega status    
            // Se consigue la respuesta del servidor
            dataStream = response.GetResponseStream();
            // Se abre el stream
            StreamReader reader = new StreamReader(dataStream);
            // se lee el contenido
            string Cont = reader.ReadToEnd();
            var jCont = reader.ReadToEnd();

            deserialize(Cont);
            reader.Close();
            dataStream.Close();
            response.Close();

        }

        public void SearchPos(char[,] board, int tam, Mine[] mine, Drink[] drink)
        {
            int contMine = 0;
            int contDrink = 0;
            for(int i = 0; i < tam; i++)
            {
                for(int j = 0; j < tam; j++)
                {
                    if (board[i, j] == '$')
                    {
                        mine[contMine].x = i;
                        mine[contMine].y = j;
                        contMine++;

                    }
                    else if(board[i, j] == '[')
                    {
                        mine[contDrink].x = i;
                        drink[contDrink].y = j;
                        contDrink++;
                    }
                }
            }
            
        }

        private void CrearTablero(int size,string data, char[,] board)
        {
            
            int cont  = 0;
            char[,] arrray = new char[size, size];

            for (int i = 0; i < size; i++) 
            {
                for(int j = 0; j < size; j++)
                {
                    cont++;
                   if(data[cont-1] == '@')
                    {
                       if (data[cont] == '1')
                        {
                            arrray[i, j] = '1';
                        }
                        else if (data[cont] == '2')
                        {
                            arrray[i, j] = '2';
                        }
                        else if (data[cont] == '3')
                        {
                            arrray[i, j] = '3';
                        }
                        else if (data[cont] == '4')
                        {
                            arrray[i, j] = '4';
                        }

                    }
                   
                    else if (data[cont] == '#')
                    {
                        arrray[i, j] = '#';
                    }
                    else if (data[cont-1] == '$')
                    {
                        
                         if(data[cont] == '1' && myHero.id != 1)
                        {
                            arrray[i, j] = 'A';
                        }
                        else if (data[cont] == '2' && myHero.id != 2)
                        {
                            arrray[i, j] = 'B';
                        }
                        
                        else if (data[cont] == '3' && myHero.id != 3)
                        {
                            arrray[i, j] = 'C';
                        }
                        else if (data[cont] == '4' && myHero.id != 4)
                        {
                            arrray[i, j] = 'D';
                        }
                        else if (data[cont] == '1' && myHero.id == 1)
                        {
                            arrray[i, j] = '#';
                        }
                        else if (data[cont] == '2' && myHero.id == 2)
                        {
                            arrray[i, j] = '#';
                        }
                        else if (data[cont] == '3' && myHero.id == 3)
                        {
                            arrray[i, j] = '#';
                        }
                        else if (data[cont] == '4' && myHero.id == 4)
                        {
                            arrray[i, j] = '#';
                        }
                        else
                            arrray[i, j] = '$';
                    }
                    else if (data[cont-1] == '[')
                    {
                        arrray[i, j] = 'Q';
                        
                    }
                    
                    else
                        arrray[i, j] = ' ';
                    cont++;
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(arrray[i, j]);
                }
                Console.WriteLine();
            }
            Board = arrray;
            
            

        }

       
    }
}

