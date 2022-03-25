using BusinessLogic.Contracts;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    /// <summary>
    /// This class is used to set up all the cards details
    /// </summary>
    public class ReturnCardsObjectsService : IReturnCardsObjects
    {
        private const int TotalNumOfCards = 3;// 52; the standard card number is 52 hard code
        public async Task<IEnumerable<CardsInformation>> AllCards()
        {
            var CardsAssign = new List<CardsInformation>();

            for (int startCount = 0; startCount < TotalNumOfCards; startCount++)
            {
                string cardFullInfo = await GenerateCardFullInfo(startCount);
                char Letter = await GenerateCardLetter(cardFullInfo);
                CardsAssign.Add(new CardsInformation { CardNo = startCount + 1, LetterOnCard = Letter, CardIconOrImage = cardFullInfo, IconColor = await GenerateCardColor() });
            }
            return CardsAssign;
        }

        public async Task<string> GenerateCardFullInfo(int index)
        {
            string[] names = new string[] { "King", "Queen", "King" };//, "A", "J", "A" };

            return names[index];
        }

        public async Task<char> GenerateCardLetter(string cardName)
        {
            return cardName[0];
        }

        public async Task<string> GenerateCardColor()
        {
            string[] names = new string[] { "Black", "Red" };
            Random rnd = new Random();
            int index = rnd.Next(names.Length);
            return names[index];
        }

    

    }
}
