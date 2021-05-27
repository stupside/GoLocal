namespace GoLocal.Bus.Authorizer.Commons.Responses
{
    public class AuthorizerResult
    {
        public bool Succeed => !(NotFound || Unauthorized);
        public bool NotFound { get; private set; }
        public bool Unauthorized { get; private set; }

        public AuthorizerResult IsNotFound()
        {
            if (Unauthorized)
                Unauthorized = false;
            
            NotFound = true;
            return this;
        }
        
        public AuthorizerResult IsUnauthorized()
        {
            if (NotFound)
                NotFound = false;
            
            Unauthorized = true;
            return this;
        }
    }
}