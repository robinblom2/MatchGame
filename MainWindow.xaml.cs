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

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()                                                             // Konstruktor, allt som ligger i konstruktorn körs så fort programmet körs. 
        {
            InitializeComponent();
            SetUpGame();                                                                // Direkt vid start av programmet anropas metoden SetUpGame().

        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()                               // Vi skapar en lista av typen string. Vi tilldelar sedan listan 8 par av olika emojis. 
            {
                "🐶", "🐶",
                "🐵", "🐵",
                "🦊", "🦊",
                "🦄", "🦄",
                "🐷", "🐷",
                "🦔", "🦔",
                "🐔", "🐔",
                "🐍", "🐍",
            };

            Random random = new Random();                                               // Vi skapar en slump generator. 

            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())       // För varje Textblock i Main-grid, upprepa följande...
            {
                int index = random.Next(animalEmoji.Count);                             // Hitta ett slumpat nummer mellan 0 och antalet emojis som är kvar i listan och ge "index" detta värde. 
                string nextEmoji = animalEmoji[index];                                  // Använd det utvalda slumpade värdet i "index" för att få fram en slumpad emoji från listan. Lagra den slumpade emojin i "nextEmoji". 
                textBlock.Text = nextEmoji;                                             // Uppdatera TextBlock med den slumpade emojin.
                animalEmoji.RemoveAt(index);                                            // Ta bort den slumpade emojin från listan "animalEmoji". 
            }



        }
    }
}
