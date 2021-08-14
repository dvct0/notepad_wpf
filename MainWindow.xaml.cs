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
            FileInfo[] files = dir.GetFiles("*.txt");
            foreach (var item in files)
            {
                Notes.Add(item);
            }
        }

        private void Create (object sender, RoutedEventArgs e)//Create a new note
        {
            if (NameForNewNote.Text != "" && !File.Exists(NotesDirectory + NameForNewNote.Text))
            {
                File.Create(NotesDirectory + NameForNewNote.Text + ".txt");
            }
            GetNotesList();
        }

        private void Delete (object sender, RoutedEventArgs e)//Delete selected note
        {
            if (listOfNotes.SelectedIndex != -1)
            {
                if (File.Exists(NotesDirectory + listOfNotes.SelectedItem.ToString()))
                {
                    File.Delete(NotesDirectory + listOfNotes.SelectedItem.ToString());
                }
            }
            note.Text = "";
            GetNotesList();
        }

        private void Edit (object sender, RoutedEventArgs e)//Edit selected note
        {
            if (listOfNotes.SelectedIndex != -1)
            {
                note.IsReadOnly = false;
            }
        }

        private void Save (object sender, RoutedEventArgs e)//Save selected note
        {
            if (listOfNotes.SelectedIndex != -1)
            {
                if (File.Exists(NotesDirectory + listOfNotes.SelectedItem.ToString()))
                {
                    File.WriteAllText(NotesDirectory + listOfNotes.SelectedItem.ToString(), note.Text);
                    note.IsReadOnly = true;
                }
            }
        }

        private void ViewSelectedNote (object sender, SelectionChangedEventArgs e)//show note
        {
            note.IsReadOnly = true;
            if (listOfNotes.SelectedIndex != -1)
            {
                note.Text = File.ReadAllText(NotesDirectory + listOfNotes.SelectedItem.ToString());
            }
        }

        private void ValidInputForFileName (object sender, System.Windows.Input.TextCompositionEventArgs e)//check file name
        {
            e.Handled = !("<>:\"/\\|?*".IndexOf(e.Text) < 0);
        }
    }
}
