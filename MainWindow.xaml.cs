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

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        public void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)         // Metoden anropas vid musklick. Den tar två in-parametrar. Dels emojin användaren klickar på. Den andra parametern är för själva musklicket. 
        {
            TextBlock textBlock = sender as TextBlock;                                  // Den emoji som användaren klickar på hamnar i "textBlock". 
            if (findingMatch == false)                                                  // OM findingMatch är false (om användaren ej har matchat två emojis). Så gör det här... 
            {
                textBlock.Visibility = Visibility.Hidden;                               // Sätt emojin som användaren klickade på till osynlig (och oklickbar). 
                lastTextBlockClicked = textBlock;                                       // Vi lagrar emojin som användaren klickade på i variabeln "lastTextBlockClicked" (ifall den behöver visas igen, om nästa emoji användaren klickar på inte är samma).
                findingMatch = true;                                                    // Sätt sedan vår bool "findingMatch" till true. Detta gör att vid nästa musklick hoppar programmet över IF-satsen och går direkt till else if-satsen istället. 
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)                       // Programmet kontrollerar om den första emojin användaren klickade på är samma som den emoji användaren nyss klickade på..     (MATCHNING)
            {
                textBlock.Visibility = Visibility.Hidden;                               // Sätt den senaste klickade emojin till osynlig (och oklickbar).
                findingMatch = false;                                                   // Eftersom vi nu har hittat en matchning ska vår bool återgå till false, så att programmet i nästa varv går in i IF-satsen ovan. 
            }
            else                                                                        // ANNARS om användaren klickar på en emoji som ej matchar den första emojin...
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;                   // Programmet sätter den första klickade emojin till att bli synlig igen (så att det går att klicka på den igen under nästa runda).
                findingMatch = false;                                                   // Vi sätter vår bool "findingMatch" till false, så att programmet under nästa runda går in i IF-satsen ovan. 
            }


        }
    }
}
