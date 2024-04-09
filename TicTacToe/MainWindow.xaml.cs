// MainWindow.xaml.cs
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TicTacToe
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _isPlayerX = true;
        private readonly string[,] _board = new string[3, 3];
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CellClickCommand { get; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            CellClickCommand = new RelayCommand(CellClick);
        }

        private void CellClick(object parameter)
        {
            var coords = parameter as string;
            if (coords == null) return;

            var parts = coords.Split(',');
            if (parts.Length != 2) return;

            var row = int.Parse(parts[0]);
            var column = int.Parse(parts[1]);

            if (_board[row, column] == null)
            {
                _board[row, column] = _isPlayerX ? "X" : "O";
                Button button = (Button)MainGrid.Children[row * 3 + column];
                button.Content = _isPlayerX ? "X" : "O";
                _isPlayerX = !_isPlayerX;

                if (CheckForWinner())
                {
                    MessageBox.Show((_isPlayerX ? "O" : "X") + " wins!");
                    ResetBoard();
                }
                else if (IsBoardFull())
                {
                    MessageBox.Show("It's a draw!");
                    ResetBoard();
                }
            }
        }

        private bool CheckForWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_board[i, 0] == _board[i, 1] && _board[i, 1] == _board[i, 2] && _board[i, 0] != null)
                    return true;
                if (_board[0, i] == _board[1, i] && _board[1, i] == _board[2, i] && _board[0, i] != null)
                    return true;
            }

            if (_board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2] && _board[0, 0] != null)
                return true;
            if (_board[0, 2] == _board[1, 1] && _board[1, 1] == _board[2, 0] && _board[0, 2] != null)
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            foreach (var cell in _board)
            {
                if (cell == null)
                    return false;
            }
            return true;
        }

        private void ResetBoard()
        {
            _isPlayerX = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _board[i, j] = null;
                }
            }
            foreach (var button in MainGrid.Children)
            {
                if (button is Button btn)
                {
                    btn.Content = "";
                }
            }
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
