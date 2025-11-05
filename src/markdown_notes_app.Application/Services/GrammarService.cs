using markdown_notes_app.Core.Entities;
using markdown_notes_app.Core.Interfaces.Common;
using markdown_notes_app.Core.Results;
using markdown_notes_app.Infrastructure.ExternalServices;

namespace markdown_notes_app.Application.Services
{
    public class GrammarService
    {
        private readonly SaplingAPIClient _saplingApiClient;
        private readonly ILoggerManager<GrammarService> _loggerManager;

        public GrammarService(SaplingAPIClient saplingAPIClient, ILoggerManager<GrammarService> logger)
        {
                _saplingApiClient = saplingAPIClient;
                _loggerManager = logger;
        }

        public async Task<Result<SaplingResponse>> GrammarCheckAsync(string text, object jsonBody)
        {
            try
            {
                if (_saplingApiClient == null)
                    return ResultFactory.Error<SaplingResponse>("SaplingAPIClient is null.", 500, "Grammar Service Failure");
                return await _saplingApiClient.PostSpellCheckAsync(text, jsonBody);
            }
            catch (Exception ex)
            {
                //log - More Robust Errors Saying Where My Error is
                return ResultFactory.Error<SaplingResponse>(ex.Message, 500, "Grammar Service Failure");
            }
        }

        public async Task<string> ApplyCorrectionsAsync(string text, SaplingResponse saplingResponse)
        {
            throw new NotImplementedException();
        }
    }
}
