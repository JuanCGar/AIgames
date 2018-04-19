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

        public bool error { get; set; } = false;
        

        public int currentTurn { get; private set; }
        public int maxTurns { get; private set; }
        public bool finished { get; private set; }
        public bool errored { get; private set; }
        public string errorText { get; private set; }
        private string serverURL;
        public Tile[][] board;
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
                Uri += "api/arena";
            
                // Crea un request
                WebRequest request = WebRequest.Create(Uri);
                //seleccion del post
                request.Method = "POST";
                // se convierte a Bytes
                string postData = "key=";
                postData += key;
               if (mode == "training" && turns > 0)
                    postData += "&turns=" + turns;
                //if (map == null)
                  //  postData += "&map=" + map;
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

            Console.WriteLine(gameResponse.game.heroes[0].name);

            playUrl = gameResponse.playUrl;
            viewURL = gameResponse.viewUrl;

            myHero = gameResponse.hero;
            heroes = gameResponse.game.heroes;

            errored = gameResponse.hero.crash;
            currentTurn = gameResponse.game.turns;
            maxTurns = gameResponse.game.maxTurns;
            finished = gameResponse.game.finished;
            //createBoard(gameResponse.game.board.size, gameResponse.game.board.tiles);
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
                        if (data[cont] == '1')
                        {
                            arrray[i, j] = 'A';
                        }
                        else if (data[cont] == '2')
                        {
                            arrray[i, j] = 'B';
                        }
                        else if (data[cont] == '3')
                        {
                            arrray[i, j] = 'C';
                        }
                        else if (data[cont] == '4')
                        {
                            arrray[i, j] = 'D';
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
        private void createBoard(int size, string data)
        {
            
            //check to see if the board list is already created, if it is, we just overwrite its values
            if (board == null || board.Length != size)
            {
                board = new Tile[size][];

                //need to initialize the lists within the list
                for (int i = 0; i < size; i++)
                {
                    board[i] = new Tile[size];
                }
            }

            //convert the string to the List<List<Tile>>
            int x = 0;
            int y = 0;
            char[] charData = data.ToCharArray();

            for (int i = 0; i < charData.Length; i += 2)
            {
                
                switch (charData[i])
                {
                    case '#':
                        board[x][y] = Tile.IMPASSABLE_WOOD;
                        break;
                    case ' ':
                        board[x][y] = Tile.FREE;
                        break;
                    case '@':
                        switch (charData[i + 1])
                        {
                            case '1':
                                board[x][y] = Tile.HERO_1;
                                break;
                            case '2':
                                board[x][y] = Tile.HERO_2;
                                break;
                            case '3':
                                board[x][y] = Tile.HERO_3;
                                break;
                            case '4':
                                board[x][y] = Tile.HERO_4;
                                break;

                        }
                        break;
                    case '[':
                        board[x][y] = Tile.TAVERN;
                        break;
                    case '$':
                        switch (charData[i + 1])
                        {
                            case '-':
                                board[x][y] = Tile.GOLD_MINE_NEUTRAL;
                                break;
                            case '1':
                                board[x][y] = Tile.GOLD_MINE_1;
                                break;
                            case '2':
                                board[x][y] = Tile.GOLD_MINE_2;
                                break;
                            case '3':
                                board[x][y] = Tile.GOLD_MINE_3;
                                break;
                            case '4':
                                board[x][y] = Tile.GOLD_MINE_4;
                                break;
                        }
                        break;
                }

                //time to increment x and y
                x++;
                if (x == size)
                {
                    x = 0;
                    y++;
                }
            }


        }
    }
}

