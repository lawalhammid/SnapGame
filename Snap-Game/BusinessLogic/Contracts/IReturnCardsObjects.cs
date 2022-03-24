using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts
{
    public interface IReturnCardsObjects
    {
        //I used task incase I changed it to fetch data from database
        Task<IEnumerable<CardsInformation>> AllCards();
        Task<string> GenerateCardLetter();
        Task<string> GenerateCardColor();
        Task<string> GenerateCardIconOrImage();
    }
}
