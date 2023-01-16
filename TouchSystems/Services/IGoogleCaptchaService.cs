using System.Threading.Tasks;

namespace TouchSystems.Services
{
    public interface IGoogleCaptchaService
    {
        Task<bool> VerifyToken(string token, double threshold);
    }
}
