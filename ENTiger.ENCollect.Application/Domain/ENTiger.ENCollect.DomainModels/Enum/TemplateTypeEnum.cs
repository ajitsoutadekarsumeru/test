namespace ENTiger.ENCollect;

public class TemplateTypeEnum : FlexEnum
{
    public TemplateTypeEnum()
    { }

    public TemplateTypeEnum(string value, string displayName) : base(value, displayName)
    {
    }
    public static readonly TemplateTypeEnum Email = new TemplateTypeEnum("0", "Email");
    public static readonly TemplateTypeEnum SMS = new TemplateTypeEnum("1", "SMS");
    public static readonly TemplateTypeEnum Letter = new TemplateTypeEnum("2", "Letter");
}