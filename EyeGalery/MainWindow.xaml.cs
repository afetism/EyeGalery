using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Ookii.Dialogs.Wpf;
using ListBox = System.Windows.Controls.ListBox;
using EyeGalery.View;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


namespace EyeGalery;





    
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
      
        public ObservableCollection<String> Items { get; set; } = new ObservableCollection<String>();
        bool  firstSave= true;
        String SelectedFolder { get; set; }=String.Empty;
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

            
        }
   

        private void Click_Open(object sender, RoutedEventArgs e)
        {

            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.Description = "Select a folder";
            dialog.UseDescriptionForTitle = true;

            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
             string selectedFolder = dialog.SelectedPath;

                
                var files = Directory.GetFiles(selectedFolder);

                foreach (string file in files)
                {
                    Items.Add(file);
                }
            }
        }


        private void Click_Save(object sender, RoutedEventArgs e)
        {
          if(firstSave)
          {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.Description = "Select a folder";
            dialog.UseDescriptionForTitle = true;

            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                string selectedFolder = dialog.SelectedPath;
                foreach (string imagePath in Items)
                {
                    string fileName = Path.GetFileName(imagePath);
                    string destinationPath = Path.Combine(selectedFolder, fileName);

                    if (!File.Exists(destinationPath))
                    {
                        File.Copy(imagePath, destinationPath);
                    }
                }
            }
          }
          else
          {
            string selectedFolder = SelectedFolder;
            foreach (string imagePath in Items)
            {
                string fileName = Path.GetFileName(imagePath);
                string destinationPath = Path.Combine(selectedFolder, fileName);

                if (!File.Exists(destinationPath))
                {
                    File.Copy(imagePath, destinationPath);
                }
            }
        }
        }




        private void Click_SaveAs(object sender, RoutedEventArgs e)
        {

            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.Description = "Select a folder";
            dialog.UseDescriptionForTitle = true;

            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                string selectedFolder = dialog.SelectedPath;
                SelectedFolder = selectedFolder;
                foreach (string imagePath in Items)
                {
                    string fileName = Path.GetFileName(imagePath);
                    string destinationPath = Path.Combine(selectedFolder, fileName);

                    if (!File.Exists(destinationPath))
                    {
                        File.Copy(imagePath, destinationPath);
                    }
                }
            firstSave=false;
        }
     
        }

        private void Click_Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
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

        private void Click_SmallIcon(object sender, RoutedEventArgs e)
        {
            ListBox.Style = FindResource("SmallIconListBoxStyle") as Style;


        }

        private void Click_LargeIcon(object sender, RoutedEventArgs e)
        {
            ListBox.Style = FindResource("ImageListBoxStyle") as Style;
        }
    }
