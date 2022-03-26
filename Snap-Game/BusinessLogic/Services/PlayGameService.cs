using BusinessLogic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using BusinessLogic.ViewModels;

namespace BusinessLogic.Services
{
    public class PlayGameService : IPlayGame
    {
        IReturnCardsObjects _iReturnCardsObjects;
        private const string NameToDisplayforEachPlayer = "Player";
        public PlayGameService(IReturnCardsObjects iReturnCardsObjects)
        {
            _iReturnCardsObjects = iReturnCardsObjects;
        }

        //Create players names and id
        public async Task<List<Player>> GeneratePlayersIdentities(int numberOfPlayers)
        {
            var Players = new List<Player>();

           
            for (int i = 0; i < numberOfPlayers; i++)
            {

                Players.Add(new Player
                {
                    PlayerIdentity = $"{NameToDisplayforEachPlayer} { 1 + i }"
                });
            }

            return Players;
        }

        //Assign Cards to Players
        public async Task<List<Player>> AssignNoCardsToPlayers(List<Player> Players)
        {
            Console.WriteLine($"Kindly wait... The system is assigning cards to all Players \n");

            var assignNoCardsToPlayers = new List<Player>();
           
            var returnCards = await _iReturnCardsObjects.AllCards();

            Random rng = new Random();
            //The cards need to be reshuffle before playing
            var cardsList = returnCards.OrderBy(a => rng.Next()).ToList();

            int indexOfThePers = 0; // this isto get the next player to assign cards to.
            foreach (var card in cardsList)
            {
                //getting the player to assign the card to in a lists of players in a clockwise direction
                indexOfThePers = Players.ElementAtOrDefault(indexOfThePers) == null ? 0 : indexOfThePers;

                var getCardDetails = cardsList.FirstOrDefault(c => c.CardNo == card.CardNo);

                assignNoCardsToPlayers.Add(new Player
                {
                    CardNo = getCardDetails.CardNo,
                    PlayerIdentity = Players[indexOfThePers].PlayerIdentity,
                    CardDetails = getCardDetails != null ? $" Card No { getCardDetails.CardNo } Card Letter: {getCardDetails.LetterOnCard} Card Image: {getCardDetails.CardIconOrImage}" : String.Empty,
                    LetterOnCard = getCardDetails.LetterOnCard,
                    CardIconOrImage = getCardDetails.CardIconOrImage,
                });

                // select next player in a clockwise direction
                indexOfThePers++;
            }

            return assignNoCardsToPlayers;
        }

        //Display all cards that was assigned to players into the screen in hidden form
        public async Task<string> DisplayAllCardsToScreenInHiddenform(List<Player> assignNoCardsToPlayers)
        {
            var CardslistDisplay = assignNoCardsToPlayers.OrderBy(c => c.PlayerIdentity).ToList();

            /*Loop CardslistDisplay display all cards to screen for player to play 
              with its id to reveal what is there */
            string seperateString = string.Empty;
            foreach (var assignNoCardsToPlayer in CardslistDisplay)
            {
                if (seperateString != assignNoCardsToPlayer.PlayerIdentity)
                {
                    Console.WriteLine($"----Cards assigned to {assignNoCardsToPlayer.PlayerIdentity} and cards remaining for {assignNoCardsToPlayer.PlayerIdentity} to play are:");
                }
                seperateString = assignNoCardsToPlayer.PlayerIdentity;
                //print 
                Console.WriteLine($"{ assignNoCardsToPlayer.CardNo }");
            }

            return String.Empty;
        }

