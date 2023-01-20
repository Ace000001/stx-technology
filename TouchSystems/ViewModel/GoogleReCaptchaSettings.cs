namespace TouchSystems.ViewModel
{
    public class GoogleReCaptchaSettings
    {
        public string SiteKey { get; set; }
        public string SecretKey { get; set; }
        public bool Success { get; set; }
        public double Score { get; set; }
    }
}
