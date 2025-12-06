using System.ComponentModel;
using Newtonsoft.Json;

namespace TrabalhoGUI.Models
{
    public class Book : INotifyPropertyChanged
    {
        private int _id;
        private string? _nome_Livro;    // ← underline e L minúsculo
        private string? _nome_Autor;    // ← underline e A minúsculo
        private decimal _preco;
        private int _id_Categoria;      // ← underline
        private string? _categoriaNome; // ← sem underline (vem do JSON como categoriaNome)
        private string? _imagem_Url;    // ← underline e U minúsculo
        private string? _descricao;

        [JsonProperty("id")]
        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        [JsonProperty("nome_Livro")]
        public string? Nome_Livro       // ← underline e L minúsculo
        {
            get => _nome_Livro;
            set { _nome_Livro = value; OnPropertyChanged(nameof(Nome_Livro)); }
        }

        [JsonProperty("nome_Autor")]
        public string? Nome_Autor       // ← underline e A minúsculo
        {
            get => _nome_Autor;
            set { _nome_Autor = value; OnPropertyChanged(nameof(Nome_Autor)); }
        }

        [JsonProperty("preco")]
        public decimal Preco
        {
            get => _preco;
            set { _preco = value; OnPropertyChanged(nameof(Preco)); }
        }

        [JsonProperty("id_Categoria")]
        public int Id_Categoria         // ← underline
        {
            get => _id_Categoria;
            set { _id_Categoria = value; OnPropertyChanged(nameof(Id_Categoria)); }
        }

        [JsonProperty("categoriaNome")]
        public string? CategoriaNome    // ← sem underline
        {
            get => _categoriaNome;
            set { _categoriaNome = value; OnPropertyChanged(nameof(CategoriaNome)); }
        }

        [JsonProperty("imagem_Url")]
        public string? Imagem_Url       // ← underline e U minúsculo
        {
            get => _imagem_Url;
            set { _imagem_Url = value; OnPropertyChanged(nameof(Imagem_Url)); }
        }

        [JsonProperty("descricao")]
        public string? Descricao
        {
            get => _descricao;
            set { _descricao = value; OnPropertyChanged(nameof(Descricao)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}