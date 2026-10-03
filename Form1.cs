using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        LibraryDBEntities libDB = new LibraryDBEntities();
        int selectedId;

        void listele()
        {
            dataGridView1.DataSource = libDB.Books.ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Books book = new Books
            {
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                Publisher = txtPublisher.Text,
                Edition = txtEdition.Text,
                Year = txtYear.Text,
                Price = txtPrice.Text,
                Genre = txtGenre.Text,
                Tags = txtTags.Text,
                Pages = txtPages.Text,
                Language = txtLang.Text,
            };

            libDB.Books.Add(book);
            libDB.SaveChanges();

            listele();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Books book = libDB.Books.Find(selectedId);
            libDB.Books.Remove(book);
            libDB.SaveChanges();

            listele();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Books book = libDB.Books.Find(selectedId);

            book.Title = txtTitle.Text;
            book.Author = txtAuthor.Text;
            book.Publisher = txtPublisher.Text;
            book.Edition = txtEdition.Text;
            book.Year = txtYear.Text;
            book.Price = txtPrice.Text;
            book.Genre = txtGenre.Text;
            book.Tags = txtTags.Text;
            book.Pages = txtPages.Text;
            book.Language = txtLang.Text;

            libDB.SaveChanges();

            listele();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Books> books = new List<Books> { };

            foreach (var book in libDB.Books)
            {
                if (
                    book.Title == txtTitle.Text ||
                    book.Author == txtAuthor.Text ||
                    book.Publisher == txtPublisher.Text ||
                    book.Edition == txtEdition.Text ||
                    book.Year == txtYear.Text ||
                    book.Price == txtPrice.Text ||
                    book.Genre == txtGenre.Text ||
                    book.Tags == txtTags.Text ||
                    book.Pages == txtPages.Text ||
                    book.Language == txtLang.Text
                )
                {
                    books.Add(book);
                }
            }

            if (books.Count > 0)
            {
                dataGridView1.DataSource = books;
            }
            else
            {
                listele();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedId = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            Books book = libDB.Books.Find(selectedId);

            txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author;
            txtPublisher.Text = book.Publisher;
            txtEdition.Text = book.Edition;
            txtYear.Text = book.Year;
            txtPrice.Text = book.Price;
            txtGenre.Text = book.Genre;
            txtTags.Text = book.Tags;
            txtPages.Text = book.Pages;
            txtLang.Text = book.Language;
        }
    }
}
