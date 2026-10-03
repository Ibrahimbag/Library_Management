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

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
