using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace API_Auto_Test
{
    public  class ResponseHandler
    {
        public string content { get; }
        public string headers { get; }
        public string statusCode {get;}
        public string contentType {get;}
        public string contentLength {get;}

        public ResponseHandler(IRestResponse response)
        {
            content = response.Content.ToString();
            headers = response.Headers.ToString();
            statusCode = response.StatusCode.ToString();
            contentType = response.StatusCode.ToString();
            contentLength = response.StatusCode.ToString();
        }
        public static string CheckStatus(string statuscode)
        {
            Dictionary<string, string> codes = new Dictionary<string, string>()
            {
                { " Continue", "(100) - Indicates that all is still in order and that the client should continue with the request or ignore it if it has already been completed." },
                { "Switching Protocol", "(101) - Specifies the protocol that the server switches using the client Upgrade request (en-US)." },
                { "OK", "(200) - Successful. The request has been successfully processed" },
                { "Created", "(201) - Indicates that the request was successful and led to the creation of the resource." },
                { "Accepted", "(202) - Indicates that the request has been received but has not yet been processed." },
                { "Non-Authoritative Information", "(203) - Indicates that the request was successful, but the attached payload was modified to account for the origin server200 (OK) response using a conversion proxy." },
                { "No Content", "(204) - Indicates that the request is successful, but that the client does not need to leave their current page." },
                { "Reset Content", "(205) - Informs the client of a document view change, e.g. to clear the contents of a form, reset the canvas state or update the user interface." },
                { "Partial Content", "(206) - Indicates that the request has succeeded and the body contains the requested data ranges as described in Range query" },
                { "Multiple Choices", "(300) - Indicates that the request has more than one possible responses." },
                { "Moved Permanently", "(301) - Indicates that the requested resource has been definitively moved to the URL given by the Location headers." },
                { "Found", "(302) - Indicates that the resource requested has been temporarily moved to the URL given by the Location header. " +
                "A browser redirects to this page but search engines don't update their links to the resource (in 'SEO-speak', it is said that the 'link-juice' is not sent to the new URL)." },
                { "See Other", "(303) - Indicates that the redirects don't link to the newly uploaded resources, but to another page (such as a confirmation page or an upload progress page). " },
                { "Not Modified", "(304) - Indicates that there is no need to retransmit the requested resources." },
                { "Temporary Redirect", "(307) - Indicates that the resource requested has been temporarily moved to the URL given by the Location headers." },
                { "Permanent Redirect", "(308) - Indicates that the resource requested has been definitively moved to the URL given by the Location headers." },
                { "Bad request", "(400) - Indicates that the server cannot or will not process the request due to something that is perceived to be a client error " +
                "(for example, malformed request syntax, invalid request message framing, or deceptive request routing)." },
                { "Unauthorized", "(401) - Indicates that the client request has not been completed because it lacks valid authentication credentials for the requested resource." },
                { "Payment Required", "(402) -  Is a nonstandard response status code that is reserved for future use. " +
                "This status code was created to enable digital cash or (micro) payment systems and would indicate that the requested content is not available until the client makes a payment." },
                { "Forbidden", "(403) -  Response status code indicates that the server understands the request but refuses to authorize it." },
                { "Not Found", "(404) - Response status code indicates that the server cannot find the requested resource." },
                { "Method Not Allowed", "(405) - Response status code indicates that the server knows the request method, but the target resource doesn't support this method." },
                { "Not Acceptable", "(406) - Client error response code indicates that the server cannot produce a response matching the list of acceptable values defined in the request's proactive content negotiation headers, and that the server is unwilling to supply a default representation." },
                { "Proxy Authentication Required", "(407) - Client error status response code indicates that the request has not been applied because it lacks valid authentication credentials for a proxy server that is between the browser and the server that can access the requested resource." },
                { "Request Timeout", "(408) - Response status code means that the server would like to shut down this unused connection." },
                { "Conflict", "(409) - Response status code indicates a request conflict with current state of the target resource." },
                { "Gone", "(410) - Client error response code indicates that access to the target resource is no longer available at the origin server and that this condition is likely to be permanent." },
                { "Length Required", "(411) - Client error response code indicates that the server refuses to accept the request without a defined Content-Length header." },
                { "Precondition Failed", "(412) - Client error response code indicates that access to the target resource has been denied." },
                { "Payload Too Large", "(413) - Response status code indicates that the request entity is larger than limits defined by server; the server might close the connection or return a Retry-After header field." },
                { "URI Too Long", "(414) - Response status code indicates that the URI requested by the client is longer than the server is willing to interpret." },
                { "Unsupported Media Type", "(415) - Client error response code indicates that the server refuses to accept the request because the payload format is in an unsupported format." },
                { "Range Not Satisfiable", "(416) - Error response code indicates that a server cannot serve the requested ranges." },
                { "Expectation Failed", "(417) - Client error response code indicates that the expectation given in the request's Expect header could not be met." },
                { "I'm a teapot", "(418) - client error response code indicates that the server refuses to brew coffee because it is, permanently, a teapot. " +
                "A combined coffee/tea pot that is temporarily out of coffee should instead return 503." },
                { "Unprocessable Entity", "(422) - response status code indicates that the server understands the content type of the request entity, and the syntax of the request entity is correct, " +
                "but it was unable to process the contained instructions." },
                { "Too Early", "(425) - Response status code indicates that the server is unwilling to risk processing a request that might be replayed, which creates the potential for a replay attack." },
                { "Upgrade Required", "(426) - Client error response code indicates that the server refuses to perform the request using the current protocol but might be willing to do so after the client upgrades to a different protocol." },
                { "Precondition Required", "(428) - Response status code indicates that the server requires the request to be conditional." },
                { "Too Many Requests", "(429) - Response status code indicates the user has sent too many requests in a given amount of time ('rate limiting')." },
                { "Request Header Fields Too Large", "(431) - Response status code indicates that the server refuses to process the request because the request's HTTP headers are too long. " +
                "The request may be resubmitted after reducing the size of the request headers." },
                { "Unavailable For Legal Reasons", "(451) - Client error response code indicates that the user requested a resource that is not available due to legal reasons, such as a web page for which a legal action has been issued." },
                { "Internal Server Error", "(500) - Server error response code indicates that the server encountered an unexpected condition that prevented it from fulfilling the request." },
                { "Not Implemented", "(501) - Server error response code means that the server does not support the functionality required to fulfill the request." },
                { "Bad Gateway", "(502) - Server error response code indicates that the server, while acting as a gateway or proxy, received an invalid response from the upstream server." },
                { "Service Unavailable", "(503) - Server error response code indicates that the server is not ready to handle the request." },
                { "Gateway Timeout", "(504) - Server error response code indicates that the server, while acting as a gateway or proxy, did not get a response in time from the upstream server that it needed in order to complete the request." },
                { "HTTP Version Not Supported", "(505) - Response status code indicates that the HTTP version used in the request is not supported by the server." },
                { "Variant Also Negotiates", "(506) - Response status code may be given in the context of Transparent Content Negotiation" },
                { "Insufficient Storage", "(507) - Response status code may be given in the context of the Web Distributed Authoring and Versioning (WebDAV) protocol" },
                { "Loop Detected", "(508) - Response status code may be given in the context of the Web Distributed Authoring and Versioning (WebDAV) protocol." },
                { "Not Extended", "(510) - Response status code is sent in the context of the HTTP Extension Framework." },
                { "Network Authentication Required", "(511) - Response status code indicates that the client needs to authenticate to gain network access." },
            };
            foreach (var status in codes)
                if (statuscode == status.Key)
                    return status.Value;
            return "";
        }
    }
}
