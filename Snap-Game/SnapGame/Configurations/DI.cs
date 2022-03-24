using BusinessLogic.Contracts;
using BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnapGame.Configurations
{
    /// <summary>
    /// This file is for DI registration
    /// </summary>
    public static class DI
    {
        public static ServiceProvider RegisterDI()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IReturnCardsObjects, ReturnCardsObjectsService>()
                .AddSingleton<IValidatePlayer, ValidatePlayerService>()
                .AddSingleton<IPlayGame, PlayGameService>()

              .BuildServiceProvider();

            return serviceProvider;
        }
    }

}
