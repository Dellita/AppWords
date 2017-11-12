using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;

namespace AppWords
{
    public class Game
    {
        Player _player1;
        int _numberSteep;
        string _message;
        List<string> _listWord;
        ResourceLoader _resourceloader = new Windows.ApplicationModel.Resources.ResourceLoader();
        bool _running;
        int _level;

        public Game()
        {
            _running = false;
            _level = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public async void WordDict()
        {
            _listWord = await FileStorage.ReadFile(Global.TYPE_DICT);
        }

        /// <summary>
        ///   Подготавливает и запускает игру.
        /// </summary>
        public void NewGame()
        {
             WordDict();
            _player1 = new Player();
      //      ABCButtonDuagonal();
            _running = true;
            _numberSteep = 0;
            _level = 0;
            MsgPlayers();
        }


        public void  NextLevel()
        {
        
            _running = true;
            _numberSteep = 0;
            WordDict();
            MsgPlayers();
        }

        public void Level()
        {
            _level++;
        }

        public void nextSteep()
        {
            _numberSteep++;
        }

        public bool isRunning()
        {
            return _running;
        }

        public int isLevel()
        {
            return _level;
        }
        /// <summary>
        /// Добавляет слово игроку.
        /// </summary>
        /// <param name="word">Слово.</param>
        public void AddWordPlayers(string word)
        {
            if (_listWord.Contains(word))
            {
                _player1.AddWord(word);
            }
        }

        /// <summary>
        /// Подготавливает информацию о игроках.
        /// </summary>
        public string MsgPlayers()
        {
            return _player1.CountWord().ToString();
        }
       

        /// <summary>
        /// Проверяет окончание игры.
        /// </summary>
        public string CheckOverGames()
        {
            MsgPlayers();

            if (_numberSteep == Global.GOR_CELL * Global.VERT_CELL - Global.GOR_CELL)
            {
                _message = _resourceloader.GetString("gameOver");
                _running = false;
                NextLevel();
               // NewGame();
            }
            else
            {
                _message = null;
                _running = true;
            }
            return _message;
        }

       
    }
}