        public async Task<List<SnapChatPlayer>> PlayerPlaying(List<Player> assignNoCardsToPlayers, 
                                                            List<Player> Players)
        {
            var PlayingPool = new List<PlayerPool>();

            //Below is used for Snap Pool i.e player that has snaps
            var resultOfPlayerWithSnaps = new List<SnapChatPlayer>();

            foreach (var getCarddetails in assignNoCardsToPlayers)
            {

                Console.WriteLine($"{getCarddetails.PlayerIdentity}, Please enter { getCarddetails.CardNo } and press the enter key to reveal the first card at the top of your cards stack. Note: { getCarddetails.CardNo } is the Card No at the top of your cards stack");

                int cardNoTyped = getCarddetails.CardNo;
               

                if (int.TryParse(Console.ReadLine(), out cardNoTyped))
                {

                    //If user did not enetered the first card at the top do below
                    if(cardNoTyped != getCarddetails.CardNo)
                    {
                        Console.WriteLine($"{getCarddetails.PlayerIdentity}, kindly know that the first card in your slack is  {getCarddetails.CardNo} i.e the first Card No to reveal the  card details. Please enter this number");

                        await PlayerPlaying(assignNoCardsToPlayers, Players);
                        
                    }

                    //Console.WriteLine($"The Card details you just selected is { getCarddetails.CardDetails }");

                    Console.WriteLine($"The card you selected is Card No. {getCarddetails.CardNo}. The letter on it is {getCarddetails.LetterOnCard}, and its image is {getCarddetails.CardIconOrImage}");

                    PlayingPool.Add(new PlayerPool
                    {
                        CardDetails = getCarddetails.CardDetails,
                        PlayerIdentity = getCarddetails.PlayerIdentity,
                        LetterOnCard = getCarddetails.LetterOnCard
                    });

                    //update the listOfCardbyReducing it from what that has been played
                    assignNoCardsToPlayers = assignNoCardsToPlayers.Where(c=> c.CardNo != cardNoTyped).ToList();   

                    //to determine snap using the letter on the card
                    var getCardExist = PlayingPool.Where(c => c.LetterOnCard == getCarddetails.LetterOnCard).ToList();

                    if (getCardExist.Count == 2)
                    {
                        //get player that shouted Snap randomly
                        Random rndPersonShoutedSnap = new Random();

                        var ranPlayer = rndPersonShoutedSnap.Next(0, getCardExist.Count);

                        var playerThatShoutedSnap = Players[ranPlayer];
                        /*  update resultOfPlayerWithSnaps with all the existing cards getCardExist and
                            add it thelist person winning for update assignNoCardsToPlayers
                         */
                        var PlayerExisitInresultOfPlayerWithSnaps = resultOfPlayerWithSnaps.Where(c => c.PlayerIdentity == playerThatShoutedSnap.PlayerIdentity).ToList();
                       
                        if (PlayerExisitInresultOfPlayerWithSnaps.Count == 0)
                        {
                            resultOfPlayerWithSnaps.Add(new SnapChatPlayer
                            {
                                PlayerIdentity = playerThatShoutedSnap.PlayerIdentity,
                                TotalNumberOfCard = getCardExist.Count()
                            });
                        }
                        else
                        {
                            var obj = resultOfPlayerWithSnaps.FirstOrDefault(x => x.PlayerIdentity == playerThatShoutedSnap.PlayerIdentity);

                            if (obj != null)
                            {

                                obj.TotalNumberOfCard = PlayerExisitInresultOfPlayerWithSnaps.Count + getCardExist.Count();
                            }
                        }

                        Console.WriteLine("Snap!");
                        Console.WriteLine($"{playerThatShoutedSnap.PlayerIdentity} just won {getCardExist.Count()} cards!");
                    }
                    //update the screen with the cards that remains
                    // the figure 2 was used below to tell since second player  has played not untill all players played 
                    if(PlayingPool.Count() % 2 == 0)
                    await DisplayAllCardsToScreenInHiddenform(assignNoCardsToPlayers);

                    if (assignNoCardsToPlayers.Count == 0)
                        break;

                }
            }

            return resultOfPlayerWithSnaps;
        }

        public async Task<WinnerResponse> ReturnWinner(int numberOfPlayers)
        {
            WinnerResponse _winnerResponse = new WinnerResponse();
            var Player = new Player();
            var Players = new List<Player>();

            //create players name and id
            Players = await GeneratePlayersIdentities(numberOfPlayers);

            var assignNoCardsToPlayers = new List<Player>();

            //assign cards to players
            assignNoCardsToPlayers = await AssignNoCardsToPlayers(Players);
            
            //display all cards to the screen in hidden format 
            await DisplayAllCardsToScreenInHiddenform(assignNoCardsToPlayers);
            
            //Player start playing
            //Below variable is used for playing pool i.e to keep track of the card the players played
            var PlayingPool = new List<PlayerPool>();
            
            //Below is used for Snap Pool
            var returnAllPlayersThatPlayed = new List<SnapChatPlayer>();

            returnAllPlayersThatPlayed = await PlayerPlaying(assignNoCardsToPlayers, Players);

            //check who has max cards
            var winPlayer = await WhoHasMaxCards(returnAllPlayersThatPlayed);
            _winnerResponse.PlayerIdentity = winPlayer.PlayerIdentity;
            _winnerResponse.TotalNumberOfCard = winPlayer.TotalNumberOfCard;

            return _winnerResponse;
        }
        public async  Task<SnapChatPlayer> WhoHasMaxCards(List<SnapChatPlayer> resultOfPlayerWithSnaps)
        {
            int maxTotalCardInResult = resultOfPlayerWithSnaps.Max(c => c.TotalNumberOfCard);
            return resultOfPlayerWithSnaps.First(x => x.TotalNumberOfCard == maxTotalCardInResult);
        }
    }
}