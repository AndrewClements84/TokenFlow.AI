using System;
using TokenFlow.AI.Registry;

namespace TokenFlow.Tools.Commands
{
    public static class ListModelsCommand
    {
        public static int Run(IModelRegistry registry = null)
        {
            registry ??= new ModelRegistry();

            Console.WriteLine($"[TokenFlow.AI] Models loaded from: {registry.LoadSource}");
            Console.WriteLine("------------------------------------------------");

            foreach (var model in registry.GetAll())
                Console.WriteLine($"{model.Id,-20} | {model.Family,-10} | Max In: {model.MaxInputTokens,8} | Max Out: {model.MaxOutputTokens,8}");

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"Total models: {registry.GetAll().Count}");
            return 0;
        }
    }
}

