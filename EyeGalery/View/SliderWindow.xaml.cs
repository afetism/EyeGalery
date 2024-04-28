using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using MessageBox = System.Windows.Forms.MessageBox;

namespace EyeGalery.View
{
    /// <summary>
    /// Interaction logic for SliderWindow.xaml
    /// </summary>
    public partial class SliderWindow : Window, INotifyPropertyChanged
    {
        string _source { get; set; }
       public string Source {
            get => _source
                ;
            set
            {
                _source= value;
                NotifyPropertyChanged();
        
             }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<string> Items { get; set; } = new();
        public SliderWindow()
        {
            InitializeComponent();
            DataContext=this;
        }

     
       

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool itemFound = false; 
        private int currentIndex = 0;
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            string sourceString = Source.ToString();

            for (int i = 0; i < Items.Count-1; i++)
            {
                if (Items[i] == sourceString)
                {
                    itemFound = true;
                    currentIndex = i;
                    break;
                }
            }

            if (itemFound)
            {
                
                
                Source=Items[currentIndex + 1];
           
                itemFound = false; 
            }
            else
            {
             Next.Visibility = Visibility.Hidden;
              
            }
        }

        private void BackPhoto_Click(object sender, RoutedEventArgs e)
        {
            string sourceString = Source.ToString();

            for (int i = Items.Count-1; i >0; i--)
            {
                if (Items[i] == sourceString)
                {
                    itemFound = true;
                    currentIndex = i;
                    break;
                }
            }

            if (itemFound)
            {


                Source=Items[currentIndex - 1];

                itemFound = false;
                Next.Visibility = Visibility.Visible;
            }
            else
            {
                Back.Visibility = Visibility.Hidden;

            }
        }

        private DispatcherTimer timer;

        private void Auto_Play_Click(object sender, RoutedEventArgs e)
        {
           
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); 
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
      
            if (currentIndex < Items.Count - 1)
            {
                currentIndex++;
                Source = Items[currentIndex];
            }
            else
            {
                
                timer.Stop();
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
            }
        }
    }
}
