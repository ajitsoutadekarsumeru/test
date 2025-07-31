using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect;

// Combines the scope filter with an optional ParentId.
public record EffectiveScope(AccountScopeFilter Filter, string? ParentId);
