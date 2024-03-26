using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _projectPath = @"D:\Рабочий стол\Інтерфейси\DPGI\Task6\Files\file\";

        public MainWindow()
        {
            InitializeComponent();

            var deleteCommand = new CommandBinding(ApplicationCommands.Delete, Execute_Delete, CanExecute_Delete);
            var openCommand = new CommandBinding(ApplicationCommands.Open, Execute_Open, CanExecute_Open);
            var saveCommand = new CommandBinding(ApplicationCommands.Save, Execute_Save, CanExecute_Save);

            CommandBindings.Add(deleteCommand);
            CommandBindings.Add(openCommand);
            CommandBindings.Add(saveCommand);

        }

        public void CanExecute_Delete(object sender, CanExecuteRoutedEventArgs e)
        {
            if (inputTextBox.Text.Trim().Length > 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        public void Execute_Delete(object sender, ExecutedRoutedEventArgs e)
        {
            inputTextBox.Clear();
        }

        public void CanExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void Execute_Open(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog fleDialog = new OpenFileDialog
            {
                InitialDirectory = _projectPath,
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (fleDialog.ShowDialog() == true)
            {
                try
                {
                    inputTextBox.Text = File.ReadAllText(fleDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        public void CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            if (inputTextBox.Text.Trim().Length > 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        public void Execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            string filePath = _projectPath + "text.txt";
            File.WriteAllText(filePath, inputTextBox.Text);
            MessageBox.Show("The file was saved!");
        }

    }
}