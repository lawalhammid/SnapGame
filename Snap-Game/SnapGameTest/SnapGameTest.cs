using BusinessLogic.Contracts;
using BusinessLogic.Services;
using Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnapGameTest
{
    public class SnapGameTest
    {
        private Mock<IReturnCardsObjects> _iReturnCardsObjects;
        private Mock<IValidatePlayer> _iValidatePlayer;
        private ValidatePlayerService _validatePlayerService;
        private PlayGameService _playGameService;

        [SetUp]
        public void Setup()
        {
            _iReturnCardsObjects = new Mock<IReturnCardsObjects>();

            _iReturnCardsObjects.Setup(x => x.AllCards())
                .Returns(AllCardsTest());

            _validatePlayerService = new ValidatePlayerService();

            _playGameService = new PlayGameService(_iReturnCardsObjects.Object);
        }

        [Test]
        public void ValidateNoOfPlayersTest()
        {
            var res =  _validatePlayerService.NoOfPlayers(2).Result;

            Assert.IsTrue(res == 0);

            Assert.Pass();
        }

        [Test]
        public void GeneratePlayersIdentitiesTest()
        {
            int numberOfPlayer = 2;

            var res = _playGameService.GeneratePlayersIdentities(numberOfPlayer).Result;

            Assert.IsTrue(res.Count == numberOfPlayer);            
        }

        /* This is not runing well because of the foraeach I used in PlayerPlaying  function I will revisit.  need to submit cause ot time
         */
        [Test]
        public void ReturnWinnerTest()
        {
            int numberOfPlayer = 2;
            
            var res = _playGameService.ReturnWinner(numberOfPlayer).Result;

            Assert.IsTrue(res.TotalNumberOfCard > 0 && res.TotalNumberOfCard == 2);
        }

        public async Task<IEnumerable<CardsInformation>> AllCardsTest()
        {
            var CardsAssign = new List<CardsInformation>();

            CardsAssign.Add(new CardsInformation { CardNo = 1, LetterOnCard = "K", CardIconOrImage = "Queen", IconColor = "Black" });
            CardsAssign.Add(new CardsInformation { CardNo = 2, LetterOnCard = "K", CardIconOrImage = "Queen", IconColor = "Red" });
            CardsAssign.Add(new CardsInformation { CardNo = 3, LetterOnCard = "K", CardIconOrImage = "Queen", IconColor = "Black" });

            return CardsAssign;
        }
    }
}