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
using System.Windows.Media.Animation;
using System.Timers;
using System.Windows.Threading;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Анимация остановка
    /// Нехватка при обработке тика
    /// увеличенная картинка --
    /// Анимация птички
    /// Перезагрузка формы
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timerBird = new DispatcherTimer();
        List<Rectangle> ListRect = new List<Rectangle> { };

        public MainWindow()
        {

            InitializeComponent();
            //Random rand = new Random();

            timerBird.Tick += timerBird_Tick;
            timerBird.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timerBird.Start();
             

            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0,0,2); // рандом для сложности
            timer.Start();        

        }

        void timerBird_Tick(object sender, EventArgs e)
        {
            
            //System.Diagnostics.Debug.WriteLine("{0} sdfsdf",DateTime.Now.Millisecond.ToString());
            //throw new NotImplementedException(); // 320 слева 40 ширина
            //foreach (var tr in ListRect)
            //{
            //    if (Canvas.GetLeft(tr) < 320 && Canvas.GetLeft(tr) > 220)
            //    {
            //        if (Canvas.GetTop(Pic) <= tr.Height)//|| Canvas.GetBottom(Pic) <= tr.Height)
            //        {
            //            throw new Exception("Ошибка");
            //        }
            //    }
            //    else
            //        if (Canvas.GetLeft(tr) < 220)
            //        {
            //            ListRect.RemoveAt(1);

            //            throw new Exception("Удалить бы");
            //        }
            //}

            if (ListRect.Count >= 2)
            {
                if (Canvas.GetLeft(ListRect[0]) < 330 && Canvas.GetLeft(ListRect[0]) > 230)
                {
                    if (Canvas.GetTop(Pic) <= ListRect[0].Height-15 || Canvas.GetTop(Pic) >= ListRect[0].Height+100-30 )// ширина обработка
                    {
                        //throw new Exception(string.Format("Ошибка + {0}",ListRect.Count));
                        Stop();
                    }
                }
                else
                    if (Canvas.GetLeft(ListRect[0]) < 220)
                    {
                        ListRect.RemoveAt(0);
                        ListRect.RemoveAt(0);

                        //throw new Exception("Удалить бы");
                    }
                if (Canvas.GetTop(Pic) > 259)
                {
                    Stop();
                }
            }

        }

        void timer_Tick(object sender, EventArgs e)
        {
            Bou();
            //throw new NotImplementedException();
            
        }

        Random rand = new Random();
        double X;
        DoubleAnimation a = new DoubleAnimation();


        protected override void OnKeyDown(KeyEventArgs e)
        {
            X = Canvas.GetTop(Pic) - 40;  // -- прыжок 25
            if (e.Key == Key.Up && this.timer.IsEnabled)
            {
                //var AnimationRound = new DoubleAnimation();


                //.Text = text.Text + "+";
                //Canvas.SetTop(Pic, Canvas.GetTop(Pic)-50); // не меняет значение в SE             


                var eA = new ExponentialEase();
                eA.EasingMode = EasingMode.EaseIn;
                //eA.EasingMode = EasingMode.EaseOut;
                //eA.Oscillations = 100;
                //eA.Springiness = 100;
                a.EasingFunction = eA;//

                a.From = X; //100; //Canvas.GetTop(Pic); -- начало
                a.To = 260;//Convas.Height + 48;           -- конец
                //a.AccelerationRatio = 0.5;
                a.Duration = TimeSpan.FromSeconds((275 - X) / 200);  // -- 100 - скорость
                
                //
                //var eA = new QuarticEase();
                
                //

                Pic.BeginAnimation(Canvas.TopProperty, a);
                
                //Canvas.SetTop(Pic, X - 30);

            }
            base.OnKeyDown(e);
        }


        private void Stop()
        {
            this.timer.Stop();
            this.timerBird.Stop();
            this.St.Pause(this);            
            Convas_1.Visibility = Visibility.Visible;
            if (St.Children.Count > 4)
            {
                TextBlock2.Text = TextBlock2.Text + (this.St.Children.Count/2 -2);
            }
            else TextBlock2.Text =  TextBlock2.Text + 0;
        }
       


        DoubleAnimation b = new DoubleAnimation();
        DoubleAnimation c = new DoubleAnimation();

        public Storyboard St = new Storyboard();


        public void Bou() // стобики
        {            
            Random rand = new Random();
            double srand = rand.Next(3, 5);

            double random = rand.Next(20, 200);
           
            Rectangle rec = new Rectangle();
            //SolidColorBrush MySolidColorBrush = new SolidColorBrush();
            ImageBrush MyImageBrush = new ImageBrush();
            MyImageBrush.Stretch = Stretch.None;
            MyImageBrush.ImageSource = new BitmapImage(new Uri(@"..\..\Five.jpg", UriKind.Relative));
            //MySolidColorBrush.Color = Color.FromArgb(255, 151, 199, 130);
            rec.Width = 50;
            rec.Height = random;
            //rec.Fill = MySolidColorBrush;
            rec.Fill = MyImageBrush;
            //rec.Name = string.Format("Top_rec_{0}", Convas.Children.Count.ToString());
            Convas.Children.Add(rec);
            Canvas.SetZIndex(rec, -2);
            Canvas.SetRight(rec, 20);
            Canvas.SetTop(rec, 0);

            //SolidColorBrush MySolidColorBrush1 = new SolidColorBrush();
            //MySolidColorBrush1.Color = Color.FromArgb(255, 151, 199, 130);
            Rectangle rec_1 = new Rectangle();
            
            rec_1.Width = 50;
            rec_1.Height = 319 - 100 - random;
            //rec_1.Fill = MySolidColorBrush1;
            rec_1.Fill = MyImageBrush;
            //rec_1.Name = string.Format("Bottom_rec_{0}",Convas.Children.Count-1);
            Convas.Children.Add(rec_1);
            Canvas.SetTop(rec_1, random + 100);
            Canvas.SetZIndex(rec_1, -1);
            Canvas.SetRight(rec_1, 20);

            //c.From = Convas.Width;
            //c.To = 0;
            //c.Duration = TimeSpan.FromSeconds(5);
            ////rec_1.BeginAnimation(Canvas.LeftProperty, c);
            //Storyboard.SetTarget(c, rec_1);
            //Storyboard.SetTargetProperty(c, new PropertyPath(Canvas.LeftProperty));

            SetAnimation(rec, St, b);
            SetAnimation(rec_1, St, c);

            //St.Children.Add(c);
            ////c.Name = string.Format("c{0}",DateTime.Now.Second);
            //St.Children.Add(b);
            ////b.Name = string.Format("b{0}", DateTime.Now.Second);

            this.ListRect.Add(rec);
            this.ListRect.Add(rec_1);


            St.Begin(this,true);
            //foreach (var f in this.St.Children)
            //{
            //    System.Diagnostics.Debug.WriteLine("sdfsdf {0}", f.Name);
            //}

        }

        private void SetAnimation(Rectangle rec, Storyboard st, DoubleAnimation da)
        {

            da.From = Convas.Width;
            da.To = 0;
            da.Duration = TimeSpan.FromSeconds(5);
            //var eA = new ElasticEase();
            //eA.EasingMode = EasingMode.EaseOut;
            //eA.Oscillations = 100;
            //eA.Springiness = 100;
            //da.EasingFunction = eA;
            
            //rec.BeginAnimation(Canvas.LeftProperty, b);
            Storyboard.SetTarget(da, rec);
            Storyboard.SetTargetProperty(da, new PropertyPath(Canvas.LeftProperty));
            St.Children.Add(da);
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Convas_1.Visibility = Visibility.Hidden;
            System.Diagnostics.Process.Start("WpfApplication3.exe");
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Convas_1.Visibility = Visibility.Hidden;
            this.Close();
        }
    }
}

// Плавность анимации
//DoubleAnimation.EasingFunction
//Функция плавности ElasticEase
//
//
