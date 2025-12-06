using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TrabalhoGUI.Models;

namespace TrabalhoGUI.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5229/api";

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/books");
                
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var books = JsonConvert.DeserializeObject<List<Book>>(json);
                    return books ?? new List<Book>();
                }
                
                return new List<Book>();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Erro ao conectar com a API: {ex.Message}", 
                    "Erro de Conexão", 
                    System.Windows.MessageBoxButton.OK, 
                    System.Windows.MessageBoxImage.Error);
                return new List<Book>();
            }
        }
    }
}