//using ProxyBenefix;
using System.Threading.Tasks;

namespace Common.Proxies
{
	public class GestaoFiscalService
	{
		//private readonly NfeiServiceSoapClient _service;
		public GestaoFiscalService()
		{
			//var remoteAddress = "https://homolog.e-benefix.com.br/NFeV10/nfeiservice.asmx";
			//var remoteAddress = "https://www1.webenefix.com.br/epoca/nfeiservice.asmx";
			//_service = new NfeiServiceSoapClient(NfeiServiceSoapClient.EndpointConfiguration.NfeiServiceSoap, remoteAddress);
		}

		public async Task SolicitarImportacaoArquivoXML()
		{
			//var result = await _service.SolicitarImportacaoArquivoXMLAsync(new byte[] { }, "CNPJ", 0, "usuario", "senha");
		}
	}
}
