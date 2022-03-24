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
                CardsAssign.Add(new CardsInformation { CardNo = startCount + 1, LetterOnCard = await GenerateCardLetter(), CardIconOrImage = await GenerateCardIconOrImage(), IconColor = await GenerateCardColor() });
            }
            return CardsAssign;
        }

        public async Task<string> GenerateCardLetter()
        {
            string[] names = new string[] { "K", "K", "K" };
            //{ "Q", "K", "K" };// hard code would later uncommented { "A", "2", "3", "4", "5", "6", "7","8", "9", "10", "J", "Q", "K" };
            Random rnd = new Random();
            int index = rnd.Next(names.Length);
            return names[index];
        }

        public async Task<string> GenerateCardColor()
        {
            string[] names = new string[] { "Black", "Red" };
            Random rnd = new Random();
            int index = rnd.Next(names.Length);
            return names[index];
        }

        public async Task<string> GenerateCardIconOrImage()
        {
            string[] names = new string[] { "Queen", "King" };
            Random rnd = new Random();
            int index = rnd.Next(names.Length);
            return names[index];
        }

    }
}
