using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace aimlabclone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int score;
        private Random random;

        public MainWindow()
        {
            InitializeComponent();
            random = new Random();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Spustí herní smyčku
            StartGameLoop();
        }

        private void StartGameLoop()
        {
            // Generuje nové cíle na herním plátně každé 1.5 sekundy
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1.5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Generuje náhodnou pozici cíle na herním plátně
            double canvasWidth = gameCanvas.ActualWidth;
            double canvasHeight = gameCanvas.ActualHeight;
            double targetSize = 50;
            double x = random.NextDouble() * (canvasWidth - targetSize);
            double y = random.NextDouble() * (canvasHeight - targetSize);

            // Vytvoří nový cíl jako elipsu na herním plátně
            Ellipse target = new Ellipse();
            target.Width = targetSize;
            target.Height = targetSize;
            target.Fill = Brushes.Red;
            target.MouseDown += Target_MouseDown;

            // Nastaví pozici cíle na vygenerované souřadnice
            Canvas.SetLeft(target, x);
            Canvas.SetTop(target, y);

            // Přidá cíl do herního plátna
            gameCanvas.Children.Add(target);
        }

        private void Target_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Odstraní zasažený cíl z herního plátna a zvýší skóre
            Ellipse target = (Ellipse)sender;
            gameCanvas.Children.Remove(target);
            score++;
            scoreLabel.Content = "Score: " + score;
        }

        private void gameCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Penalizuje hráče za kliknutí mimo cíl snížením skóre
            score--;
            scoreLabel.Content = "Score: " + score;
        }
    }
}


