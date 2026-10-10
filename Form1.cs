using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            listele(libDB.Books.ToList());
        }

        LibraryDBEntities1 libDB = new LibraryDBEntities1();
        int selectedId;

        void listele(List<Books> book)
        {
            dataGridView1.DataSource = book.Select(b => new
            {
                b.Id,
                b.Title,
                Publisher = b.Publishers != null ? b.Publishers.PublisherName : "",
                Genre = b.Genres != null ? b.Genres.GenreName : "",
                // Since a book can have multiple authors, we can grab the first one or join them
                Author = b.Authors.FirstOrDefault() != null
                ? b.Authors.FirstOrDefault().authorName
                : "",
                b.Edition,
                b.Year,
                b.Price,
                b.Tags,
                b.Pages,
                b.Language
            })
            .ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int.TryParse(txtYear.Text, out int year);
            int.TryParse(txtPages.Text, out int pages);
            decimal.TryParse(txtPrice.Text, out decimal price);

            string publisherName = txtPublisher.Text.Trim();
            string genreName = txtGenre.Text.Trim();
            string authorName = txtAuthor.Text.Trim();

            Publishers publisher = libDB.Publishers
                .FirstOrDefault(p => p.PublisherName.ToLower() == publisherName.ToLower());

            if (publisher == null)
            {
                publisher = new Publishers { PublisherName = publisherName };
                libDB.Publishers.Add(publisher);
            }

            Genres genre = libDB.Genres
                .FirstOrDefault(g => g.GenreName.ToLower() == genreName.ToLower());

            if (genre == null)
            {
                genre = new Genres { GenreName = genreName };
                libDB.Genres.Add(genre);
            }

            Authors author = libDB.Authors
                .FirstOrDefault(a => (a.authorName).ToLower() == authorName.ToLower());

            if (author == null)
            {
                author = new Authors { authorName = authorName };
                libDB.Authors.Add(author);
            }

            Books book = new Books
            {
                Title = txtTitle.Text,
                Publishers = publisher,
                Genres = genre,
                Edition = txtEdition.Text,
                Year = year,
                Price = price,
                Tags = txtTags.Text,
                Pages = pages,
                Language = txtLang.Text
            };

            book.Authors.Add(author);

            libDB.Books.Add(book);

            libDB.SaveChanges();

            listele(libDB.Books.ToList());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Books book = libDB.Books.Find(selectedId);

            libDB.Books.Remove(book);
            libDB.SaveChanges();

            listele(libDB.Books.ToList());
        }

        // TODO
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Books book = libDB.Books.Find(selectedId);

            if (book == null)
            {
                MessageBox.Show("Select a book to update.");
                return;
            }

            int.TryParse(txtYear.Text, out int year);
            int.TryParse(txtPages.Text, out int pages);
            decimal.TryParse(txtPrice.Text, out decimal price);

            string title = txtTitle.Text.Trim();
            string authorName = txtAuthor.Text.Trim();
            string publisherName = txtPublisher.Text.Trim();
            string genreName = txtGenre.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(authorName) ||
                string.IsNullOrWhiteSpace(publisherName) ||
                string.IsNullOrWhiteSpace(genreName))
            {
                MessageBox.Show("Title, author, publisher, and genre are required.");
                return;
            }

            Publishers publisher = libDB.Publishers
                .FirstOrDefault(p => p.PublisherName.ToLower() == publisherName.ToLower());
            if (publisher == null)
            {
                publisher = new Publishers { PublisherName = publisherName };
                libDB.Publishers.Add(publisher);
            }

            Genres genre = libDB.Genres
                .FirstOrDefault(g => g.GenreName.ToLower() == genreName.ToLower());
            if (genre == null)
            {
                genre = new Genres { GenreName = genreName };
                libDB.Genres.Add(genre);
            }

            Authors author = libDB.Authors
                .FirstOrDefault(a => a.authorName.ToLower() == authorName.ToLower());
            if (author == null)
            {
                author = new Authors { authorName = authorName };
                libDB.Authors.Add(author);
            }

            book.Title = title;
            book.Publishers = publisher;
            book.Genres = genre;
            book.Edition = txtEdition.Text;
            book.Year = year;
            book.Price = price;
            book.Tags = txtTags.Text;
            book.Pages = pages;
            book.Language = txtLang.Text;
            book.Authors.Clear();
            book.Authors.Add(author);

            libDB.SaveChanges();

            listele(libDB.Books.ToList());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Books> book = libDB.Books.Where(x => x.Title.Contains(txtTitle.Text)).ToList();

            listele(book);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedId = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            txtTitle.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtAuthor.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtPublisher.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtEdition.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtYear.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtPrice.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txtGenre.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtTags.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            txtPages.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            txtLang.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
        }
    }
}
