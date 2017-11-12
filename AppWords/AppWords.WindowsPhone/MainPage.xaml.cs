using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Globalization;
using System.Collections;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;
using System.Reflection;
using Windows.Graphics.Display;
using Windows.UI.Xaml.Media.Imaging;


// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace AppWords

{
      /// <summary>
    /// Главная страница.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Button _txtButton;
        Button _btnKey;
        Game _game;
        string _message;
        Button _txtButtonDown = new Button();
        public SolidColorBrush _mainColor;
        ResourceLoader _resourceloader = new Windows.ApplicationModel.Resources.ResourceLoader();
        double _screenWidth;
        double _screenHeight;
        int _widthButton;

        public MainPage()
        {
            InitializeComponent();
           _screenWidth = Window.Current.Bounds.Width * (int)DisplayInformation.GetForCurrentView().ResolutionScale / 100;
           _screenHeight = Window.Current.Bounds.Height * (int)DisplayInformation.GetForCurrentView().ResolutionScale / 100;
            _widthButton = (int)_screenWidth / 16;
            if (_widthButton < 43)
                _widthButton = 43;

            ListOfCreateButton();
            ListOfCreateKeyABCButton();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            //    _mainColor = Global.DIALOG_COLOR;
            //       _mainColor.Opacity = 0.7;
            //      sPSetup.Background = _mainColor;

            _game = new Game();
            NewGame();
        }

      
         /// <summary>
        /// Создает список для посторения кнопок.
        /// </summary>
        private void ListOfCreateButton()
        {
            int x = 0;
           List<List<Button>> lsts = new List<List<Button>>();
            for (int i = 0; i < Global.VERT_CELL; i++)
            {
                lsts.Add(new List<Button>());
                for (int j = 0; j < Global.GOR_CELL; j++)
                {
                    x++;
                    TextButton(x);
                    lsts[i].Add(_txtButton);
                }
            }
            lst.ItemsSource = lsts;
                 
        }

        /// <summary>
        /// Заводит новую кнопку.
        /// </summary>
        /// <param name="x"></param>
        private void TextButton(int x)
        {
            _txtButton = new Button();
            _txtButton.Name = x.ToString();
            _txtButton.Click += this.TxtButton_Click;
            _txtButton.HorizontalAlignment = HorizontalAlignment.Center;
            _txtButton.VerticalAlignment = VerticalAlignment.Center;
       //     _txtButton.FontSize = (int)_widthButton/2;
            _txtButton.Margin = new Thickness(1);
            _txtButton.MinWidth =  _widthButton;
            _txtButton.MinHeight = _widthButton * 1.4;// Math.Truncate(_widthButton * _screenHeight/_screenWidth-10);
            _txtButton.MaxWidth = 120;
            _txtButton.MaxHeight = 120*1.4;
            //     txtButton.Foreground = mainColor;
            //       txtButton.BorderBrush = mainColor;
        }

        /// <summary>
        /// Формирует список клавиш для клавиатуры.
        /// </summary>
        private void ListOfCreateKeyABCButton()
        {
            int x = 0;
            List<List<Button>> lstKeyBoard = new List<List<Button>>();
            //UTF-8 Hex-410 Dec - 1040
            int iKey = Global.KEY_UTF8 - 1; 
            for (int i = 0; i < 4; i++)
            {
                lstKeyBoard.Add(new List<Button>());
                for (int j = 0; j < 8; j++)
                {
                    iKey++;
                    NewBtnKeyABC(iKey);
                    lstKeyBoard[i].Add(_btnKey);
                }
            }
     
            contrKeyBoard.ItemsSource = lstKeyBoard;
        }

        /// <summary>
        /// Формирует кнопку для клавиатуры.
        /// </summary>
        /// <param name="iKey"></param>
        private void NewBtnKeyABC(int iKey)
        {
            _btnKey = new Button();
            _btnKey.Name = Convert.ToChar(iKey).ToString();
            _btnKey.Content = Convert.ToChar(iKey).ToString().Trim();
            _btnKey.KeyDown += BtnKey_KeyDown;
            _btnKey.Click += this.Button_Click;
            _btnKey.Background = Global.DIALOG_COLOR;
            _btnKey.Margin = new Thickness(1);
            _btnKey.MinWidth = 40;
            _btnKey.MinHeight = 40;
            _btnKey.MaxWidth = 120;
            _btnKey.MaxHeight = 120;
            //     btnKey.Foreground = mainColor;
            //    btnKey.BorderBrush = mainColor;
        }

        private void BtnKey_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            var itemsSource = lst.ItemsSource as IEnumerable;
            foreach (var item in itemsSource)
            {

                if (((Button)item).Name == _txtButtonDown.Name)
                    ((Button)item).Content = ((Button)sender).Content;
            }
        }

        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //// TODO: Подготовьте здесь страницу для отображения.

            //// TODO: Если приложение содержит несколько страниц, обеспечьте
            //// обработку нажатия аппаратной кнопки "Назад", выполнив регистрацию на
            //// событие Windows.Phone.UI.Input.HardwareButtons.BackPressed.
            //// Если вы используете NavigationHelper, предоставляемый некоторыми шаблонами,
            //// данное событие обрабатывается для вас.
            //SuspensionManager.RegisterFrame(ScenarioFrame, "scenarioFrame");
            //if (ScenarioFrame.Content == null)
            //{
            //    // When the navigation stack isn't restored navigate to the ScenarioList
            //    if (!ScenarioFrame.Navigate(typeof(ScenarioList)))
            //    {
            //        throw new Exception("Failed to create scenario list");
            //    }
            //}
        }

 
        /// <summary>
        /// Начинается новая игра по нажатию на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNewGameClick(object sender, RoutedEventArgs e)
        {
            //    _running = false;
            NewGame();

       }

    

        /// <summary>
        /// Устанавливает буквы по диагонали.
        /// </summary>
        private void ABCButtonDuagonal()
        {
            var itemSourse = lst.ItemsSource as List<List<Button>>;
            Random _rndABC = new Random();
            string abc = randomABC();
            for (int i = 0; i < Global.VERT_CELL; i++)
            {
                for (int j = 0; j < Global.GOR_CELL; j++)
                {
                    if (i == j)
                    {
                        itemSourse[i][j].Content = abc;
                        itemSourse[i][j].Foreground = Global.COLOR_ABC;
                    }
                    else
                    {
                        itemSourse[i][j].Background = null;
                        itemSourse[i][j].Content = null;
                    }
                }
            }
        }

        private string randomABC()
        {
            Random _rndABC = new Random();
            string abc;
            do
            {
                abc = Convert.ToChar(Global.KEY_UTF8 + _rndABC.Next(31)).ToString();
            }
            while (abc == "Ъ" ||
                   abc == "Ь" ||
                   abc == "Й" ||
                   abc == "Э");
            return abc;
        }
        
     
        private void GetImage()
        {
   
            ResolutionScale resolutionScale = DisplayInformation.GetForCurrentView().ResolutionScale;
             Uri uri = null;
            switch (resolutionScale)
            {
                case ResolutionScale.Scale100Percent:
                    uri = new Uri("ms-appx:///images/scale-100/girl.jpg");
                    break;
                case ResolutionScale.Scale140Percent:
                    uri = new Uri("ms-appx:///images/scale-140/girl.png");
                    break;
                case ResolutionScale.Scale180Percent:
                    uri = new Uri("ms-appx:///images/scale-180/girl.png");
                    break;
            }
         
            ImageFon.ImageSource = new BitmapImage(uri);
        }

       

        ///// <summary>
        ///// Проверяет окончание игры.
        ///// </summary>
        //private async void CheckOverGames()
        //{
        //    MsgPlayers();

        //    if (_numberSteep == Global.GOR_CELL * Global.VERT_CELL - Global.GOR_CELL)
        //    {
        //          _message = _resourceloader.GetString("gameOver");

        //        await DialogServise.ShowAlertAsync("", _message);
        //        _running = false;
        //        NewGame();
        //    }
        //    else
        //    {
        //        _running = true;
        //    }
        //}

        /// <summary>
        /// Подготавливает информацию о игроках.
        /// </summary>
        private void MsgPlayers()
        {
            countWordPlayer.Text = _game.MsgPlayers();
        }

     
        /// <summary>
        /// Обрабатывет нажатие букв.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_game.isRunning())
            {
                var itemSourse = lst.ItemsSource as List<List<Button>>;
                for (int i = 0; i < Global.GOR_CELL; i++)
                {
                    for (int j = 0; j < Global.VERT_CELL; j++)
                    {
                       ChangeAfterPlayer(sender, itemSourse, i, j);
                    }
                }
                this.contrKeyBoard.Visibility = Visibility.Collapsed;
                spButton.Visibility = Visibility.Visible;
            }
            CheckOverGames();
        }

        /// <summary>
        /// Метод производит изменения после хода игрока.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="itemSourse"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void ChangeAfterPlayer(object sender, List<List<Button>> itemSourse, int i, int j)
        {
            if (itemSourse[i][j].Name == _txtButtonDown.Name)
            {
                itemSourse[i][j].Foreground = Global.COLOR_PPLAYER1;
                itemSourse[i][j].Content = ((Button)sender).Content;
                CheckWord(i, j);
                _game.nextSteep();
            }
        }

        /// <summary>
        /// Проверяет, есть ли составленное слово по горизонтали 
        /// или по вертикали
        /// или по диоганали.
        /// </summary>
        /// <param name="gor">Координата строки.</param>
        /// <param name="vert">Координата столбца.</param>
        public void CheckWord(int gor, int vert)
        {
            var itemSourse = lst.ItemsSource as List<List<Button>>;
            string word = "";
            //Ищет слово по строке.
            for (int j = 0; j < Global.GOR_CELL; j++)
            {
                if (itemSourse[gor][j].Content != null)
                    word += itemSourse[gor][j].Content.ToString();
            }
            if (word.Length == Global.GOR_CELL)
            {
                _game.AddWordPlayers(word);
            }
        }

        /// <summary>
        /// Проверяет окончание уровня игры.
        /// </summary>
        public async void CheckOverGames()
        {
            MsgPlayers();
            _message = _game.CheckOverGames();
            if (_message != null)
            {
                await DialogServise.ShowAlertAsync("", _message);
                _game.Level();
                NextLevel();
                 //  NewGame();
            }
        }

        private void NextLevel()
        {
       
            if (_game.isLevel() == 1)
            {
                Global.TYPE_DICT = Global.FILENAME_DICT_FOURWORD;
                Global.VERT_CELL = Global.NUMBER_WORD_FOUR;
                Global.GOR_CELL = Global.NUMBER_WORD_FOUR;
            }
            if (_game.isLevel() == 2)
            {
                Global.TYPE_DICT = Global.FILENAME_DICT_FAINWORD;
                Global.VERT_CELL = Global.NUMBER_WORD_FAIN;
                Global.GOR_CELL = Global.NUMBER_WORD_FAIN;
            }
            if (_game.isLevel() == 3)
            {
                Global.TYPE_DICT = Global.FILENAME_DICT_SEXWORD;
                Global.VERT_CELL = Global.NUMBER_WORD_SEX;
                Global.GOR_CELL = Global.NUMBER_WORD_SEX;
            }
            _game.NextLevel();
            ListOfCreateButton();
            ABCButtonDuagonal();
        }


        /// <summary>
        ///Принажатии на клавишу ввода текста происходит вызов клавиш алфавита.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtButton_Click(object sender, RoutedEventArgs e)
        {

            ((Button)sender).Background = Global.NO_COLOR;
            if (((Button)sender).Foreground != Global.COLOR_ABC)
            {
                ((Button)sender).Background = Global.DIALOG_COLOR;
                ((Button)sender).Foreground = Global.COLOR_PPLAYER1;
                _txtButtonDown = (Button)sender;
                spButton.Visibility = Visibility.Collapsed;
                this.contrKeyBoard.Visibility = Visibility.Visible;
              
            }
          
        }


        /// <summary>
        /// При нажатии на клавишу прмощь, переходит на страницу помощи.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHelpClick(object sender, RoutedEventArgs e)
        {
             Frame.Navigate(typeof(HelpPage));
        }

        private void ClicTypeDictFourWord(object sender, RoutedEventArgs e)
        {
            Global.TYPE_DICT = Global.FILENAME_DICT_FOURWORD;
     
            Global.VERT_CELL = Global.NUMBER_WORD_FOUR;
            Global.GOR_CELL = Global.NUMBER_WORD_FOUR;
            ListOfCreateButton();
            NewGame();
        }

        private void ClicTypeDictFriWord(object sender, RoutedEventArgs e)
        {
            Global.TYPE_DICT = Global.FILENAME_DICT_FRIWORD;
            Global.VERT_CELL = Global.NUMBER_WORD_FRI;
            Global.GOR_CELL = Global.NUMBER_WORD_FRI;
            ListOfCreateButton();
            NewGame();
        }

      
        private void NewGame()
        {
            Global.TYPE_DICT = Global.FILENAME_DICT_FRIWORD;
            Global.VERT_CELL = Global.NUMBER_WORD_FRI;
            Global.GOR_CELL = Global.NUMBER_WORD_FRI;
            ListOfCreateButton();
            ABCButtonDuagonal();
            _game.NewGame();
            MsgPlayers();

        }
        
    }
}
