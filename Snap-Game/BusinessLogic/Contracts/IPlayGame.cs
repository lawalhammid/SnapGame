using BusinessLogic.ViewModels;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts
{
    public interface IPlayGame
    {
        Task<List<Player>> GeneratePlayersIdentities(int numberOfPlayers);
        Task<List<Player>> AssignNoCardsToPlayers(List<Player> Player);
        Task<string> DisplayAllCardsToScreenInHiddenform(List<Player> assignNoCardsToPlayers);
        Task<List<SnapChatPlayer>> PlayerPlaying(List<Player> assignNoCardsToPlayers, List<Player> Players);     
        Task<SnapChatPlayer> WhoHasMaxCards(List<SnapChatPlayer> numberOfPlayers);
        Task<WinnerResponse> ReturnWinner(int numberOfPlayers);
    }
}
