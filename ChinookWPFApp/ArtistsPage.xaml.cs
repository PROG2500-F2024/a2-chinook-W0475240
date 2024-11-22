using ChinookWPFApp.Data;
using System;
using System.Collections.Generic;
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




namespace ChinookWPFApp
{
    public partial class ArtistsPage : Page
    {
        public ArtistsPage()
        {
            InitializeComponent();
            LoadArtists();
        }

        private void LoadArtists()
        {
            using (var context = new ChinookContext())
            {
                ArtistListView.ItemsSource = context.Artists.ToList();
            }
        }
    }
}




