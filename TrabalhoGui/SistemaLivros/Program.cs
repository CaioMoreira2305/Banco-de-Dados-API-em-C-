using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaLivros
{
    // CLASSE LIVRO
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public decimal Preco { get; set; }
        public string Categoria { get; set; }
    }

    // FORMULÁRIO PRINCIPAL
    public class MainForm : Form
    {
        private List<Livro> livros = new List<Livro>();
        private DataGridView grid;
        private Label statusLabel;
        private int proximoId = 22;

        public MainForm()
        {
            SetupForm();
            CarregarDadosIniciais();
            AtualizarGrid();
        }

        private void SetupForm()
        {
            // Configurações da janela
            Text = "📚 Sistema de Gerenciamento de Livros - CRUD";
            Size = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;

            // Painel superior com botões
            var topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.LightGray
            };

            // Botões CRUD
            var btnAdicionar = new Button
            {
                Text = "➕ Adicionar",
                Location = new Point(20, 10),
                Size = new Size(100, 30),
                BackColor = Color.LightGreen
            };
            btnAdicionar.Click += (s, e) => AdicionarLivro();

            var btnEditar = new Button
            {
                Text = "✏️ Editar",
                Location = new Point(130, 10),
                Size = new Size(100, 30),
                BackColor = Color.LightBlue
            };
            btnEditar.Click += (s, e) => EditarLivro();

            var btnExcluir = new Button
            {
                Text = "🗑️ Excluir",
                Location = new Point(240, 10),
                Size = new Size(100, 30),
                BackColor = Color.LightCoral
            };
            btnExcluir.Click += (s, e) => ExcluirLivro();

            var btnAtualizar = new Button
            {
                Text = "🔄 Atualizar",
                Location = new Point(350, 10),
                Size = new Size(100, 30)
            };
            btnAtualizar.Click += (s, e) => AtualizarGrid();

            // Grade de dados
            grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(10),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };

            // Label de status
            statusLabel = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.AliceBlue,
                Font = new Font("Arial", 9, FontStyle.Bold),
                Text = "Pronto..."
            };

            // Adicionar controles
            topPanel.Controls.AddRange(new Control[] { btnAdicionar, btnEditar, btnExcluir, btnAtualizar });
            Controls.AddRange(new Control[] { grid, statusLabel, topPanel });
        }

        private void CarregarDadosIniciais()
        {
            // Dados iniciais (21 livros)
            livros = new List<Livro>
            {
                new Livro { Id = 1, Titulo = "Catónia, Volumen I", Autor = "Mestre Stewart", Preco = 0.00m, Categoria = "Académicos" },
                new Livro { Id = 2, Titulo = "Introdução à Economia", Autor = "N. Gregory Marley", Preco = 0.00m, Categoria = "Académicos" },
                new Livro { Id = 3, Titulo = "Princípios da Física", Autor = "Raymond A. Schway e John", Preco = 0.00m, Categoria = "Académicos" },
                new Livro { Id = 4, Titulo = "Estruturas de Diários e Algoritmos em Administração", Autor = "Adam Douzicke", Preco = 0.00m, Categoria = "Académicos" },
                new Livro { Id = 5, Titulo = "Pesquisa Operacional", Autor = "Henry J.Bai", Preco = 0.00m, Categoria = "Académicos" },
                new Livro { Id = 6, Titulo = "Outrinas a Ciência Central", Autor = "Theodore L. Brown et al., 2009", Preco = 0.00m, Categoria = "Académicos" },
                new Livro { Id = 7, Titulo = "Bancos de Neve", Autor = "Imbós Grimm", Preco = 0.00m, Categoria = "Literatura infantil" },
                new Livro { Id = 8, Titulo = "Chaquetilhos Vermelho", Autor = "Imbós Grimm", Preco = 0.00m, Categoria = "Literatura infantil" },
                new Livro { Id = 9, Titulo = "Grochtes", Autor = "Charles Fernault", Preco = 0.00m, Categoria = "Literatura infantil" },
                new Livro { Id = 10, Titulo = "Os Tés Pougainhas", Autor = "Joseph McGee", Preco = 0.00m, Categoria = "Literatura infantil" },
                new Livro { Id = 11, Titulo = "João e Maria", Autor = "Imbós Grimm", Preco = 0.00m, Categoria = "Literatura infantil" },
                new Livro { Id = 12, Titulo = "A Bela Adormecida", Autor = "Charles Fernault", Preco = 0.00m, Categoria = "Literatura infantil" },
                new Livro { Id = 13, Titulo = "Assuntos no Espresso Oriente", Autor = "Aguilar Carlos de Oliveira", Preco = 0.00m, Categoria = "Materno" },
                new Livro { Id = 14, Titulo = "O Código Da Vinci", Autor = "Dan Brown", Preco = 0.00m, Categoria = "Materno" },
                new Livro { Id = 15, Titulo = "O Sábado dos Inocentes", Autor = "Thomas Harris", Preco = 0.00m, Categoria = "Materno" },
                new Livro { Id = 16, Titulo = "Harry Potter e a Poeta Rizovski", Autor = "J.L. Rowling", Preco = 0.00m, Categoria = "Ficção" },
                new Livro { Id = 17, Titulo = "O Senhor dos Antes A Sociedade do Estado", Autor = "J.E.S. Tolton", Preco = 0.00m, Categoria = "Ficção" },
                new Livro { Id = 18, Titulo = "Duna", Autor = "Frank Herbert", Preco = 0.00m, Categoria = "Ficção" },
                new Livro { Id = 19, Titulo = "Jogos Vorazes", Autor = "Starame Collins", Preco = 0.00m, Categoria = "Ficção" },
                new Livro { Id = 20, Titulo = "1984", Autor = "Giorgio Orwell", Preco = 0.00m, Categoria = "Ficção" },
                new Livro { Id = 21, Titulo = "Jadiratável Marcelo Novo", Autor = "Jabous Husky", Preco = 0.00m, Categoria = "Ficção" }
            };
        }

        private void AtualizarGrid()
        {
            grid.DataSource = null;
            grid.DataSource = livros;
            statusLabel.Text = $"📚 {livros.Count} livros cadastrados | Selecione um para editar/excluir";
        }

        // === OPERAÇÕES CRUD ===

        private void AdicionarLivro()
        {
            var form = new LivroForm("Adicionar Novo Livro");
            if (form.ShowDialog() == DialogResult.OK)
            {
                livros.Add(new Livro
                {
                    Id = proximoId++,
                    Titulo = form.Titulo,
                    Autor = form.Autor,
                    Preco = form.Preco,
                    Categoria = form.Categoria
                });
                AtualizarGrid();
                MessageBox.Show("Livro adicionado com sucesso!", "Sucesso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditarLivro()
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um livro para editar.", "Aviso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var livro = (Livro)grid.SelectedRows[0].DataBoundItem;
            var form = new LivroForm("Editar Livro", livro);
            
            if (form.ShowDialog() == DialogResult.OK)
            {
                livro.Titulo = form.Titulo;
                livro.Autor = form.Autor;
                livro.Preco = form.Preco;
                livro.Categoria = form.Categoria;
                AtualizarGrid();
                MessageBox.Show("Livro atualizado com sucesso!", "Sucesso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExcluirLivro()
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um livro para excluir.", "Aviso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var livro = (Livro)grid.SelectedRows[0].DataBoundItem;
            var resposta = MessageBox.Show($"Excluir o livro '{livro.Titulo}'?", 
                "Confirmar Exclusão", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (resposta == DialogResult.Yes)
            {
                livros.Remove(livro);
                AtualizarGrid();
                MessageBox.Show("Livro excluído com sucesso!", "Sucesso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    // FORMULÁRIO PARA ADICIONAR/EDITAR LIVRO
    public class LivroForm : Form
    {
        public string Titulo { get; private set; } = "";
        public string Autor { get; private set; } = "";
        public decimal Preco { get; private set; }
        public string Categoria { get; private set; } = "";

        private TextBox txtTitulo, txtAutor, txtPreco;
        private ComboBox cmbCategoria;

        public LivroForm(string tituloForm, Livro livro = null)
        {
            SetupForm(tituloForm);
            if (livro != null) CarregarDados(livro);
        }

        private void SetupForm(string tituloForm)
        {
            // Configurações do formulário
            Text = tituloForm;
            Size = new Size(450, 300);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            // Labels
            var lblTitulo = new Label { Text = "Título:", Location = new Point(20, 30), Size = new Size(80, 25) };
            var lblAutor = new Label { Text = "Autor:", Location = new Point(20, 70), Size = new Size(80, 25) };
            var lblPreco = new Label { Text = "Preço (R$):", Location = new Point(20, 110), Size = new Size(80, 25) };
            var lblCategoria = new Label { Text = "Categoria:", Location = new Point(20, 150), Size = new Size(80, 25) };

            // Campos de entrada
            txtTitulo = new TextBox { Location = new Point(110, 30), Size = new Size(300, 25) };
            txtAutor = new TextBox { Location = new Point(110, 70), Size = new Size(300, 25) };
            txtPreco = new TextBox { Location = new Point(110, 110), Size = new Size(100, 25) };
            txtPreco.Text = "0.00";

            // ComboBox de categorias
            cmbCategoria = new ComboBox { Location = new Point(110, 150), Size = new Size(200, 25) };
            cmbCategoria.Items.AddRange(new string[] {
                "Académicos", 
                "Literatura infantil", 
                "Materno", 
                "Ficção", 
                "Não-ficção", 
                "Técnico", 
                "Outros"
            });
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;

            // Botões
            var btnSalvar = new Button { 
                Text = "Salvar", 
                Location = new Point(150, 200), 
                Size = new Size(100, 35),
                DialogResult = DialogResult.OK,
                BackColor = Color.LightGreen
            };
            btnSalvar.Click += ValidarESalvar;

            var btnCancelar = new Button { 
                Text = "Cancelar", 
                Location = new Point(260, 200), 
                Size = new Size(100, 35),
                DialogResult = DialogResult.Cancel,
                BackColor = Color.LightCoral
            };

            // Adicionar controles
            Controls.AddRange(new Control[] {
                lblTitulo, txtTitulo,
                lblAutor, txtAutor,
                lblPreco, txtPreco,
                lblCategoria, cmbCategoria,
                btnSalvar, btnCancelar
            });

            AcceptButton = btnSalvar;
            CancelButton = btnCancelar;
        }

        private void CarregarDados(Livro livro)
        {
            txtTitulo.Text = livro.Titulo;
            txtAutor.Text = livro.Autor;
            txtPreco.Text = livro.Preco.ToString("F2");
            cmbCategoria.SelectedItem = livro.Categoria;
        }

        private void ValidarESalvar(object sender, EventArgs e)
        {
            // Validações
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("O título é obrigatório.", "Validação", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitulo.Focus();
                DialogResult = DialogResult.None;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAutor.Text))
            {
                MessageBox.Show("O autor é obrigatório.", "Validação", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAutor.Focus();
                DialogResult = DialogResult.None;
                return;
            }

            if (!decimal.TryParse(txtPreco.Text, out decimal preco) || preco < 0)
            {
                MessageBox.Show("Digite um preço válido (ex: 29.90).", "Validação", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPreco.Focus();
                DialogResult = DialogResult.None;
                return;
            }

            if (cmbCategoria.SelectedItem == null)
            {
                MessageBox.Show("Selecione uma categoria.", "Validação", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategoria.Focus();
                DialogResult = DialogResult.None;
                return;
            }

            // Salvar dados
            Titulo = txtTitulo.Text.Trim();
            Autor = txtAutor.Text.Trim();
            Preco = preco;
            Categoria = cmbCategoria.SelectedItem.ToString();
        }
    }

    // PROGRAMA PRINCIPAL
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}