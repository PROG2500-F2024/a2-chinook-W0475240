using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Linq;


namespace ChinookWPFApp
{
    public partial class MusicCatalogPage : Page
    {
        public ICollectionView GroupedArtists { get; set; }

        public MusicCatalogPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new ChinookContext())
            {
                var groupedData = context.Artists
                    .OrderBy(a => a.Name)
                    .GroupBy(a => a.Name[0].ToString().ToUpper())
                    .Select(group => new ArtistGroup
                    {
                        Key = group.Key,
                        Artists = group.Select(artist => new Artist
                        {
                            Name = artist.Name,
                            Albums = artist.Albums.Select(album => new Album
                            {
                                Title = album.Title,
                                Tracks = album.Tracks.Select(track => new Track
                                {
                                    Title = track.Name
                                }).ToList()
                            }).ToList()
                        }).ToList()
                    }).ToList();

                GroupedArtists = new CollectionViewSource { Source = groupedData }.View;
                DataContext = this;
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = SearchBox.Text.ToLower();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                GroupedArtists.Filter = item =>
                {
                    var group = item as ArtistGroup;
                    return group.Artists.Any(artist =>
                        artist.Name.ToLower().Contains(searchTerm));
                };
            }
            else
            {
                GroupedArtists.Filter = null;
            }
        }
    }

    public class ArtistGroup
    {
        public string Key { get; set; }
        public List<Artist> Artists { get; set; }
    }

    public class Artist
    {
        public string Name { get; set; }
        public List<Album> Albums { get; set; }
    }

    public class Album
    {
        public string Title { get; set; }
        public List<Track> Tracks { get; set; }
    }

    public class Track
    {
        public string Title { get; set; }
    }
}

