using LINQ;
using System.Text.RegularExpressions;


var games = new List<Games>

{

    new Games { Title = "The Legend of Zelda: Breath of the Wild", Genre = "Action-adventure", ReleaseYear = 2017, Rating = 9.5, Price = 59 },

    new Games { Title = "God of War", Genre = "Action-adventure", ReleaseYear = 2018, Rating = 9.3, Price = 49 },

    new Games { Title = "Red Dead Redemption 2", Genre = "Action-adventure", ReleaseYear = 2018, Rating = 9.7, Price = 69 },

    new Games { Title = "The Witcher 3: Wild Hunt", Genre = "RPG", ReleaseYear = 2015, Rating = 9.4, Price = 39 },

    new Games { Title = "Minecraft", Genre = "Sandbox", ReleaseYear = 2011, Rating = 9.0, Price = 26 },

    new Games { Title = "Fortnite", Genre = "Battle Royale", ReleaseYear = 2017, Rating = 8.5, Price = 0 },

    new Games { Title = "Among Us", Genre = "Party", ReleaseYear = 2018, Rating = 8.0, Price = 5 },

    new Games { Title = "Cyberpunk 2077", Genre = "RPG", ReleaseYear = 2020, Rating = 7.5, Price = 59 },

    new Games { Title = "Hades", Genre = "Roguelike", ReleaseYear = 2020, Rating = 9.2, Price = 24 },

    new Games { Title = "Animal Crossing: New Horizons", Genre = "Simulation", ReleaseYear = 2020, Rating = 9.1, Price = 59 }

};



//List<string> allGames = new List<string>();

//foreach (var game in games)
//{
//    allGames.Add(game.Title);
//}

//foreach (var title in allGames)
//{
//    Console.WriteLine(title);
//}

//to find the all data 
//var allGames = games.Select(n => n.Title);

//foreach (var title in allGames)
//{
//    Console.WriteLine(title);
//}

//to find perticular statement

//var Genregames = games.Where(games => games.Genre == "RPG");

//foreach (var item in Genregames)
//{
//    Console.WriteLine(item.Title);
//}

//true false check
// " ANY operation is used for true of false"

//var moderngames = games.Any(game => game.ReleaseYear >= 2025);
//Console.WriteLine(moderngames);


//order function
//orderby
//var Genregames = games.OrderByDescending(games => games.ReleaseYear);

//foreach (var item in Genregames)
//{
//    Console.WriteLine($"{item.Title} -- {item.ReleaseYear}");
//}


//Aggregate Function
//var avgprice = games.Aggregate(games => games.Price);
//Console.WriteLine($"avg game price:{avgprice}");


//var mingameprice = games.Min(games => games.Price);
//Console.WriteLine($"min games prices {mingameprice}");

//var first = games.First(games => games.Rating == mingameprice);
//Console.WriteLine($"{first:Rating}");


//groupby---------------------------------------------------
//var groupbyup = games.GroupBy(g=>g.Title);
//foreach (var group in groupbyup)
//{
//    Console.WriteLine($"genre -- {group.Key}");
//    foreach(var game in group)
//    {
//        Console.WriteLine($"--{game.Title}");
//    }
//}

var multicondition = games.Where(games => games.Genre == "RPG")
    .OrderByDescending(games => games.ReleaseYear);

foreach (var game in multicondition)
{
    Console.WriteLine(game);
}



