using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AngleSharp.Io;

namespace RepoLint
{
	internal class FileRequester : BaseRequester
	{
		private static readonly Dictionary<string, byte[]> fileCache = new Dictionary<string, byte[]>();

		public override bool SupportsProtocol(string protocol) => protocol == ProtocolNames.File;
		protected override async Task<IResponse> PerformRequestAsync(Request request, CancellationToken cancel)
		{
			var path = HttpUtility.UrlDecode(request.Address.Path);
			if (!fileCache.TryGetValue(path, out byte[] content))
			{
				content = await File.ReadAllBytesAsync(path).ConfigureAwait(false);
				fileCache.Add(path, content);
			}

			return new DefaultResponse { Address = request.Address, Content = new MemoryStream(content), StatusCode = System.Net.HttpStatusCode.OK };
		}
	}
}