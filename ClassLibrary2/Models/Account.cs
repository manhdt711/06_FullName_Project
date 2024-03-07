using System;
using System.Collections.Generic;

namespace ElecStore.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public byte Type { get; set; }
    }
}
