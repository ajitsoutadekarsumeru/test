﻿namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAccountLabelsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Label { get; set; }
    }
}