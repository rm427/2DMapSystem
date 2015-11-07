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

using Engine;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double TileWidth;
        double TileHeight; /// This and TileWidth are set in the CreateGrid() function.
        int GridWidth = 11; ///Number of tiles 'wide' the grid is
        int GridHeight = 11;
        /// Keep GridWidth and GridHeight in the same proportions as GridPixelWdith & GPH.
        /// Otherwise the tiles will not be square.
        int XDimension = 999;
        int YDimension = 999;
        int LeftGrid = 150; ///The leftmost pixel of the grid
        int TopGrid = 75; /// The bottom pixel of the grid
        int GridPixelWidth = 600; /// Should be GridWidth*TileWidth
        int GridPixelHeight = 600;
        Images ImageInstance = new Images();


        Location[,] DisplayedTiles = new Location[10, 10];
        Image[,] Box = new Image[65, 65];
        Image[,] ObjectBox = new Image[65, 65];

        Rectangle[] BoundRectangle = new Rectangle[5];

        /// Generate player
        Player ThePlayer = new Player(10, 10, 100, "PlayerImage", 15, 15);
        Image PlayerSprite = new Image();

        public MainWindow()
        {
            InitializeComponent();

            DisplayedTiles = new Location[XDimension, YDimension];
            DisplayedTiles = GenerateTileArray();

            /// Create player sprite
            MainCanvas.Children.Add(PlayerSprite = new Image { Height = TileHeight, Width = TileWidth });
            Canvas.SetLeft(PlayerSprite, LeftGrid + (ThePlayer.XRef * TileWidth));
            Canvas.SetTop(PlayerSprite, TopGrid + (ThePlayer.YRef * TileHeight));
            PlayerSprite.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Player/Player.png", UriKind.Absolute));

            CreateGrid(GridWidth, GridHeight);
            UpdateGridCentre(ThePlayer.XRef, ThePlayer.XRef, ThePlayer.YRef, ThePlayer.YRef);
        }


        public Location[,] GenerateTileArray()
        {

            /// Declare a Location class array of length Dimension*Dimension
            Location[,] LocArray = new Location[XDimension, YDimension];

            /// Generate each tile
            for (int x = 0; x < XDimension; x = x + 1)
            {
                for (int y = 0; y < YDimension; y = y + 1)
                {
                    int z = x * XDimension + y;   /// ID calculator
                    Location location = new Location(z, x, y, "grass", "");
                    LocArray[x, y] = location;
                    string String = "ID: " + z + " Coord: " + location.XRef + "," + location.YRef;
                }
            }
            LocArray[1, 4].ObjectType = "house7";
            LocArray[2, 4].ObjectType = "house8";
            LocArray[3, 4].ObjectType = "house9";
            LocArray[1, 3].ObjectType = "house4";
            LocArray[2, 3].ObjectType = "house5";
            LocArray[3, 3].ObjectType = "house6";
            LocArray[1, 2].ObjectType = "house1";
            LocArray[2, 2].ObjectType = "house2";
            LocArray[3, 2].ObjectType = "house3";

            return LocArray;
        }


        public void UpdateCoords()
        {
            int X = ThePlayer.XRef;
            int Y = ThePlayer.YRef;
            CoordsLabel.Content = X + ", " + Y;
            /// CoOrdLabel.Text = X + ", " + Y;
        }

        public void UpdatePlayerSprite(int oldx, int newx, int oldy, int newy)
        {
            int OldX = oldx; int NewX = newx; int OldY = oldy; int NewY = newy;
            /// For some reason there's a thick 'outline' of green at the top when zoomed in you can't walk on.
            ThePlayer.XRef = NewX;
            ThePlayer.YRef = NewY;

            int GridX = ThePlayer.XRef % GridWidth;
            int GridY = ThePlayer.YRef % GridHeight;

            /// PlayerSprite.Location = new Point(100 + (GridX * TileWidth), TopGrid - (GridY * TileHeight));
            Canvas.SetLeft(PlayerSprite, LeftGrid + (GridX * TileWidth));
            Canvas.SetTop(PlayerSprite, TopGrid + ((GridHeight - 1 - GridY) * TileHeight));
            Canvas.SetZIndex(PlayerSprite, 2);
            PlayerSprite.Width = TileWidth;
            PlayerSprite.Height = TileHeight;

        }

        public void CreateGrid(int gridwidth, int gridheight)
        {
        /// Step 1: Clear any tiles & objects that need to be cleared.
        /// Step 2: Set the dimensions of the tiles.
        /// Step 3: Add the needed tile & object boxes (Should they be added to the canvas? Is that a potential leak??)
        /// Step 4: Add the rectangles around the grid
        /// Step 5: Run the grid update.
            for (int x = 0; x < 64; x = x + 1)
            {
                for (int y = 0; y < 64; y = y + 1)
                {
                    if (Box[x,y] != null)
                    {
                        Box[x, y].Source = null;
                    }
                    if (ObjectBox[x, y] != null)
                    {
                        ObjectBox[x, y].Source = null;
                    }
                }
            }

            GridWidth = gridwidth;
            GridHeight = gridheight;
            int GridX = ThePlayer.XRef % GridWidth;
            int GridY = ThePlayer.YRef % GridHeight;
            int i = ThePlayer.XRef - GridX;
            int j = ThePlayer.YRef - GridY;

            /// Need this stuff so TitleWidth and TitleHeight can hold decimals.
            double GPW = GridPixelWidth; double GPH = GridPixelHeight;
            double GW = GridWidth; double GH = GridHeight;
            TileWidth = GPW / GW; ///So like 720/24
            TileHeight = GPH / GH; 

            /// For some reason the grid will still fluctuate in size.
            /// Best to put some images over the edges of the grid so you can't see it.
            /// CoordsLabel.Content = TileWidth + ", " + TileHeight;


            for (int x = 0; x < GridWidth; x = x + 1)
            {
                for (int y = 0; y < GridHeight; y = y + 1)
                {
                    int k = (GridHeight - 1 - y);
                    MainCanvas.Children.Add(Box[x, y] = new Image { Height = TileHeight, Width = TileWidth });
                    Canvas.SetLeft(Box[x, y], LeftGrid + (x * (TileWidth - 1)));
                    Canvas.SetTop(Box[x, y], TopGrid + (k * (TileHeight - 1)));

                    MainCanvas.Children.Add(ObjectBox[x, y] = new Image { Height = TileHeight, Width = TileWidth });
                    Canvas.SetLeft(ObjectBox[x, y], LeftGrid + (x * (TileWidth-1)));
                    Canvas.SetTop(ObjectBox[x, y], TopGrid + (k * (TileHeight-1)));
                }
            }

            AddRectangle(1, 50, GridPixelHeight, LeftGrid, TopGrid);
            AddRectangle(2, 50, GridPixelHeight, GridPixelWidth + LeftGrid-50, TopGrid);
            AddRectangle(3, GridPixelWidth, 50, LeftGrid, TopGrid);
            AddRectangle(4, GridPixelWidth, 50, LeftGrid, GridPixelHeight + TopGrid-50);

            UpdateGridCentre(ThePlayer.XRef, ThePlayer.XRef, ThePlayer.YRef, ThePlayer.YRef);
        }

        public void AddRectangle(int ID, int width, int height, int TopX, int TopY)
        {
            MainCanvas.Children.Add(BoundRectangle[ID] = new Rectangle { Height = height, Width = width });
            Canvas.SetLeft(BoundRectangle[ID], TopX);
            Canvas.SetTop(BoundRectangle[ID], TopY);
            BoundRectangle[ID].Fill = new SolidColorBrush(System.Windows.Media.Colors.Gray);
            Canvas.SetZIndex(BoundRectangle[ID], 3);
            /// This will be Z of 3, ahead of player (2), object (1), tile (0).
        }

        public void UpdateGridCentre(int oldx, int newx, int oldy, int newy)
        {
            /// Step 1: Loop, setting tile and object displays for the grid.
            /// Step 2: Adjust the player sprite accordingly.
            ThePlayer.XRef = newx;
            ThePlayer.YRef = newy;

            int i = ThePlayer.XRef;
            int j = ThePlayer.YRef;
            for (int x = 0; x < GridWidth; x = x + 1) /// 9 instead of 10 so we have a middle cell.
            {
                for (int y = 0; y < GridHeight; y = y + 1)
                {
                    Box[x, y].Source = null;
                    ObjectBox[x, y].Source = null;
                    int m = i - ((GridWidth - 1) / 2) + x;
                    int n = j - ((GridHeight - 1) / 2) + y;

                    if ((m>= 0 & m<XDimension) & (n>=0 & n< YDimension))
                    {
                        if (DisplayedTiles[m, n].TileType != "")
                        {
                            Box[x, y].Source = new BitmapImage(new Uri(ImageInstance.UpdateDisplayedTile(DisplayedTiles[m, n].TileType), UriKind.Absolute));
                        }
                        if (DisplayedTiles[m, n].ObjectType != "")
                        {
                            ObjectBox[x, y].Source = new BitmapImage(new Uri(ImageInstance.UpdateImage(DisplayedTiles[m, n].ObjectType), UriKind.Absolute));
                            Canvas.SetZIndex(ObjectBox[x, y], 1);
                        }
                    }
                    else
                    {
                        Box[x, y].Source = new BitmapImage(new Uri(ImageInstance.UpdateDisplayedTile("empty"), UriKind.Absolute));
                    }



                }
            }
            Canvas.SetLeft(PlayerSprite, LeftGrid + (((GridWidth - 1) / 2) * (TileWidth - 1)));
            Canvas.SetTop(PlayerSprite, TopGrid + (((GridHeight - 1) / 2) * (TileHeight - 1)));
            Canvas.SetZIndex(PlayerSprite, 2);
            PlayerSprite.Width = TileWidth;
            PlayerSprite.Height = TileHeight;

        }



        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string str = e.Parameter as string;
            switch (str)
            {
                case null:
                    throw new Exception("Up");
                case "Up":
                    if (ThePlayer.YRef < YDimension - 1)
                    {
                        int OldX = ThePlayer.XRef;
                        int NewX = ThePlayer.XRef;
                        int OldY = ThePlayer.YRef;
                        int NewY = ThePlayer.YRef + 1;
                        UpdateGridCentre(OldX, NewX, OldY, NewY);
                        UpdateCoords();
                    }
                    break;
                case "Down":
                    if (ThePlayer.YRef > 0)
                    {
                        int OldX = ThePlayer.XRef;
                        int NewX = ThePlayer.XRef;
                        int OldY = ThePlayer.YRef;
                        int NewY = ThePlayer.YRef-1;
                        UpdateGridCentre(OldX, NewX, OldY, NewY);
                        UpdateCoords();
                    }
                    break;
                case "Right":
                    if (ThePlayer.XRef < XDimension - 1)
                    {
                        int OldX = ThePlayer.XRef;
                        int NewX = ThePlayer.XRef+1;
                        int OldY = ThePlayer.YRef;
                        int NewY = ThePlayer.YRef;
                        UpdateGridCentre(OldX, NewX, OldY, NewY);
                        UpdateCoords();
                    }
                    break;
                case "Left":
                    if (ThePlayer.XRef > 0)
                    {
                        int OldX = ThePlayer.XRef;
                        int NewX = ThePlayer.XRef-1;
                        int OldY = ThePlayer.YRef;
                        int NewY = ThePlayer.YRef;
                        UpdateGridCentre(OldX, NewX, OldY, NewY);
                        UpdateCoords();
                    }
                    break;
                case "Add":
                    if (GridWidth - 2 > 0 & GridHeight - 2 > 0)
                    {
                        CreateGrid(GridWidth - 2, GridHeight - 2);
                    }
                    break;
                case "Subtract":
                    if (GridWidth + 2 < 32 & GridHeight + 2 < 32)
                    {
                        CreateGrid(GridWidth + 2, GridHeight + 2);
                    }
                    break;
                case "Case7":
                    //Code
                    break;
            }
            e.Handled = true;
        }

    }
}
