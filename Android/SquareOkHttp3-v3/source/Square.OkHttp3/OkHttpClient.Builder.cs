using System;
using System.Collections.Generic;
using Java.Net;
using Javax.Net.Ssl;

namespace Square.OkHttp3
{
    partial class OkHttpClient
    {
        partial class Builder
        {
            public Builder AddInterceptor(Func<IInterceptorChain, Response> interceptor)
            {
                return AddInterceptor(new InterceptorImpl(interceptor));
            }

            public Builder AddNetworkInterceptor(Func<IInterceptorChain, Response> interceptor)
            {
                return AddNetworkInterceptor(new InterceptorImpl(interceptor));
            }

            public Builder Authenticator(Func<Route, Response, Request> authenticate)
            {
                return Authenticator(new AuthenticatorImpl(authenticate));
            }

            public Builder ProxyAuthenticator(Func<Route, Response, Request> authenticate)
            {
                return ProxyAuthenticator(new AuthenticatorImpl(authenticate));
            }

            public Builder Dns(Func<string, IList<InetAddress>> lookup)
            {
                return Dns(new DnsImpl(lookup));
            }

            public Builder HostnameVerifier(Func<string, ISSLSession, bool> verify)
            {
                return HostnameVerifier(new HostnameVerifierImpl(verify));
            }

            private class AuthenticatorImpl : Java.Lang.Object, IAuthenticator
            {
                private readonly Func<Route, Response, Request> authenticate;

                public AuthenticatorImpl(Func<Route, Response, Request> authenticate)
                {
                    this.authenticate = authenticate;
                }

                public Request Authenticate(Route p0, Response p1)
                {
                    return authenticate(p0, p1);
                }
            }

            private class InterceptorImpl : Java.Lang.Object, IInterceptor
            {
                private readonly Func<IInterceptorChain, Response> interceptor;

                public InterceptorImpl(Func<IInterceptorChain, Response> interceptor)
                {
                    this.interceptor = interceptor;
                }

                public Response Intercept(IInterceptorChain p0)
                {
                    return interceptor(p0);
                }
            }

            private class DnsImpl : Java.Lang.Object, IDns
            {
                private readonly Func<string, IList<InetAddress>> lookup;

                public DnsImpl(Func<string, IList<InetAddress>> lookup)
                {
                    this.lookup = lookup;
                }

                public IList<InetAddress> Lookup(string p0)
                {
                    return lookup(p0);
                }
            }

            private class HostnameVerifierImpl : Java.Lang.Object, IHostnameVerifier
            {
                private readonly Func<string, ISSLSession, bool> verify;

                public HostnameVerifierImpl(Func<string, ISSLSession, bool> verify)
                {
                    this.verify = verify;
                }

                public bool Verify(string hostname, ISSLSession session)
                {
                    return verify(hostname, session);
                }
            }
        }
    }
}
