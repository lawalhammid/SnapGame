using Microsoft.Extensions.Logging;
using SnapGame.Configurations;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using BusinessLogic.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace SnapGame
{
    public class MainProgram
    {
        public static async void MainAsync(string[] args)
        {
            await InvokeGame();

            static async Task<string> InvokeGame()
            {
                //call our DI
                var serviceProvider = DI.RegisterDI();

                Console.WriteLine("Start playing Snap (card game) by entering the number of players: ");
                int numberOfPlayers = 0;
                if (int.TryParse(Console.ReadLine(), out numberOfPlayers))
                {
                    //validate no of player first
                    if (await serviceProvider.GetService<IValidatePlayer>().NoOfPlayers(numberOfPlayers) != 0)
                    {
                        //call this function again if the number of player does not match standards i.e 2 or max of 6 players
                        await InvokeGame();
                    }

                    var playGames = serviceProvider.GetService<IPlayGame>();
                    var returnWinner = await playGames.ReturnWinner(numberOfPlayers);

                    Console.WriteLine($"Rounding up game scores...");
                    Console.WriteLine($"The winner is {returnWinner.PlayerIdentity} with {returnWinner.TotalNumberOfCard} cards");

                    Console.WriteLine($"Enter 1 and press enter to play a new game. To end the game, press any other key and press enter key");
                    int continueGame = 0;
                    if (int.TryParse(Console.ReadLine(), out continueGame))
                    {
                        if(continueGame == 1)
                        {
                            await InvokeGame();
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Invalid input! Enter a valid number");
                    //call this function again if the input is not a valid one
                    await InvokeGame();
                }
                return string.Empty;
            }
        }
    }
}
