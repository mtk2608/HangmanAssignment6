using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Windows.Input;


namespace HangmanAssignment;

public partial class HangmanGamePage : ContentPage, INotifyPropertyChanged
{
    public string Spotlight
    {
        get => spotlight;
        set
        {
            spotlight = value;
            OnPropertyChanged(nameof(Spotlight));
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




    //Fields
    List<string> words = new()
    {
        "python",
        "javascript",
        "maui",
        "csharp",
        "sql",
        "xaml",
        "word",
        "excel",
        "powerpoint",
        "code",
         "java",
         "javascript"

    };
    string answer = "";
    private string spotlight;
    private List<char> guessed = new();
    private List<char> letters = new();
    private string message;
    private int mistakes = 0;
    private int maxWrong = 8;
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
            return;
        }

        guessed.Add(letter);

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

        Letters.Remove(letter);
    }

    private void CheckIfGameWon()
    {
        if (Spotlight.Replace(" ", "") == answer)
        {
            Message = "You Win!!";
        }
    }

    private void CheckIfGameLost()
    {
        if (mistakes == maxWrong)
        {

            Message = "You Lost! Try again";

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

    private void UpdateStatus()
    {
        GameStatus = $"Error: {mistakes} of {maxWrong}";
    }


    //event Handlers
    private void Guess_Clicked(object sender, EventArgs e)
    {

        string letter = GuessBox.Text;
        HandleGuess(letter[0]);
        GuessBox.Text = string.Empty;

    }

    private void Reset_Clicked(object sender, EventArgs e)
    {
        ResetGame();
    }
} 