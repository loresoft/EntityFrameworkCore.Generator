using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator
{
    [HelpOption("--help")]
    public abstract class CommandBase
    {
        protected CommandBase(ILoggerFactory logger, IConsole console)
        {
            Logger = logger.CreateLogger(GetType());
            Console = console;            
        }

        protected ILogger Logger { get; }

        protected IConsole Console { get; }

        protected abstract int OnExecute(CommandLineApplication application);
    }
}