using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect;

public class UserLevelProjection : DomainModelBridge
{
    /// <summary>
    /// PK & FK to ApplicationUser.Id
    /// </summary>
    public string ApplicationUserId { get; private set; }

       /// <summary>
    /// Navigation back to the user.
    /// </summary>
    public ApplicationUser ApplicationUser { get; private set; }

    /// <summary>
    /// The level of the designation the user holds.
    /// </summary>
    public int Level { get; private set; } //MaxLevel

    // EF Core requires parameterless ctor
    public UserLevelProjection() { }

    /// <summary>
    /// Initialize a new projection for a user, default level = 0.
    /// </summary>
    public UserLevelProjection(string applicationUserId, int level)
    {
        if (applicationUserId == string.Empty)
            throw new ArgumentException("applicationUserId is required", nameof(applicationUserId));
        Id = SequentialGuid.NewGuidString();
        ApplicationUserId = applicationUserId;
        Level = level;

        this.TrackingState = TrackingState.Added;
    }
    public void Delete()
    {
        this.TrackingState = TrackingState.Deleted;
        this.SetAddedOrModified();
    }

}
