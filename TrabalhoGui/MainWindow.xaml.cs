using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

// ADICIONE ESTE USING se o Book estiver em Models:
using TrabalhoGUI.Models;

// OU se o Book estiver na raiz (sem pasta Models):
// using TrabalhoGUI;

namespace TrabalhoGUI
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Book> Books { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Books = new ObservableCollection<Book>();
            DataContext = this;
            
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadBooksAsync();
        }

        private async Task LoadBooksAsync()
        {
            try
            {
                LoadingPanel.Visibility = Visibility.Visible;
                StatusText.Text = "Carregando livros...";
                
                var books = await GetBooksFromApiAsync();
                
                Books.Clear();
                foreach (var book in books)
                {
                    Books.Add(book);
                }
                
                StatusText.Text = $"Carregados {Books.Count} livros";
                
                // Mostra mensagem se carregou dados
                if (Books.Count > 0)
                {
                    StatusText.Text += $" - Primeiro: {Books[0].Nome_Livro}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar livros: {ex.Message}", 
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusText.Text = "Erro ao carregar dados";
            }
            finally
            {
                LoadingPanel.Visibility = Visibility.Collapsed;
            }
        }

        private async Task<List<Book>> GetBooksFromApiAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5229/api/books");
            
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var books = JsonConvert.DeserializeObject<List<Book>>(json);
                return books ?? new List<Book>(); // Corrige warning CS8603
            }
            
            return new List<Book>();
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadBooksAsync();
        }
    }
}