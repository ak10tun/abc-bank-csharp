using System;

namespace abc_bank
{
    public interface IPeriod
    {
        DateTime StartDateTime { get; }
        DateTime EndDateTime { get;  }
        int Duration { get; }

    }
}