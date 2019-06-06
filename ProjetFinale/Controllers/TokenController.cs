/*using ProjetFinale.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace ProjetFinale.Controllers
{
    public class TokenController : ApiController
    {
       
        private readonly ITokenGenerator _tokenGenerator;

        public TokenController() : this(new TokenGenerator()) { }

        public TokenController(ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        internal object WithCallTo(Func<object, object> p)
        {
            throw new NotImplementedException();
        }

        // POST: Token
        [System.Web.Http.HttpPost]
        public IHttpActionResult Index(string device, string identity)
        {
            if (device == null || identity == null) return null;

            const string appName = "TwilioChatDemo";
            var endpointId = string.Format("{0}:{1}:{2}", appName, identity, device);

            var token = _tokenGenerator.Generate(identity, endpointId);
            string obj = "identity , token";
            return Ok(obj);
        }
    }
}

*/