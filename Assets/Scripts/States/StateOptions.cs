using System;

namespace Modules.States
{
    [Flags]
    public enum StateOptions
    {
        None                                    = 0,
        ClosePrevious                           = 1 << 0,
        AddToStack                              = 1 << 1,
        LinkWithLastOnStack                     = 1 << 2,
        RemovePreviousFromStack                 = 1 << 3,
        ClosePreviousAndAddToStack              = ClosePrevious | AddToStack,
        CloseAndRemovePreviousFromStack         = ClosePrevious | RemovePreviousFromStack
    }
}