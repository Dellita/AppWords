using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;

namespace AppWords

{
    public class AI : Player
    {
        Random _rndABC;
        List<string> _listWord;
        List<string> _listWordsCellTemp;
        string _wordTemp;

        public AI()
        {
            this.Name = "AI";
            WordDict();
        }


        /// <summary>
        /// Ходит ИИ.
        /// </summary>
        public string SteepAI()
        {
            _rndABC = new Random();
            if (_listWordsCellTemp.Count>0)
            {
                int i = _rndABC.Next(_listWordsCellTemp.Count-1);
                var buk = _listWordsCellTemp[i].Substring(_wordTemp.Length).ToCharArray()[0];
                return buk.ToString();//_listWordsCellTemp[i].Substring(2);
            }
            // _rndABC.Next(31);
            //UTF-8 Hex-410 Dec - 1040
            return Convert.ToChar(Global.KEY_UTF8 + _rndABC.Next(31)).ToString();
        }
        
        /// <summary>
        /// Выбирает клетку для текущего хода ИИ.
        /// </summary>
        /// <param name="itemsSource"></param>
        /// <returns></returns>
        internal int RndButton(IEnumerable itemsSource)
        {
            int ind = 0;
            _rndABC = new Random();
            var itemS = itemsSource as List<List<Button>>;
            for (int i = 0; i < Global.GOR_CELL; i++)
            {
                for (int j = 0; j < Global.VERT_CELL; j++)
                {
                    ind++;
                    if (itemS[i][j].Content == null)
                    {
                     
                         CheckWordCell(itemS, i, j);
                         return ind;
                   
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Производит поиск слов для хода ИИ.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="gor"></param>
        /// <param name="ver"></param>
        private void CheckWordCell(List<List<Button>> item, int gor, int vert)
        {
            _wordTemp = "";
            //Ищет слово по строке.
            //  if (vert == Global.VERT_CELL-1)
            for (int j = 0; j < Global.GOR_CELL - 1; j++)
            {
                if (item[gor][j].Content != null)
                    _wordTemp += item[gor][j].Content.ToString();
            }
            //Ищет слово по столбцу.
            //  if (gor == Global.GOR_CELL - 1 && _wordTemp.Length == 0)
            if (_wordTemp.Length == 0)
            {
                for (int i = 0; i < Global.VERT_CELL; i++)
                {
                    if (item[i][vert].Content != null)
                        _wordTemp += item[i][vert].Content.ToString();
                }
            }
            _listWordsCellTemp = new List<string>();
            if (_listWord != null)
            {
                foreach (var lWord in _listWord)
                {
                    if (lWord.StartsWith(_wordTemp))
                        _listWordsCellTemp.Add(lWord);
                }
            }
        }


        /// <summary>
        /// Подключает словарь.
        /// </summary>
        private async void WordDict()
        {
             _listWord = await FileStorage.ReadFile(Global.TYPE_DICT);
        }



    }
}
