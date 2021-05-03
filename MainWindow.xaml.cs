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
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();                                  // Vi lägger till en timer som ska börja när spelet startar, och sluta när sista djuret är matchat. 
        int tenthsOfSecondsElapsed;                                                     // Denna variabel håller koll på hur lång tid som gått.
        int matchesFound;                                                               // Denna variabel håller koll på hur många matchningar användaren gjort.

        public MainWindow()                                                             // Konstruktor, allt som ligger i konstruktorn körs så fort programmet körs. 
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();                                                                // Direkt vid start av programmet anropas metoden SetUpGame().

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()                                                        // Denna metod ansvarar för att förbereda spelet. Den tilldelar varje textBlock en emoji. 
        {
            List<string> animalEmoji = new List<string>()                                   // Metoden skapar en lista med 16 emojis. Med hälp av en slumpgenerator slumpas en emoji fram och hamnar på ett slumpat textBlock. 
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

            Random random = new Random();                                                   // Vi skapar en slump generator. 

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())          // För varje Textblock i Main-grid, upprepa följande...
            {
                if (textBlock.Name != "timeTextBlock")                                      // Vi lägger till en IF-sats i foreach-loopen som går igenom listan med alla emojis, annars får vi OutOfRange Exception pga av timer-textBlock. (17 textBlock, bara 16 emojis). 
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);                             // Hitta ett slumpat nummer mellan 0 och antalet emojis som är kvar i listan och ge "index" detta värde. 
                    string nextEmoji = animalEmoji[index];                                  // Använd det utvalda slumpade värdet i "index" för att få fram en slumpad emoji från listan. Lagra den slumpade emojin i "nextEmoji". 
                    textBlock.Text = nextEmoji;                                             // Uppdatera TextBlock med den slumpade emojin.
                    animalEmoji.RemoveAt(index);                                            // Ta bort den slumpade emojin från listan "animalEmoji". 
                }
            }

            timer.Start();                                                                  // Timern startar direkt när programmet körs, eftersom anropet ligger i SetUpGame(). 
            tenthsOfSecondsElapsed = 0;                                                     // Vi anger att timern ska börja på 0 sekunder, och att 0 matchningar är funna vid start. 
            matchesFound = 0;                                                               // Eftersom detta ligger här, så startas timern om efter varje runda. 

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
                matchesFound++;                                                         // Vid varje matchning av emojis så ökar värdet på "matchesFound" med 1. 
                textBlock.Visibility = Visibility.Hidden;                               // Sätt den senaste klickade emojin till osynlig (och oklickbar).
                findingMatch = false;                                                   // Eftersom vi nu har hittat en matchning ska vår bool återgå till false, så att programmet i nästa varv går in i IF-satsen ovan. 
                
            }
            else                                                                        // ANNARS om användaren klickar på en emoji som ej matchar den första emojin...
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;                   // Programmet sätter den första klickade emojin till att bli synlig igen (så att det går att klicka på den igen under nästa runda).
                findingMatch = false;                                                   // Vi sätter vår bool "findingMatch" till false, så att programmet under nästa runda går in i IF-satsen ovan. 
            }


        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)                                                      // OM användaren har hittat 8 matchningar... 
            {
                SetUpGame();                                                            // Så starar spelet om (och timern startar om).
            }
        }
    }
}
