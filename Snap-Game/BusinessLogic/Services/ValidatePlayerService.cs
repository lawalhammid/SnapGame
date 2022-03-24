using BusinessLogic.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    /// <summary>
    /// the function NoOfPlayers returns 0 means succesfully 
    /// validated else invalid number of players
    /// </summary>
    public class ValidatePlayerService : IValidatePlayer
    {
        private const int MinNumberOfPlayers = 2;
        private const int MaxNumberOfPlayers = 6;
        public async Task<int> NoOfPlayers(int SuppliedNoOfPlayer)
        {
            if(SuppliedNoOfPlayer < MinNumberOfPlayers || 
                SuppliedNoOfPlayer > MaxNumberOfPlayers)
            {
                Console.WriteLine($"The number of players must be atleast { MinNumberOfPlayers } and must not be more than {MaxNumberOfPlayers} players");
                return 1; // error validate
            }

            return 0; // success validate 
        }
    }
}
