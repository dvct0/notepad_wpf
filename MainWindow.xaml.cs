using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using System.IO;

namespace notepad_wpf
{
    public partial class MainWindow : Window
    {
        ObservableCollection<FileInfo> Notes = new ObservableCollection<FileInfo>();
        DirectoryInfo RootDir = new DirectoryInfo(Directory.GetCurrentDirectory());
        string NotesDirectory;

        public MainWindow ()
        {
            InitializeComponent();
            listOfNotes.ItemsSource = Notes;

            //check and create a folder for notes
            NotesDirectory = RootDir.ToString() + "/notes/";
            if (Directory.Exists(NotesDirectory) == false)
            {
                Directory.CreateDirectory(NotesDirectory);
            }

            GetNotesList();
        }

        private void GetNotesList ()//get a list with notes
        {
            Notes.Clear();
            DirectoryInfo dir = new DirectoryInfo(NotesDirectory);
            FileInfo[] files = dir.GetFiles("*.html");
            foreach (var item in files)
            {
                Notes.Add(item);
            }
        }

        private void Create (object sender, RoutedEventArgs e)//Create a new note
        {
            try
            {
                File.Create(NotesDirectory + "/" + NameForNewNote.Text + ".html");
            }
            catch
            {

            }
            GetNotesList();
        }

        private void Delete (object sender, RoutedEventArgs e)//Delete selected note
        {

        }

        private void Edit (object sender, RoutedEventArgs e)//Edit selected note
        {

        }

        private void Save (object sender, RoutedEventArgs e)//Save selected note
        {

        }
    }
}
