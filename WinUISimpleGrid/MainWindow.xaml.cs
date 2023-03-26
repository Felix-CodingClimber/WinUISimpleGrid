using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;

namespace WinUISimpleGrid;

public sealed partial class MainWindow : Window
{
    private readonly ObservableCollection<ExampleDataItem> exampleItems;

    private readonly List<string> exampleNames = new List<string>()
    {
        "Lawrence",
        "Rodolfo",
        "Oswaldo",
        "Jayvion",
        "Braxton",
        "Austin",
        "Agustin",
        "Memphis",
        "Brenton",
        "Ernest",
        "Elias",
        "Leroy",
        "Bentley",
        "Anderson",
        "Kameron",
        "Ahmed",
        "Alexis",
        "Korbin",
        "Dereon",
        "Asa",
        "Freddy",
        "Bradyn",
        "Cristopher",
        "Stanley",
        "Kolby",
        "Ross"
    };

    private readonly List<string> exampleSurnames = new List<string>()
    {
        "Solis",
        "Rios",
        "Shaw",
        "Barrett",
        "Duke",
        "Hoffman",
        "Boone",
        "Paul",
        "Decker",
        "Vang",
        "Le",
        "Knapp",
        "Beltran",
        "Mason",
        "Simmons",
        "Woodward",
        "Carroll",
        "Valenzuela",
        "Harvey",
        "Mercado",
        "Shelton",
        "Horn",
        "Jacobson",
        "Cooper",
        "Tyler",
        "Hayes"
    };

    public MainWindow()
    {
        exampleItems = new ObservableCollection<ExampleDataItem>();

        Random random = new Random();
        int index = 0;
        for (int i = 0; i < 20; i++)
        {
            ExampleDataItem item = new ExampleDataItem()
            {
                Name = exampleNames[random.Next(exampleNames.Count)],
                Surname = exampleSurnames[random.Next(exampleSurnames.Count)],
                Age = index++
            };
            exampleItems.Add(item);
        }

        this.InitializeComponent();
    }
}
