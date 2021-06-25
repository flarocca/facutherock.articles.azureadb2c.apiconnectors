namespace FacuTheRock.Articles.AzureAdB2C.ApiConnectors.Responses
{
    public class ShowBlockPageResponseContent : ResponseContentBase
    {
        public ShowBlockPageResponseContent(string userMessage)
            : base(ResponseType.ShowBlockPage, userMessage)
        {
        }
    }
}
