using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts
{
    public  interface IValidatePlayer
    {
        Task<int> NoOfPlayers(int SuppliedNoOfPlayer);
    }
}
