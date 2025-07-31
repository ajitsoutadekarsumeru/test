namespace ENTiger.ENCollect;

public class TriggerConditionTypeEnum : FlexEnum
{
    public TriggerConditionTypeEnum()
    { }

    public TriggerConditionTypeEnum(string value, string displayName) : base(value, displayName)
    {
    }
    public static readonly TriggerConditionTypeEnum XDaysBeforeDueDate = new TriggerConditionTypeEnum("XDaysBeforeDueDate", "XDaysBeforeDueDate");
    public static readonly TriggerConditionTypeEnum XDaysAfterStatementDate = new TriggerConditionTypeEnum("XDaysAfterStatementDate", "XDaysAfterStatementDate");
    public static readonly TriggerConditionTypeEnum XDaysPastDue = new TriggerConditionTypeEnum("XDaysPastDue", "XDaysPastDue");
    public static readonly TriggerConditionTypeEnum OnPtpDate = new TriggerConditionTypeEnum("OnPtpDate", "OnPtpDate");
    public static readonly TriggerConditionTypeEnum OnBrokenPtp = new TriggerConditionTypeEnum("OnBrokenPtp", "OnBrokenPtp");
    public static readonly TriggerConditionTypeEnum OnAgencyAllocationChange = new TriggerConditionTypeEnum("OnAgencyAllocationChange", "OnAgencyAllocationChange");
}

