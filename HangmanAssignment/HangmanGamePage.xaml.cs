using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Windows.Input;


namespace HangmanAssignment;

public partial class HangmanGamePage : ContentPage, INotifyPropertyChanged
{
    //Properties
    public string Spotlight
    {
        get => spotlight;
        set
        {
            spotlight = value;
            OnPropertyChanged();
        }
    }

    public List<char> Letters
    {
        get => letters;
        set
        {
            letters = value;
            OnPropertyChanged();
        }
    }

    public string Message
    {
        get => message;
        set
        {
            message = value;
            OnPropertyChanged();
        }
    }

    public string GameStatus
    {
        get => gameStatus;
        set
        {
            gameStatus = value;
            OnPropertyChanged();
        }
    }

    public string CurrentImage
    {
        get => currentImage;
        set
        {
            currentImage = value;
            OnPropertyChanged();
        }
    }

    public ICommand GuessCommand => new Command<char>(HandleGuess);



    //Fields

    List<string> words = new()
        {
            "python",
            "javascript",
            "maui",
            "csharp",
            "mongodb",
            "sql",
            "xaml",
            "word",
            "excel",
            "powerpoint",
            "code",
            "hotreload",
            "snippets",
            "itna",
            "ctmehr",
            "pooyesh"
        };
    string answer = "";
    private string spotlight;
    private List<char> guessed = new();
    private List<char> letters = new();
    private string message;
    private int mistakes = 0;
    private int maxWrong = 7;
    private string gameStatus;
    private string currentImage = "hang1.png";


    //Constructor

    public HangmanGamePage()
    {
        InitializeComponent();
        BindingContext = this;
        PickWord();
        CalculateWord(answer, guessed);
    }


    //GameEngine

    private void PickWord()
    {
        answer =
            words[new Random().Next(0, words.Count)];
    }

    private void CalculateWord(string answer, List<char> guessed)
    {
        var temp =
            answer.Select(x => (guessed.IndexOf(x) >= 0 ? x : '_'))
            .ToArray();

        Spotlight = string.Join(" ", temp);
    }

    private void HandleGuess(char letter)
    {
        if (guessed.Contains(letter))
        {
            guessed.Add(letter);
            Letters.Remove(letter);
        }

        if (answer.Contains(letter))
        {
            CalculateWord(answer, guessed);
            CheckIfGameWon();
        }
        else
        {
            mistakes++;
            UpdateStatus();
            CheckIfGameLost();
            CurrentImage = $"hang{mistakes}.png";
        }

    }

    private void CheckIfGameWon()
    {
        if (Spotlight.Replace(" ", "") == answer)
        {
            Message = "You Win";
        }
    }

    private void CheckIfGameLost()
    {
        if (mistakes == maxWrong)
        {
            Message = "You Lost!!";
        }
    }

    private void ResetGame()
    {
        mistakes = 0;
        guessed.Clear();
        PickWord();
        CalculateWord(answer, guessed);
        UpdateStatus();
        CurrentImage = "hang1.png";
        Message = "";
    }

    /*rivate void DisableLetters()
     {
         foreach (var child in LettersContainer.Children)
         {
             var btn = child as Button;
             if (btn != null)
                 btn.IsEnabled = false;
         }
     }

     private void EnableLetters()
     {
         foreach (var child in LettersContainer.Children)
         {
             var btn = child as Button;
             if (btn != null)
                 btn.IsEnabled = true;
         }
     }*/



    private void UpdateStatus()
    {
        GameStatus = $"Error: {mistakes} of {maxWrong}";
    }

    //event Handlers
    private void Guess_Clicked(object sender, EventArgs e)
    {

            string letter = GuessBox.Text;
            HandleGuess(letter[0]);

    }

    private void Reset_Clicked(object sender, EventArgs e)
    {
        ResetGame();
    }
} 