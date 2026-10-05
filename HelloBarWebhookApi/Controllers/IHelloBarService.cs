namespace HelloBarWebhookApi.Controllers
{
    public interface IHelloBarService
    {
        int InsertHelloBarEmail(string name,string email,string? winningOffer,string? winningOfferCode);
        int InsertHelloBarEmail(string name, string email);
    }
}