namespace ENTiger.ENCollect.DevicesModule
{
    public partial class ValidateRegisterDeviceOtpDto : DtoBridge
    {
        public string IMEI { get; set; }
        public string OTP { get; set; }
    }
}