using Foundation;

namespace BundledCode {

	public class CustomProtocol : NSObject, INSUrlProtocolClient {

		public CustomProtocol (NSUrlRequest request, NSCachedUrlResponse cachedResponse, INSUrlProtocolClient client)
		{
		}

		public void CachedResponseIsValid (NSUrlProtocol protocol, NSCachedUrlResponse cachedResponse)
		{
		}

		public void CancelledAuthenticationChallenge (NSUrlProtocol protocol, NSUrlAuthenticationChallenge challenge)
		{
		}

		public void DataLoaded (NSUrlProtocol protocol, NSData data)
		{
		}

		public void FailedWithError (NSUrlProtocol protocol, NSError error)
		{
		}

		public void FinishedLoading (NSUrlProtocol protocol)
		{
		}

		public void ReceivedAuthenticationChallenge (NSUrlProtocol protocol, NSUrlAuthenticationChallenge challenge)
		{
		}

		public void ReceivedResponse (NSUrlProtocol protocol, NSUrlResponse response, NSUrlCacheStoragePolicy policy)
		{
		}

		public void Redirected (NSUrlProtocol protocol, NSUrlRequest redirectedToEequest, NSUrlResponse redirectResponse)
		{
		}
	}
}
