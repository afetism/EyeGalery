using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using ListBox = System.Windows.Controls.ListBox;
using Image = System.Windows.Controls.Image;
using EyeGalery.View;
namespace EyeGalery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
      
        public ObservableCollection<String> Items { get; set; } = new ObservableCollection<String>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext=this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void Click_New(object sender, RoutedEventArgs e)
        {
            Items.Clear();

            Items=new();
        }
        public string root;

        private void Click_Open(object sender, RoutedEventArgs e)
        {

            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.Description = "Select a folder";
            dialog.UseDescriptionForTitle = true;

            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                string selectedFolder = dialog.SelectedPath;

                root=selectedFolder;
                var files = Directory.GetFiles(selectedFolder);

                foreach (string file in files)
                {
                    Items.Add(file);
                }
            }
        }






    
    

        private void Click_SaveAs(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Click_Save(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();

            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                string selectedFolderPath = dialog.SelectedPath;

                
                string fileName = "yeniDosya.txt";
                string filePath = Path.Combine(selectedFolderPath, fileName);

               
                File.WriteAllText(filePath, "Dosya içeriği");
            }
        }

        private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;
            var selectedImage = listBox.SelectedItem as string;

            if (selectedImage != null)
            {


                SliderWindow sliderWindow = new SliderWindow();
                sliderWindow.Source= selectedImage;
                sliderWindow.Items=Items;
                var result= sliderWindow.ShowDialog();
          
                if (result == true)
                {

                }


            }
        }

        private void Click_AddFile(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFile = openFileDialog.FileName;

                Items.Add(selectedFile);
            }
        }
    }
}