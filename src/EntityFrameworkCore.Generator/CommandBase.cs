using System;
using McMaster.Extensions.CommandLineUtils;

namespace EntityFrameworkCore.Generator
{
    [HelpOption("--help")]
    public abstract class CommandBase
    {
        protected abstract int OnExecute(CommandLineApplication application);
    }
}